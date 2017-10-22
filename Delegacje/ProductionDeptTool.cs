using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    class ProductionDeptTool1
    {
        public void Subscribe(DocumentProcessor processor)
        {
            processor.Processing += processor_Processing;
            processor.Processed += processor_Processed;
        }

        public void Unsubscribe(DocumentProcessor processor)
        {
            processor.Processing -= processor_Processing;
            processor.Processed -= processor_Processed;
        }

        void processor_Processing(object sender, ProcessCancelEventArgs e)
        {
            Console.WriteLine("Narzedzie 1. - zarejestrowano przetwarzanie, wykonanie nie zostalo anulowane");
        }

        void processor_Processed(object sender, EventArgs e)
        {
            Console.WriteLine("Narzędzie 1. - zarejestrowano zakończenie przetwarzania.");
        }
    }

    class ProductionDeptTool2
    {
        public void Subscribe(DocumentProcessor processor)
        {
            processor.Processing +=
                (sender, e) =>
                {
                    Console.WriteLine("Narzedzie 2. - zarejestrowano przetwarzanie i anulowano je.");
                    if (e.Document.Text.Contains("dokument"))
                    {
                        e.Cancel = true;
                    }
                };
            processor.Processed +=
                (sender, e) => Console.WriteLine("Narzedzie 2. - zarejestrowano zakonczenie przetwarzania");
        }
    }
}
