using System;
using System.Collections.Generic;
using System.Text;

namespace SEC_Analysis.Classes
{
    class WordListScore
    {
        public int score
        {
            get; set;
        }

        public HashSet<string> wordList
        {
            get; set;
        }

        public WordListScore(HashSet<string> wordList, int score)
        {
            this.score = score;
            this.wordList = wordList;
        }

        public WordListScore()
        {

        }
    }
}
