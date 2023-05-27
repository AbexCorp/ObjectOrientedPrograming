using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using Zadanie1;
//using ver1;

//using Zadanie2; //Uses ver1

using Zadanie3;
using ver3;


namespace Program
{
    public class Program
    {
        public static void Main()
        {
            //Zadanie1
            /*
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


            //Zadanie2
            /*
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();

            TextDocument t = new TextDocument("testFile.txt");

            f1.PowerOn();
            f2.PowerOn();

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);

            f1.SendFax(t, f2);

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);

            f2.PowerOff();
            f1.SendFax(t, f2);

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);
            */


            //Zadanie3
            /*
            Console.WriteLine("Copier");
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


            Console.WriteLine("MultidimensionalDevice");
            MultidimensionalDevice f1 = new MultidimensionalDevice();
            MultidimensionalDevice f2 = new MultidimensionalDevice();

            TextDocument t = new TextDocument("testFile.txt");

            f1.PowerOn();
            f2.PowerOn();

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);

            f1.SendFax(t, f2);

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);

            f2.PowerOff();
            f1.SendFax(t, f2);

            Console.WriteLine(f1.SendCounter + " " + f1.ReceiveCounter);
            Console.WriteLine(f2.SendCounter + " " + f2.ReceiveCounter);
            */

        }
    }
}