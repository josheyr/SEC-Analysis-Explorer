using SEC_Analysis;
using SEC_Analysis_Explorer.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEC_Analysis_Explorer
{
    public partial class Home : Form
    {
        Config config;
        FilingManager filingManager;

        Collection<Filing> selectedFilings;

        public Home()
        {
            Utils.ensureFoldersMade();
                
            config = new Config();
            filingManager = new FilingManager();

            

            InitializeComponent();
        }

        private void updateWatchedTickerList()
        {
            tickerListBox.Items.Clear();
            
            foreach(String s in config.getWatchedTickers())
            {
                tickerListBox.Items.Add(s.ToUpper());
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            foreach(String s in StockUtils.getAllTickers())
            {
                tickerComboBox.Items.Add(s.ToUpper());
            }
        }

        private void watchTickerButton_Click(object sender, EventArgs e)
        {
            if (tickerComboBox.Items.Contains(tickerComboBox.Text.ToUpper()) && !config.getWatchedTickers().Contains(tickerComboBox.Text.ToLower()))
            {
                config.addWatchedTicker(tickerComboBox.Text.ToLower());
                filingManager.loadAllFilingsFromTicker(tickerComboBox.Text.ToLower());
                MessageBox.Show("Imported " + tickerComboBox.Text.ToUpper() + " successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updateWatchedTickerList();
                tickerComboBox.Text = "";
            }
        }

        private void tickerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tickerListBox.SelectedItem != null)
            {
                selectedFilings = filingManager.getFilings(tickerListBox.SelectedItem.ToString());

                filingListView.Items.Clear();

                foreach (Filing filing in selectedFilings)
                {
                    ListViewItem filingListViewItem = new ListViewItem();

                    filingListViewItem.Tag = filing;
                    filingListViewItem.Text = filing.getType();

                    filingListViewItem.SubItems.Add(filing.getDateFiled().ToString());
                    filingListView.Items.Add(filingListViewItem);
                }
            }
        }


        private void formattedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (filingListView.SelectedItems.Count != 0)
            {
                Filing selectedFiling = (Filing)filingListView.SelectedItems[0].Tag;

                if (formattedCheckBox.Checked)
                {
                    if (selectedFiling.getType().Contains("10") || selectedFiling.getType().Contains("S-"))
                    {
                        if (MessageBox.Show(selectedFiling.getType() + " documents are normally very large and may take a while to process.", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
                        != DialogResult.OK){
                            formattedCheckBox.Checked = false;
                            return;
                        }
                    }

                    //formats html (formatted)

                    WebBrowser wb = new WebBrowser();
                    wb.Navigate("about:blank");

                    wb.Document.Write(selectedFiling.getFilingFormatted());
                    wb.Document.ExecCommand("SelectAll", false, null);
                    wb.Document.ExecCommand("Copy", false, null);

                    filingViewer.SelectAll();
                    filingViewer.Paste();
                }
                else
                {
                    filingViewer.Lines = selectedFiling.getFilingSentences().ToArray();

                }

                filingViewer.SelectionStart = 0;
                filingViewer.ScrollToCaret();
            }
        }

        private void filingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filingListView.SelectedItems.Count != 0)
            {
                formattedCheckBox.Checked = false;
                Filing selectedFiling = (Filing)filingListView.SelectedItems[0].Tag;

                filingViewer.Lines = selectedFiling.getFilingSentences().ToArray();

                filingViewer.SelectionStart = 0;
                filingViewer.ScrollToCaret();
            }
            else
            {
                filingViewer.Clear();
            }
        }
    }
}
