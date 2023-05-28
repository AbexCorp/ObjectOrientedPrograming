using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ver4;

namespace Zadanie4
{
    public class Copier : IDevice, IPrinter, IScanner
    {

        #region <<< Variables >>>

        private int _counter = 0;
        private int _printCounter = 0;
        private int _scanCounter = 0;

        private int _currentPrintCount = 0;
        private int _currentScanCount = 0;

        
        private IDevice.State _printerState = IDevice.State.off;
        private IDevice.State _scannerState = IDevice.State.off;

        #endregion


        #region <<< Properties >>>

        public int Counter { get { return _counter; } } //zwraca liczbę uruchomień kserokopiarki
        public int PrintCounter { get { return _printCounter; } } //zwraca aktualną liczbę wydrukowanych dokumentów,
        public int ScanCounter { get {  return _scanCounter; } } //zwraca liczbę zeskanowanych dokumentów,

        private IDevice.State State
        {
            get
            {
                if(_printerState == IDevice.State.off && _scannerState == IDevice.State.off)
                    return IDevice.State.off;
                if(_printerState == IDevice.State.standby && _scannerState == IDevice.State.standby)
                    return IDevice.State.standby;
                return IDevice.State.on;
            }
        }

        #endregion


        #region <<< IDevice >>>

        public void PowerOn() => SetState(IDevice.State.on);
        public void PowerOff() => SetState(IDevice.State.off);
        public void StandbyOn() => SetState(IDevice.State.standby);
        public void StandbyOff() => SetState(IDevice.State.on);


        public IDevice.State GetState()
        {
            return State;
        }
        public void SetState(IDevice.State state)
        {
            _printerState = state;
            _scannerState = state;
        }

        private void SetPrinterState(IDevice.State state)
        {
            _currentPrintCount = 0;
            _printerState = state;
        }
        private void SetScannerState(IDevice.State state)
        {
            _currentScanCount = 0;
            _scannerState = state;
        }

        private IDevice.State GetPrinterState()
        {
            return _printerState;
        }
        private IDevice.State GetScannerState()
        {
            return _scannerState;
        }

        private void Standby()
        {
            Console.WriteLine("Standby...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Continuing");
        }

        #endregion


        #region <<< IPrinter IScaner >>>

        //Check state of printer and disable scanner before using printer
        private void OperatePrinter(out bool quit)
        {
            quit = false;
            if (GetPrinterState() == IDevice.State.off)
            {
                quit = true;
                return;
            }

            if(_currentPrintCount >= 3)
            {
                SetPrinterState(IDevice.State.standby);
                Standby();
                SetPrinterState(IDevice.State.on);
            }
            if(GetPrinterState() == IDevice.State.standby)
            {
                SetPrinterState(IDevice.State.on);
            }


            if(GetScannerState() == IDevice.State.on)
                SetScannerState(IDevice.State.standby);
        }
        //Check state of scanner and disable printer before using scanner
        private void OperateScanner(out bool quit)
        {
            quit = false;
            if(GetScannerState() == IDevice.State.off) 
            {
                quit = true;
                return;
            }

            if(_currentScanCount >= 2)
            {
                SetScannerState(IDevice.State.standby);
                Standby();
                SetScannerState(IDevice.State.on);
            }
            if(GetScannerState() == IDevice.State.standby)
            {
                SetScannerState(IDevice.State.on);
            }


            if(GetPrinterState() == IDevice.State.on)
                SetPrinterState(IDevice.State.standby);
        }


        public void Print(in IDocument document)
        {
            OperatePrinter(out bool quit);
            if (quit)
                return;

            Console.WriteLine($"{DateTime.Now.ToString()} Print: {document.GetFileName()}");
            _printCounter++;
            _currentPrintCount++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            document = null; //?
            OperateScanner(out bool quit);
            if (quit)
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
            _currentScanCount++;
            return;
        }
        public void Scan(out IDocument document)
        {
            document = null; //?
            OperateScanner(out bool quit);
            if (quit)
                return;

            Scan(out document, IDocument.FormatType.JPG);
        }

        public void ScanAndPrint()
        {
            if(GetState() == IDevice.State.off) 
                return;

            Scan(out IDocument document, IDocument.FormatType.JPG);
            Print(document);
        }

        #endregion
    }
}