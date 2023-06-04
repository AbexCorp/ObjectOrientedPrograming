using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TempElementsLib.Interfaces;

namespace TempFilesIDisposable.Classes
{
    //Przechowuje informacje o utworzonym zasobie zewnętrznym
    public class TempFile : ITempFile
    {
        /*
        Konstruktor inicjuje (tworzy) wymagany zasób zewnętrzny (plik/katalog).

        Konstruktor bezargumentowy w klasach implementujących ITempFile wykorzystuje mechanizmy obsługi plików tymczasowych
        dostarczonych w bibliotekach C# (statyczna klasa System.IO.Path i metody Path.GetTempFileName, Path.GetTempPath,
        Path.GetRandomFileName i inne zlokalizowane w tej klasie).

        Jeśli nie podano ścieżki, plik tymczasowy tworzony jest folderze domyślnym dla plików tymczasowych
        dla profilu użytkownika (np. w Windows w C:\Users\USER\AppData\Local\Temp\tmp35C7.tmp).

        Konstruktor z argumentem będącym nazwą pliku (wraz z pełną ścieżką) tworzy we wskazanej lokalizacji plik tymczasowy.

        W przypadku podania - w konstruktorach wymagających parametru - niepoprawnej ścieżki, zgłaszany jest stosowny wyjątek.

        Przy tworzeniu obiektu reprezentującego plik/katalog tymczasowy nie został zgłoszony wyjątek po jego fizycznym 
        utworzeniu - zasób zewnętrzny (niezarządzalny) zostałby utworzony, a obiekt nie będzie istniał - może spowodować to 
        wycieki pamięci lub destabilizację systemu operacyjnego (np. nie zamknięty plik).

        Przy używaniu obiektu reprezentującego plik/katalog tymczasowy nie likwidować dostępu
        do zasobu - spowoduje to zgłoszenie wyjątku ObjectDisposedException

        Po zakończeniu korzystania z pliku/katalogu tymczasowego został on skutecznie i bezwarunkowo usunięty.

        Należy obsłużyć ewentualnie pojawiające się wyjątki z System.IO https://docs.microsoft.com/en-us/dotnet/api/system.io.ioexception 
        (np. w sytuacji tworzenia katalogu podając jego nazwę, a katalog taki we wskazanej lokalizacji już istnieje)
        */

        public string FilePath => throw new NotImplementedException();

        public bool IsDestroyed => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
