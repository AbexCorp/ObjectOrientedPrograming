using System;
using System.Collections.Generic;
using Zadanie1;
using ver1;


namespace Program
{
    public class Program
    {
        public static void Main()
        {
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
        }
    }
}