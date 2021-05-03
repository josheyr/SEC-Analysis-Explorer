using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace SEC_Analysis
{
    class Filing
    {
        
        string ticker;
        DateTime dateFiled;
        bool successfulStock = false;
        bool unstonkable = true;
        string type = "";
        public string description { get; set; }


        List<string> filingSentences = new List<string>();
        string filingFormatted = "";

        /// <summary>
        /// Constructor of filings.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="type"></param>
        /// <param name="ticker"></param>
        /// <param name="date"></param>
        public Filing(string location, string type, string ticker, DateTime date)
        {
            this.type = type;
            this.description = type + " " + date.ToString();
            this.ticker = ticker;
            importFiling(location, date);
        }

        public string getType()
        {
            return type;
        }

        public string getFilingFormatted()
        {
            return filingFormatted;
        }

        public string getTicker()
        {
            return ticker;
        }

        /// <summary>
        /// Is the following week close more or less than the date filing was posted
        /// 
        /// also sets unstonkable to false if it manages to figure this out
        /// </summary>
        /// <returns>boolean for if its greater or not</returns>
        public bool isStockSucceeding()
        {
            DateTime oneWeekLater = dateFiled.AddDays(7);

            float openOnFile = StockUtils.getStockDayInfoFloat(ticker, dateFiled, "open");

            if (oneWeekLater < DateTime.Today)
            {
                float openOneWeekLater = StockUtils.getStockDayInfoFloat(ticker, oneWeekLater, "open");

                Console.WriteLine(openOnFile + " -> " + openOneWeekLater + " " + (openOnFile < openOneWeekLater ? "STONKS" : "NOT STONKS"));

                unstonkable = false;
                successfulStock = openOnFile < openOneWeekLater;
            }
            else
            {
                Console.WriteLine(openOnFile + " -> ???");
            }

            return successfulStock;
        }

        public bool isUnstonkable()
        {
            return unstonkable;
        }

        public List<string> getFilingSentences()
        {
            return filingSentences;
        }

        public void setDateFiled(DateTime dateFiled)
        {
            this.dateFiled = dateFiled;
        }
        public DateTime getDateFiled()
        {
            return dateFiled;
        }

        /// <summary>
        /// loads filing from raw lines, sets the filing deformatted lines, grabs stuff like date posted too
        /// </summary>
        /// <param name="fileLines"></param>
        public void loadFiling(string fileLines)
        {
            filingFormatted = fileLines;
            //Console.WriteLine(fileLines);

                string[] lineSplit = Utils.stripHTML(fileLines).Split('|');

                foreach (string line in lineSplit)
                {
                    if (line != "")
                    {
                        string[] sentenceSplit = line
                        .Replace("Inc. ", "Inc ")
                        .Replace("Ltd. ", "Ltd ")
                        .Replace("B.V. ", "BV ")
                        .Replace("Mr. ", "Mr ")
                        .Replace("Mrs. ", "Mrs ")
                        .Replace("Ms. ", "Ms ")
                        .Replace("&#160;", " ")
                        .Replace("&nbsp;", " ")
                        .Split(new[] { ". " }, StringSplitOptions.None);

                        foreach (string sentence in sentenceSplit)
                        {
                           

                        string new_sentence = Regex.Replace(sentence, @"\s+", " ");

                        new_sentence = WebUtility.HtmlDecode(new_sentence);

                        if (!(new_sentence.EndsWith(".") || new_sentence.EndsWith(":")) )
                            {
                                new_sentence = new_sentence + ".";
                            }

                            if ((new_sentence.Contains(" ") || new_sentence.Contains("Date")) && new_sentence.Length > 4 && !(new_sentence.Contains("&#9744;") || new_sentence.Contains("&#9746;")))
                            {
                            if (new_sentence.EndsWith(" ."))
                            {
                                new_sentence = new_sentence.Substring(0, new_sentence.Length - 2);
                            }
                                if (filingSentences.Count > 0) {
                                    if (new_sentence.StartsWith(" ") || filingSentences[filingSentences.Count - 1].EndsWith(" ") || filingSentences[filingSentences.Count - 1].EndsWith("Date:"))
                                    {
                                        filingSentences[filingSentences.Count - 1] = filingSentences[filingSentences.Count - 1] + new_sentence;
                                    }
                                    else
                                    {
                                        filingSentences.Add(new_sentence);
                                    }
                                }
                                else
                                {
                                    filingSentences.Add(new_sentence);
                                }
                            }
                        }

                    }
                }


            Console.WriteLine("Report from " + dateFiled.ToString() + " loaded!");

            
        }

        /// <summary>
        /// scrapes the filing from EDGAR (the filing website)
        /// </summary>
        /// <param name="location"></param>
        /// <param name="date"></param>
        public void importFiling(string location, DateTime date)
        {
            if(File.Exists(Directory.GetCurrentDirectory() + "/filings/" + location))
            {
                dateFiled = date;
                loadFiling(File.ReadAllText(Directory.GetCurrentDirectory() + "/filings/" + location));

            }
            else
            {
                using (WebClient client = new WebClient()){
                    string loc = "https://www.sec.gov/Archives/edgar/data/" + location.Replace("&", "/");

                    Console.WriteLine(loc);
                    try
                    {
                        client.DownloadFile(loc, Directory.GetCurrentDirectory() + "/filings/" + location);
                    }
                    catch (Exception e){
                    }
                }

                Thread.Sleep(100);

                importFiling(location, date);
            }
        }
    }
}
