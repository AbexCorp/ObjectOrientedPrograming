using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ver3;

namespace Zadanie3
{
    public class Printer : BaseDevice, IPrinter
    {

        #region <<< Variables >>>

        private int _counter = 0;
        private int _printCounter = 0;

        #endregion


        #region <<< Properties >>>

        new public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int PrintCounter { get { return _printCounter; } } //zwraca aktualną liczbę wydrukowanych dokumentów,

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


        #region <<< IPrinter >>>

        public void Print(in IDocument document)
        {
            if(IsTurnedOff)
                return;

            Console.WriteLine($"{DateTime.Now.ToString()} Print: {document.GetFileName()}");
            _printCounter++;
        }

        #endregion
    }
}