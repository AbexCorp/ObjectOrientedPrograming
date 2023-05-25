using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Zadanie1;
using ver1;

using Zadanie2;


namespace Program
{
    public class Program
    {
        public static void Main()
        {
            /* Zadanie1
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine( xerox.Counter );
            System.Console.WriteLine( xerox.PrintCounter );
            System.Console.WriteLine( xerox.ScanCounter );
            */

            /* Zadanie2
            code to test multifunctional device here
            */
            Fax f1 = new Fax();
            Fax f2 = new Fax();

            TextDocument t = new TextDocument("as.txt");

            f1.PowerOn();
            f2.PowerOn();

            f1.Fax
        }
    }
}