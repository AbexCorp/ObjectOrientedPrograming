using System;
using System.Collections.Generic;
using ver1;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        #region <<< Variables >>>

        private int _counter = 0;
        private int _printCounter = 0;
        private int _scanCounter = 0;

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int PrintCounter { get { return _printCounter; } } //zwraca aktualną liczbę wydrukowanych dokumentów,
        public int ScanCounter { get {  return _scanCounter; } } //zwraca liczbę zeskanowanych dokumentów,

        #endregion


        #region <<< BaseDevice >>>
        public new void PowerOn()
        {
            if (state == IDevice.State.on) 
                return;
            _counter = _counter + 1;
            state = IDevice.State.on;
        }

        public new void PowerOff()
        {
            if (state == IDevice.State.off) 
                return;
            state = IDevice.State.off;
        }

        #endregion


        #region <<< IPrinter IScaner >>>
        public void Print(in IDocument document)
        {
            if(state == IDevice.State.off) 
                return;

            Console.WriteLine($"{DateTime.Now.ToString()} Print: {document.GetFileName()}");
            _printCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null; //?
            if (state == IDevice.State.off)
                return;

            string filename = $"{formatType.ToString().ToUpper()}Scan{ScanCounter}.{formatType.ToString().ToLower()}"; //Not perfect
            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    document = new TextDocument(filename);
                    break;
                case IDocument.FormatType.PDF:
                    document = new PDFDocument(filename);
                    break;
                case IDocument.FormatType.JPG:
                    document = new ImageDocument(filename);
                    break;
                default:
                    return;
            }

            Console.WriteLine($"{DateTime.Now.ToString()} Scan: {filename}");
            _scanCounter++;
            return;
        }
        public void Scan(out IDocument document)
        {
            document = null; //?
            if (state == IDevice.State.off)
                return;

            Scan(out document, IDocument.FormatType.JPG);
        }

        public void ScanAndPrint()
        {
            if(state == IDevice.State.off) 
                return;

            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }

        #endregion
    }
}