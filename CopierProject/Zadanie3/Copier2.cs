using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    public class Copier : BaseDevice, IDevice, IPrinter, IScanner
    {
        public Copier() 
        {
            _printer.PowerOff();
            _scanner.PowerOff();
        }

        #region <<< Variables >>>

        protected int _counter = 0;
        protected Printer _printer = new Printer();
        protected Scanner _scanner = new Scanner();

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int ScanCounter => _scanner.ScanCounter;
        public int PrintCounter => _printer.PrintCounter;

        protected bool IsTurnedOn
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
        protected bool IsTurnedOff => !IsTurnedOn;

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


        #region <<< IPrinter, IScanner >>>

        public void Print(in IDocument document)
        {
            if (IsTurnedOff)
                return;

            _printer.PowerOn();
            _printer.Print(in document);
            _printer.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (IsTurnedOff)
                return;

            _scanner.PowerOn();
            _scanner.Scan(out document, formatType);
            _scanner.PowerOff();
        }

        public void Scan(out IDocument document)
        {
            document = null; //?
            if(IsTurnedOff)
                return;

            Scan(out document, IDocument.FormatType.JPG);
        }

        public void ScanAndPrint()
        {
            if(IsTurnedOff)
                return;

            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }

        #endregion

    }
}
