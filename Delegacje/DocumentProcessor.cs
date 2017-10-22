using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    //delegate string LogTextProvider(Document doc);

    class DocumentProcessor
    {        
        class ActionCheckPair
        {
            public Action<Document> Action { get; set; }
            public Predicate<Document> QuickCheck { get; set; }
        }

        private Dictionary<string, Delegate> events;

        public event EventHandler<ProcessCancelEventArgs> Processing
        {
            add
            {
                Delegate theDelegate = EnsureEvent("Processing");

                events["Processing"] = ((EventHandler<ProcessCancelEventArgs>) theDelegate) + value;
            }
            remove
            {
                Delegate theDelegate = EnsureEvent("Processing");

                events["Processing"] = ((EventHandler<ProcessCancelEventArgs>) theDelegate) - value;
            }
        }
        public event EventHandler<ProcessEventArgs> Processed
        {
            add
            {
                Delegate theDelegate = EnsureEvent("Processed");

                events["Processed"] = ((EventHandler<ProcessEventArgs>)theDelegate) + value;
            }
            remove
            {
                Delegate theDelegate = EnsureEvent("Processed");

                events["Processed"] = ((EventHandler<ProcessEventArgs>)theDelegate) - value;
            }
        }

        private Delegate EnsureEvent(string eventName)
        {
            if (events == null)
            {
                events = new Dictionary<string, Delegate>();
            }

            Delegate theDelegate = null;
            if (!events.TryGetValue(eventName, out theDelegate))
            {
                events.Add(eventName, null);
            }
            return theDelegate;
        }

        public Func<Document, string> LogTextProvider
        {
            get;
            set;
        }

        private readonly List<ActionCheckPair> processes = new List<ActionCheckPair>();

        public void AddProcess(Action<Document> action)
        {
            AddProcess(action, null);
        }

        public void AddProcess(Action<Document> action, Predicate<Document> quickCheck)
        {
            processes.Add(new ActionCheckPair { Action = action, QuickCheck = quickCheck });
        }

        public void Process(Document doc)
        {
            ProcessEventArgs e = new ProcessEventArgs(doc);
            ProcessCancelEventArgs ce = new ProcessCancelEventArgs(doc);
            OnProcessing(ce);
            if (ce.Cancel)
            {
                Console.WriteLine("Proces zostal anulowany.");
                if (LogTextProvider != null)
                {
                    Console.WriteLine(LogTextProvider(doc));
                }
                return;
            }
            foreach (ActionCheckPair process in processes)
            {
                if (process.QuickCheck != null && !process.QuickCheck(doc))
                {
                    Console.WriteLine("Przetwarzanie nie zakonczy sie pomyslnie.");
                    if (LogTextProvider != null)
                    {
                        Console.WriteLine(LogTextProvider(doc));
                    }
                    OnProcessed(e);
                    return;
                }
            }

            foreach (ActionCheckPair process in processes)
            {
                process.Action(doc);
                if (LogTextProvider != null)
                {
                    Console.WriteLine(LogTextProvider(doc));
                }
            }
            OnProcessed(e);
        }

        //Predicate<Document> predicate = delegate (Document doc) { return !doc.Text.Contains("?"); };
        //Predicate<Document> predicate = doc => !doc.Text.Contains("?");

        public static DocumentProcessor Configure()
        {
            DocumentProcessor rc = new DocumentProcessor();
            rc.AddProcess(
                DocumentProcesses.TranslateIntoFrench,
                doc => !doc.Text.Contains("?")
            );
            rc.AddProcess(DocumentProcesses.SpellCheck);
            rc.AddProcess(DocumentProcesses.Repaginate);

            TrademarkFilter trademarkFilter = new TrademarkFilter();
            trademarkFilter.Trademarks.Add("Wiesław");
            trademarkFilter.Trademarks.Add("Zatorski");
            trademarkFilter.Trademarks.Add("milenium");

            rc.AddProcess(trademarkFilter.HighlightTrademarks);

            return rc;
        }

        private void OnProcessing(ProcessCancelEventArgs e)
        {
            Delegate eh = null;
            if (events != null && events.TryGetValue("Processing", out eh))
            {
                EventHandler<ProcessCancelEventArgs> pceh = eh as EventHandler<ProcessCancelEventArgs>;
                if (pceh != null)
                {
                    pceh(this, e);
                }
            }

            //if (Processing != null)
            //{
            //    Processing(this, e);
            //}
        }

        private void OnProcessed(ProcessEventArgs e)
        {
            Delegate eh = null;
            if (events != null && events.TryGetValue("Processed", out eh))
            {
                EventHandler<ProcessEventArgs> pceh = eh as EventHandler<ProcessEventArgs>;
                if (pceh != null)
                {
                    pceh(this, e);
                }
            }

            //if (Processed != null)
            //{
            //    Processed(this, e);
            //}
        }
    }
}
