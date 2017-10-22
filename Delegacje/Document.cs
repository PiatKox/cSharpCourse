using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    public sealed class Document
    {
        public string Text
        {
            get;
            set;
        }

        public DateTime DocumentDate
        {
            get;
            set;
        }

        public string Author
        {
            get;
            set;
        }
    }
}
