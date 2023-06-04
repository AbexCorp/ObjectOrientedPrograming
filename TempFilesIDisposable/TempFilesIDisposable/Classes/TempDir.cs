using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TempElementsLib.Interfaces;

namespace TempFilesIDisposable.Classes
{
    public class TempDir : ITempDir
    {
        //public TempDir(){} //+1 overload
        public string DirPath => throw new NotImplementedException();

        public bool IsEmpty => throw new NotImplementedException();

        public bool IsDestroyed => throw new NotImplementedException();

        public void Dispose() //+1 overload
        {
            throw new NotImplementedException();
        }

        public void Empty()
        {
            throw new NotImplementedException();
        }
    }
}
