using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace SEC_Analysis
{
    static class StockUtils
    {

        /// <summary>
        /// Uses a stock API to retreive the day information of a certain company on a specific date. This is returned as a JSON object
        /// as we are using a JSON api. In this function, it checks if we've already retrieved the data, if not we save it so we don't need
        /// to communicate with the API more. This is also useful since the API is rate limited, we don't want to be making uneccessary requests
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static JObject getStockDayInfo(string symbol, DateTime date)
        {
            string dateString = date.ToString("yyyy-MM-dd");
            string location = symbol + "&" + dateString;

            if (File.Exists(Directory.GetCurrentDirectory() + "/stockinfo/" + location + ".json"))
            {
                try
                {
                    string file = File.ReadAllText(Directory.GetCurrentDirectory() + "/stockinfo/" + location + ".json");
                    if (file != "404")
                    {
                        JObject o1 = JObject.Parse(file);

                        return o1;
                    }
                    else
                    {
                        return null;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                    File.Delete(Directory.GetCurrentDirectory() + "/stockinfo/" + location + ".json");
                    getStockDayInfo(symbol, date);
                }
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string s = client.DownloadString("https://api.polygon.io/v1/open-close/" + symbol + "/" + dateString + "?unadjusted=true&apiKey=5g64QeYR5cJMbsz0yUPUf2XloNd3RTvP");

                        File.WriteAllText(Directory.GetCurrentDirectory() + "/stockinfo/" + location + ".json", s);
                    }

                    getStockDayInfo(symbol, date);
                }
                catch(Exception e)
                {
                    if (e.Message.Contains("404"))
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + "/stockinfo/" + location + ".json", "404");
                        return getStockDayInfo(symbol, date);
                    }
                    Console.WriteLine("https://api.polygon.io/v1/open-close/" + symbol + "/" + dateString + "?unadjusted=true&apiKey=5g64QeYR5cJMbsz0yUPUf2XloNd3RTvP" + " -> " + e.Message);
                    Thread.Sleep(60000);

                    //Console.WriteLine("Failed to retrieve...\n" + e.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// This gets the stock day closing float from the stock day info we retreive in the function getStockDayInfo() above ^^^.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float getStockDayInfoFloat(string symbol, DateTime date, string value)
        {
            bool gotInfo = false;

            while (!gotInfo)
            {

                JObject stockDayInfo = getStockDayInfo(symbol, date);


                if (stockDayInfo != null)
                {
                    return (float)stockDayInfo[value];
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }

        /// <summary>
        /// This function retrieves the CIK (central index key) code from ticker (such as AAPL for Apple Inc). We do a similar thing as the functions
        /// above by storing it in a file and retreiving it from the file rather than directly from the site in future. This is also a recursive function
        /// as it calls itself.
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public static string getCIKFromTicker(string ticker)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/data/ticker.txt"))
            {
                foreach(string line in File.ReadAllLines(Directory.GetCurrentDirectory() + "/data/ticker.txt"))
                {
                    if(line.StartsWith(ticker + "	"))
                    {
                        return line.Split('	')[1];
                    }
                }

                return "none";
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString("https://www.sec.gov/include/ticker.txt");

                    File.WriteAllText(Directory.GetCurrentDirectory() + "/data/ticker.txt", s);
                }

                return getCIKFromTicker(ticker);
            }
        }

        /// <summary>
        /// This gets a list of all tickers using the same file as the getCIKFromTicker();
        /// </summary>
        /// <returns></returns>
        public static List<String> getAllTickers()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/data/ticker.txt"))
            {
                List<String> tickerList = new List<string>();

                foreach (string line in File.ReadAllLines(Directory.GetCurrentDirectory() + "/data/ticker.txt"))
                {
                    
                    tickerList.Add(line.Split('	')[0]);
                    
                }

                return tickerList;
            }
            else
            {
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString("https://www.sec.gov/include/ticker.txt");

                    File.WriteAllText(Directory.GetCurrentDirectory() + "/data/ticker.txt", s);
                }

                return getAllTickers();
            }
        }


        /// <summary>
        /// This function retreives list of filings from the ticker. This is done by a web scrape at the url https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=
        /// </summary>
        /// <param name="cik"></param>
        /// <returns></returns>
        public static Dictionary<string, DateTime> getFilingListing(string cik)
        {
            Dictionary<string, DateTime> filings = new Dictionary<string, DateTime>();

            using (WebClient client = new WebClient())
            {
                try
                {
                    string s = client.DownloadString("https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=" + cik + "&type=&dateb=&owner=exclude&start=0&count=40&output=atom");

                    XmlDocument xmlDoc = new XmlDocument();
                    try
                    {
                        xmlDoc.LoadXml(s);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(s);
                    }
                    XmlNodeList entries = xmlDoc.GetElementsByTagName("entry");

                    for (int i = 0; i < entries.Count; i++)
                    {
                        string filingType = entries[i]["content"]["filing-type"].InnerText;
                        //if (filingType == "8-K" || filingType == "10-K")
                        //{
                        Console.WriteLine(entries[i]["content"]["filing-href"].InnerText);

                        try
                        {
                            string filing_directory = client.DownloadString(entries[i]["content"]["filing-href"].InnerText);



                            {

                                string starter = "row\"><a href=\"/ix?doc=/Archives/edgar/data/";
                                string finisher = "\">";
                                int index_of_starter = filing_directory.IndexOf(starter);

                                int filing_address_length = filing_directory.IndexOf(finisher, index_of_starter + starter.Length) - index_of_starter;


                                string location = filing_directory.Substring(index_of_starter + starter.Length, filing_address_length - starter.Length);


                                if (location.EndsWith(".htm"))
                                {
                                    filings.Add(filingType + "|" + location, DateTime.Parse(entries[i]["updated"].InnerText));

                                    continue;
                                }
                            }

                            {
                                string starter = "row\"><a href=\"/Archives/edgar/data/";
                                string finisher = "\">";
                                int index_of_starter = filing_directory.IndexOf(starter);

                                int filing_address_length = filing_directory.IndexOf(finisher, index_of_starter + starter.Length) - index_of_starter;


                                string location = filing_directory.Substring(index_of_starter + starter.Length, filing_address_length - starter.Length);


                                if (location.EndsWith(".htm"))
                                {
                                    filings.Add(filingType + "|" + location, DateTime.Parse(entries[i]["updated"].InnerText));

                                    continue;
                                }

                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                catch(Exception e)
                {

                }

                return filings;
            }
        }
    }
}
