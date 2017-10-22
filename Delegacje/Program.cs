using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    class Program
    {
        static void Main(string[] args)
        {
            Document doc1 = new Document
            {
                Author = "Jan Kowalski",
                DocumentDate = new DateTime(2000, 01, 01),
                Text = "Czy przybyłem za wcześnie?"
            };

            Document doc2 = new Document
            {
                Author = "Wiesław Zatorski",
                DocumentDate = new DateTime(2001, 01, 01),
                Text = "Wierzcie mi!Nadchodzi nowe milenium."
            };

            Document doc3 = new Document
            {
                Author = "Jan Kowalski",
                DocumentDate = new DateTime(2002, 01, 01),
                Text = "Inny rok, inny dokument."
            };

            string documentBeingProcessed = "(Brak zestawu dokumentow)";
            int processCount = 0;

            DocumentProcessor processor = DocumentProcessor.Configure();

            processor.LogTextProvider = (doc => {
                processCount += 1;
                return documentBeingProcessed;
            });

            ProductionDeptTool1 tool1 = new ProductionDeptTool1();
            tool1.Subscribe(processor);

            ProductionDeptTool2 tool2 = new ProductionDeptTool2();
            tool2.Subscribe(processor);

            documentBeingProcessed = "(Dokument 1.)";
            processor.Process(doc1);
            Console.WriteLine();
            documentBeingProcessed = "(Dokument 2.)";
            processor.Process(doc2);
            Console.WriteLine();
            documentBeingProcessed = "(Dokument 3.)";
            processor.Process(doc3);

            Console.WriteLine();
            Console.WriteLine("Liczba wykonanych procesow: " + processCount + ".");

            Console.ReadKey();
        }
    }
}

