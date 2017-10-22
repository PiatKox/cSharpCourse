using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    class TrademarkFilter
    {
        readonly List<string> trademarks = new List<string>();

        public List<string> Trademarks
        {
            get
            {
                return trademarks;
            }
        }

        public void HighlightTrademarks(Document doc)
        {
            string[] words = doc.Text.Split(' ', '.', ',');
            foreach(string word in words)
            {
                if (Trademarks.Contains(word))
                {
                    Console.WriteLine("Wyrozniono slowo \"{0}\".", word);
                }
            }
        }
    }
}
