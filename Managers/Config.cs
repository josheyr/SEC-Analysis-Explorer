using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEC_Analysis_Explorer.Managers
{
    class Config
    {
        string configFileLocation = "";
        List<String> watchedTickers = new List<String>();

        public Config()
        {
            loadConfig();
        }

        public void loadConfig()
        {

        }

        public void saveConfig()
        {

        }

        public void addWatchedTicker(String ticker)
        {
            watchedTickers.Add(ticker);
        }

        public void removeWatchedTicker(String ticker)
        {
            watchedTickers.Remove(ticker);
        }

        public List<String> getWatchedTickers()
        {
            return watchedTickers;
        }
    }
}
