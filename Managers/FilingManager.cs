using SEC_Analysis.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SEC_Analysis
{
    class FilingManager
    {
        Collection<Filing> filings = new Collection<Filing>();

        Dictionary<HashSet<string>, int> scoreboard = new Dictionary<HashSet<string>, int>();

        public FilingManager()
        {

        }

        public void showScoreboard()
        {
            List<KeyValuePair<HashSet<string>, int>> myList = scoreboard.ToList();

            myList.Sort(
                delegate (KeyValuePair<HashSet<string>, int> pair1,
                KeyValuePair<HashSet<string>, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            for (int i = myList.Count; i > myList.Count - 10; i--)
            {
                Console.WriteLine("[" + String.Join(",", myList[i - 1].Key) + "] -> " + myList[i - 1].Value);

            }
        }

        public Collection<Filing> getFilings()
        {
            return filings;
        }

        /// <summary>
        /// Retrieves filings for ticker
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public Collection<Filing> getFilings(string ticker)
        {
            Collection<Filing> filings = new Collection<Filing>();

            foreach (Filing filing in this.filings)
            {
                if(filing.getTicker().ToLower() == ticker.ToLower())
                {
                    filings.Add(filing);
                }
            }

            return filings;
        }

        /// <summary>
        /// Instantiates a filing and adds to list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="symbol"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public Filing createFiling(string name, string type, string symbol, DateTime date)
        {
            Filing filing = new Filing(name, type, symbol, date);
            filings.Add(filing);

            return filing;
        }

        /// <summary>
        /// compares all loaded filings 
        /// </summary>
        public void compareAllFilings()
        {
            foreach(Filing filing1 in filings)
            {
                foreach(Filing filing2 in filings)
                {
                    if(filing1 != filing2)
                    {
                        compareFiling(filing1, filing2);
                    }
                }
            }
        }

        /// <summary>
        /// Compares two sentences and outputs a list of matched words.
        /// </summary>
        /// <param name="sentence1"></param>
        /// <param name="sentence2"></param>
        /// <returns></returns>
        public HashSet<string> compareSentence(string sentence1, string sentence2)
        {
            HashSet<string> matchedWords = new HashSet<string>();

            Regex rgx = new Regex("[^a-zA-Z -]");

            sentence1 = rgx.Replace(sentence1.ToLower().Replace(".", ""), "");
            sentence2 = rgx.Replace(sentence2.ToLower().Replace(".", ""), "");

            foreach (string word1 in sentence1.Split(' '))
            {
                foreach (string word2 in sentence2.Split(' '))
                {
                    if(word1 == word2)
                    {
                        matchedWords.Add(word1);
                    }
                }
            }

            return matchedWords;
        }

 
        /// <summary>
        /// Exapands a list to a huge list of everything that could be contained in that list (including in the strings), for analysis purposes.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<HashSet<string>> allContaining(HashSet<string> list)
        {
            List<string> mainList = list.ToList();

            List<HashSet<string>> allContaining = new List<HashSet<string>>();

            
            for (int i = 0; i < list.Count - 1; i++)
            {
                
                for (int a = i; a < list.Count + 1; a++)
                {
                    HashSet<string> newList = new HashSet<string>();

                    for (int b = 0; b < a - 1; b++)
                    {
                        if(mainList.Count > i + b)
                            newList.Add(mainList[i + b]);
                    }

                    allContaining.Add(newList);
                }
            }
            


            return allContaining;
        }

       
        /// <summary>
        /// Gets position of a word list in the scoreboard.
        /// </summary>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public int getPosition(HashSet<string> wordList)
        {
            int pos = 0;

            foreach (var a in scoreboard)
            {
                
                if (compareStringList(wordList, a.Key))
                {
                    return pos;
                }
                pos++;
            }

            return -1;
        }

        /// <summary>
        /// This compares a list of strings, normally these are sentences which are massively compared
        /// </summary>
        /// <param name="stringlist1"></param>
        /// <param name="stringlist2"></param>
        /// <returns></returns>
        public bool compareStringList(HashSet<string> stringlist1, HashSet<string> stringlist2)
        {
            foreach(string s in stringlist1)
            {
                if (!stringlist2.Contains(s))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// This loops each index of filings from a ticker.
        /// </summary>
        /// <param name="ticker"></param>
        public void loadAllFilingsFromTicker(string ticker)
        {
            foreach (KeyValuePair<string, DateTime> s in StockUtils.getFilingListing(StockUtils.getCIKFromTicker(ticker.ToLower())))
            {
                createFiling(s.Key.Split('|')[1].Replace("/", "&"), s.Key.Split('|')[0], ticker.ToUpper(), s.Value);
            }
        }

        /// <summary>
        /// Compares too filings, then builds a scoreboard from the filings of the words which are similar.
        /// </summary>
        /// <param name="filing1"></param>
        /// <param name="filing2"></param>
        public void compareFiling(Filing filing1, Filing filing2)
        {
            if (filing1.isStockSucceeding() != filing2.isStockSucceeding()) return;

            if (filing1.isUnstonkable() || filing2.isUnstonkable()) return;

            bool isStockSucceeding = filing1.isStockSucceeding();

            foreach (string sentence1 in filing1.getFilingSentences())
            {
                foreach (string sentence2 in filing2.getFilingSentences())
                {
                    HashSet<string> matchedWords = compareSentence(sentence1, sentence2);

                    foreach(HashSet<string> possibleMatchedWords in allContaining(matchedWords))
                    {
                        if (possibleMatchedWords.Count > 4) {



                            int add_to_score = (isStockSucceeding ? 1 : -2);

                            int position = getPosition(possibleMatchedWords);

                            if (position != -1)
                            {
                                int prev = scoreboard.ElementAt(position).Value;
                                scoreboard.Remove(possibleMatchedWords);

                                scoreboard.Add(possibleMatchedWords, prev + add_to_score);
                            }
                            else
                            {
                                scoreboard.Add(possibleMatchedWords, 0 + add_to_score);
                            }



                        }
                    }

                }
            }
        }
        
    }
}
