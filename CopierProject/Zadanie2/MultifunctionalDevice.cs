using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;
using Zadanie1;

namespace Zadanie2
{
    public class MultifunctionalDevice : Copier, IFax, IPrinter, IScanner
    {
        private static int s_serialNumber = 0;

        public MultifunctionalDevice()
        {
            FaxNumber = s_serialNumber;
            s_serialNumber++;
        }

        #region <<< Variables >>>

        private int _sendCounter = 0;
        private int _receiveCounter = 0;

        #endregion


        #region <<< Properties >>>

        public int SendCounter { get { return _sendCounter; } } //Number of sent documents
        public int ReceiveCounter { get { return _receiveCounter; } } //Number of recieved documents

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


        #region <<< IFax >>>

        public int FaxNumber { get; init; }

        public void SendFax(IDocument document, IFax receiver)
        {
            if(IsTurnedOff)
                return;

            _sendCounter++;
            receiver.ReceiveFax(document, this, out bool received);
            if(received)
                Console.WriteLine($"{DateTime.Now.ToString()} Faxed: {document.GetFileName()} To: {receiver.FaxNumber}");
            else
                Console.WriteLine($"{DateTime.Now.ToString()} Failed to fax: {document.GetFileName()} To: {receiver.FaxNumber}");
        }

        public void ReceiveFax(IDocument document, in IFax sender, out bool received)
        {
            received = false;
            if(IsTurnedOff)
                return;
            received = true;

            _receiveCounter++;
            Console.WriteLine($"{DateTime.Now.ToString()} Received fax: {document.GetFileName()} From: {sender.FaxNumber}");
            Print(document);
        }

        #endregion


        #region <<< MultifunctionalDevice >>>

        public void ScanAndSendFax(IFax receiver)
        {
            if (IsTurnedOff)
                return;

            Scan(out IDocument document, IDocument.FormatType.JPG);
            SendFax(document, receiver);
        }

        #endregion
    }
}
