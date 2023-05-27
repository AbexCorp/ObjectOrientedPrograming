using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ver3;

namespace Zadanie3
{
    public class Fax : BaseDevice, IFax
    {
        private static int s_serialNumber = 0;

        public Fax() 
        {
            FaxNumber = s_serialNumber;
            s_serialNumber++;
        }


        #region <<< Variables >>>

        private int _sendCounter = 0;
        private int _receiveCounter = 0;
        private int _counter = 0;

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //Counter of power-ups
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


        #region <<< IFax >>>

        public int FaxNumber { get; init; }

        public void SendFax(IDocument document, IFax receiver)
        {
            if (IsTurnedOff)
                return;

            _sendCounter++;
            receiver.ReceiveFax(document, this, out bool received);
            if (received)
                Console.WriteLine($"{DateTime.Now.ToString()} Faxed: {document.GetFileName()} To: {receiver.FaxNumber}");
            else
                Console.WriteLine($"{DateTime.Now.ToString()} Failed to fax: {document.GetFileName()} To: {receiver.FaxNumber}");
        }

        public void ReceiveFax(IDocument document, in IFax sender, out bool received)
        {
            received = false;
            if (IsTurnedOff)
                return;
            received = true;

            _receiveCounter++;
            Console.WriteLine($"{DateTime.Now.ToString()} Received fax: {document.GetFileName()} From: {sender.FaxNumber}");
        }

        #endregion

    }
}