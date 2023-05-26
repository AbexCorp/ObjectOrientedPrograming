using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;

namespace Zadanie3
{
    internal class Copier : BaseDevice, IPrinter, IScanner
    {
        public Copier() 
        {
            _printer.PowerOff();
            _scanner.PowerOff();
        }

        #region <<< Variables >>>

        private int _counter = 0;
        private Printer _printer = new Printer();
        private Scanner _scanner = new Scanner();

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int ScanCounter => _scanner.ScanCounter;
        public int PrintCounter => _printer.PrintCounter;

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


        #region <<< IPrinter, IScanner >>>

        public void Print(in IDocument document)
        {
            if (IsTurnedOff)
                return;
            throw new NotImplementedException();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null;
            if (IsTurnedOff)
                return;
            throw new NotImplementedException();
        }

        #endregion

        #region <<< B >>>
        #endregion
    }
}
