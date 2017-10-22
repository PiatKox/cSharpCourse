using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    class ProcessEventArgs : EventArgs
    {
        public ProcessEventArgs(Document document)
        {
            Document = document;
        }

        public Document Document
        {
            get;
            private set;
        }
    }

    class ProcessCancelEventArgs : CancelEventArgs
    {
        public ProcessCancelEventArgs(Document document)
        {
            Document = document;
        }

        public Document Document
        {
            get;
            private set;
        }
    }
}
