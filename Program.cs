using SEC_Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEC_Analysis_Explorer
{
    static class Program
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();



            //filingManager.createFiling("0000320193/000119312520213158/d49399d8k".Replace("/", "&"), "AAPL");
            //filingManager.createFiling("0000320193/000032019321000009/aapl-20210127".Replace("/", "&"), "AAPL");
            //filingManager.createFiling("0001070235/000107023520000157/bbry-20201217".Replace("/", "&"), "BB");
            //filingManager.createFiling("0000924613/000110465921008124/tm214414d1_6k".Replace("/", "&"), "NOK");
            //filingManager.createFiling("0000317540/000119312520329877/d102651d8k".Replace("/", "&"), "COKE");
            //filingManager.createFiling("0001318605/000119312520236678/d21598d8k".Replace("/", "&"), "TSLA");
            //filingManager.createFiling("0000732717/000119312520246459/d68385d8k".Replace("/", "&"), "T");
            //filingManager.createFiling("0001707919/000149315221002243/form6-k".Replace("/", "&"), "NAKD");
            //filingManager.createFiling("0001537917/000119312520170525/d947077d8k".Replace("/", "&"), "TYME");
            //filingManager.createFiling("0001707919/000149315220000336/form6-k".Replace("/", "&"), "NAKD");
            //filingManager.createFiling("0001620737/000115752321000006/a52355321".Replace("/", "&"), "OGI");
            //filingManager.createFiling("0001595097/000149315221001868/form8-k".Replace("/", "&"), "CRBP");



            //filingManager.compareAllFilings();
            //filingManager.showScoreboard();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());

            //Console.WriteLine("StonkBot wishes you a good day.\n");

            //FilingManager filingManager = new FilingManager();

            //filingManager.loadAllFilingsFromTicker("googl");
            //filingManager.loadAllFilingsFromTicker("aapl");
            //filingManager.loadAllFilingsFromTicker("msft");
            //filingManager.loadAllFilingsFromTicker("bb");
            //filingManager.loadAllFilingsFromTicker("nok");

            //Console.WriteLine("all loaded");
        }
    }
}
