using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegaty
{
    class DocumentProcesses
    {
        public static void SpellCheck(Document doc)
        {
            Console.WriteLine("Sprawdzono pisownie w dokumencie.");
        }

        public static void Repaginate(Document doc)
        {
            Console.WriteLine("Dokonano podzialu dokumentu na strony");
        }

        public static void TranslateIntoFrench(Document doc)
        {
            Console.WriteLine("Document traduit.");
        }
    }
}
