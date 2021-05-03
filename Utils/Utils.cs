using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SEC_Analysis
{
    static class Utils
    {

        /// <summary>
        /// This is ran on the start and checks if the folders which we store data in are made.
        /// </summary>
        public static void ensureFoldersMade()
        {
            string[] foldersNeeded = { "data", "filings", "stockinfo" };

            foreach(string folderNeeded in foldersNeeded)
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/" + folderNeeded))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + folderNeeded);
                }
            }
        }

        /// <summary>
        /// This strips specific HTML tags and aids us in web scraping into usable data by the program.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string stripHTML(string input)
        {
            input = input.Replace("|", "");
            input = Regex.Replace(input, "<ix.*?>", "");
            input = Regex.Replace(input, "</ix.*?>", "");
            input = Regex.Replace(input, "<span.*?>", "");
            input = Regex.Replace(input, "</span>", "");
            input = Regex.Replace(input, "<.*?>", "|");
            return input;
        }
    }
}
