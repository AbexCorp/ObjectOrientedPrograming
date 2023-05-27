using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ver3;

namespace Zadanie3
{
    public class MultidimensionalDevice : Copier, IDevice, IPrinter, IScanner, IFax
    {
        public MultidimensionalDevice() 
        {
            _printer.PowerOff();
            _scanner.PowerOff();
            _fax.PowerOff();
            FaxNumber = _fax.FaxNumber;
        }

        private Fax _fax = new Fax();


        #region <<< Properties >>>

        public int FaxNumber { get; init; }
        public int SendCounter => _fax.SendCounter;
        public int ReceiveCounter => _fax.ReceiveCounter;

        #endregion


        #region <<< IFax >>>

        public void SendFax(IDocument document, IFax receiver)
        {
            if (IsTurnedOff)
                return;

            _fax.PowerOn();
            _fax.SendFax(document, receiver);
            _fax.PowerOff();
        }

        public void ReceiveFax(IDocument document, in IFax sender, out bool received)
        {
            received = false;
            if (IsTurnedOff)
                return;

            _fax.PowerOn();
            _fax.ReceiveFax(document, sender, out received);
            _fax.PowerOff();
            _printer.PowerOn();
            _printer.Print(document);
            _printer.PowerOff();
        }

        public void ScanAndSendFax(IFax receiver)
        {
            if (IsTurnedOff)
                return;

            _scanner.PowerOn();
            _fax.PowerOn();
            Scan(out IDocument document, IDocument.FormatType.JPG);
            SendFax(document, receiver);
            _scanner.PowerOff();
            _fax.PowerOff();
        }

        #endregion
    }
}
