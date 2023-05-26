using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ver1;

namespace Zadanie3
{
    public class Scanner : BaseDevice, IScanner
    {

        #region <<< Variables >>>

        private int _counter = 0;
        private int _scanCounter = 0;

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int ScanCounter { get {  return _scanCounter; } } //zwraca liczbę zeskanowanych dokumentów,

        private bool IsTurnedOn
        {
            get
            {
                switch (this.GetState())
                {
                    case IDevice.State.on:
                        return true;
                    case IDevice.State.off:
                    default:
                        return false;
                }
            }
        }
        private bool IsTurnedOff => !IsTurnedOn;

        #endregion


        #region <<< BaseDevice >>>

        public new void PowerOn()
        {
            if (IsTurnedOn) 
                return;
            _counter++;
            state = IDevice.State.on;
        }

        public new void PowerOff()
        {
            if (IsTurnedOff) 
                return;
            state = IDevice.State.off;
        }

        #endregion


        #region <<< IScanner >>>

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null; //?
            if(IsTurnedOff)
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
            if(IsTurnedOff)
                return;

            Scan(out document, IDocument.FormatType.JPG);
        }

        #endregion
    }
}