using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;

namespace Zadanie2
{
    public interface IFax : IDevice
    {
        /// <summary>
        /// Dokument jest wysyłany do podanego Faxu, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        /// <param name="receiver">obiekt typu IFax, różny od `null`</param>
        public void Fax(IDocument document, IFax receiver);

        public void Receive(IDocument document, in IFax sender, out bool received);

        public int FaxNumber { get; init; }
    }
}
