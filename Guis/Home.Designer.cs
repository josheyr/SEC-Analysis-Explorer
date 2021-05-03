namespace SEC_Analysis_Explorer
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tickerComboBox = new System.Windows.Forms.ComboBox();
            this.watchTickerButton = new System.Windows.Forms.Button();
            this.filingViewer = new System.Windows.Forms.RichTextBox();
            this.tickerGroupBox = new System.Windows.Forms.GroupBox();
            this.tickerListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.formattedCheckBox = new System.Windows.Forms.CheckBox();
            this.filingListView = new System.Windows.Forms.ListView();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tickerGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tickerComboBox
            // 
            this.tickerComboBox.FormattingEnabled = true;
            this.tickerComboBox.Location = new System.Drawing.Point(6, 19);
            this.tickerComboBox.Name = "tickerComboBox";
            this.tickerComboBox.Size = new System.Drawing.Size(121, 21);
            this.tickerComboBox.TabIndex = 1;
            // 
            // watchTickerButton
            // 
            this.watchTickerButton.Location = new System.Drawing.Point(6, 46);
            this.watchTickerButton.Name = "watchTickerButton";
            this.watchTickerButton.Size = new System.Drawing.Size(121, 23);
            this.watchTickerButton.TabIndex = 2;
            this.watchTickerButton.Text = "Import Ticker";
            this.watchTickerButton.UseVisualStyleBackColor = true;
            this.watchTickerButton.Click += new System.EventHandler(this.watchTickerButton_Click);
            // 
            // filingViewer
            // 
            this.filingViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filingViewer.Location = new System.Drawing.Point(400, 12);
            this.filingViewer.Name = "filingViewer";
            this.filingViewer.Size = new System.Drawing.Size(631, 453);
            this.filingViewer.TabIndex = 3;
            this.filingViewer.Text = "";
            // 
            // tickerGroupBox
            // 
            this.tickerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tickerGroupBox.Controls.Add(this.tickerListBox);
            this.tickerGroupBox.Controls.Add(this.tickerComboBox);
            this.tickerGroupBox.Controls.Add(this.watchTickerButton);
            this.tickerGroupBox.Location = new System.Drawing.Point(12, 12);
            this.tickerGroupBox.Name = "tickerGroupBox";
            this.tickerGroupBox.Size = new System.Drawing.Size(135, 476);
            this.tickerGroupBox.TabIndex = 4;
            this.tickerGroupBox.TabStop = false;
            this.tickerGroupBox.Text = "Ticker";
            // 
            // tickerListBox
            // 
            this.tickerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tickerListBox.FormattingEnabled = true;
            this.tickerListBox.Location = new System.Drawing.Point(6, 75);
            this.tickerListBox.Name = "tickerListBox";
            this.tickerListBox.Size = new System.Drawing.Size(121, 394);
            this.tickerListBox.TabIndex = 3;
            this.tickerListBox.SelectedIndexChanged += new System.EventHandler(this.tickerListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.filingListView);
            this.groupBox1.Location = new System.Drawing.Point(153, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 476);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filing Browser";
            // 
            // formattedCheckBox
            // 
            this.formattedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.formattedCheckBox.AutoSize = true;
            this.formattedCheckBox.Location = new System.Drawing.Point(958, 471);
            this.formattedCheckBox.Name = "formattedCheckBox";
            this.formattedCheckBox.Size = new System.Drawing.Size(73, 17);
            this.formattedCheckBox.TabIndex = 6;
            this.formattedCheckBox.Text = "Formatted";
            this.formattedCheckBox.UseVisualStyleBackColor = true;
            this.formattedCheckBox.CheckedChanged += new System.EventHandler(this.formattedCheckBox_CheckedChanged);
            // 
            // filingListView
            // 
            this.filingListView.AllowColumnReorder = true;
            this.filingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.type,
            this.date});
            this.filingListView.HideSelection = false;
            this.filingListView.Location = new System.Drawing.Point(6, 19);
            this.filingListView.MultiSelect = false;
            this.filingListView.Name = "filingListView";
            this.filingListView.Size = new System.Drawing.Size(229, 446);
            this.filingListView.TabIndex = 7;
            this.filingListView.UseCompatibleStateImageBehavior = false;
            this.filingListView.View = System.Windows.Forms.View.Details;
            this.filingListView.SelectedIndexChanged += new System.EventHandler(this.filingListView_SelectedIndexChanged);
            // 
            // type
            // 
            this.type.Text = "Filing";
            this.type.Width = 75;
            // 
            // date
            // 
            this.date.Text = "Date";
            this.date.Width = 125;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 500);
            this.Controls.Add(this.formattedCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tickerGroupBox);
            this.Controls.Add(this.filingViewer);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.tickerGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox tickerComboBox;
        private System.Windows.Forms.Button watchTickerButton;
        private System.Windows.Forms.RichTextBox filingViewer;
        private System.Windows.Forms.GroupBox tickerGroupBox;
        private System.Windows.Forms.ListBox tickerListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox formattedCheckBox;
        private System.Windows.Forms.ListView filingListView;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader date;
    }
}

