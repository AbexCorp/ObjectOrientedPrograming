using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TempElementsLib.Interfaces;

namespace TempFilesIDisposable.Classes
{
    public class TempTextFile : ITempFile
    {
        public string FilePath => throw new NotImplementedException();

        public bool IsDestroyed => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
