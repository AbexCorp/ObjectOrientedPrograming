using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using System;
using System.IO;
using Zadanie1;
using Zadanie2;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using ver1UnitTests;

namespace Zadanie2_UnitTests
{
    [TestClass]
    public class MultifunctionalDeviceUnitTest
    {
        [TestMethod]
        public void FaxNumber_Test()
        {
            MultifunctionalDevice f0 = new MultifunctionalDevice();
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            Assert.AreEqual(f1.FaxNumber, f0.FaxNumber+1);
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            Assert.AreEqual(f2.FaxNumber, f0.FaxNumber+2);
        }

        [TestMethod]
        public void SendCounter_Test()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            TextDocument d = new TextDocument("test.txt");
            f1.PowerOn();
            f2.PowerOn();

            f1.SendFax(d, f2); //1
            f1.SendFax(d, f2); //2
            f2.PowerOff();
            f1.SendFax(d, f2); //3
            f2.PowerOn();
            f1.SendFax(d, f2); //4
            f1.PowerOff();
            f1.SendFax(d, f2); //f
            f1.PowerOn();
            f1.SendFax(d, f2); //5

            Assert.AreEqual(f1.SendCounter, 5);
        }

        [TestMethod]
        public void ReceiveCounter_Test()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            TextDocument d = new TextDocument("test.txt");
            f1.PowerOn();
            f2.PowerOn();

            f1.SendFax(d, f2); //1
            f1.SendFax(d, f2); //2
            f2.PowerOff();
            f1.SendFax(d, f2); //f
            f2.PowerOn();
            f1.SendFax(d, f2); //3
            f1.PowerOff();
            f1.SendFax(d, f2); //f
            f1.PowerOn();
            f1.SendFax(d, f2); //4

            Assert.AreEqual(f2.ReceiveCounter, 4);
        }


        #region <<< SendFax >>>

        [TestMethod]
        public void SendFax_Sender_PowerOff()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            TextDocument d = new TextDocument("test.txt");
            f1.PowerOff();
            f2.PowerOff();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.SendFax(d, f2);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Faxed:"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Received fax:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 0);
            Assert.AreEqual(f2.ReceiveCounter, 0);
        }

        [TestMethod]
        public void SendFax_Receiver_PowerOff()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            TextDocument d = new TextDocument("test.txt");
            f1.PowerOn();
            f2.PowerOff();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.SendFax(d, f2);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Failed to fax:"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Received fax:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 1);
            Assert.AreEqual(f2.ReceiveCounter, 0);
        }

        [TestMethod]
        public void SendFax_PowerOn()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            TextDocument d = new TextDocument("test.txt");
            f1.PowerOn();
            f2.PowerOn();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.SendFax(d, f2);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Faxed:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Received fax:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 1);
            Assert.AreEqual(f2.ReceiveCounter, 1);
        }

        #endregion


        #region <<< ScanAndSnedFax >>>

        [TestMethod]
        public void ScanAndSendFax_Sender_PowerOff()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            f1.PowerOff();
            f2.PowerOff();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.ScanAndSendFax(f2);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan:"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Faxed:"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Received fax:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 0);
            Assert.AreEqual(f2.ReceiveCounter, 0);
        }

        [TestMethod]
        public void ScanAndSendFax_Receiver_PowerOff()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            f1.PowerOn();
            f2.PowerOff();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.ScanAndSendFax(f2);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Failed to fax:"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Received fax:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 1);
            Assert.AreEqual(f2.ReceiveCounter, 0);
        }

        [TestMethod]
        public void ScanAndSendFax_PowerOn()
        {
            MultifunctionalDevice f1 = new MultifunctionalDevice();
            MultifunctionalDevice f2 = new MultifunctionalDevice();
            f1.PowerOn();
            f2.PowerOn();


            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using( var consoleOutput = new ConsoleRedirectionToStringWriter() )
            {
                f1.ScanAndSendFax(f2);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Faxed:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Received fax:"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print:"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);


            Assert.AreEqual(f1.SendCounter, 1);
            Assert.AreEqual(f2.ReceiveCounter, 1);
        }

        #endregion
    }
}