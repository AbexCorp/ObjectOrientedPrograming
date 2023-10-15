using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaesarsCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SortedDictionary<char, int> _alphabet = new();
        public MainWindow()
        {
            InitializeComponent();
            char[] letters = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż".ToCharArray();
            for(int i = 0; i < letters.Length; i++)
            {
                _alphabet.Add(letters[i], i);
            }
        }

        private void CipherSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CipheringKey.Text = $"Ciphering key: {CipherSlider.Value}";
        }

        private void CipherButton_Click(object sender, RoutedEventArgs e)
        {
            string stringToCypher = TextToCipher.Text;
            int cypheringKey = (int)CipherSlider.Value;
            if (stringToCypher is null || stringToCypher.Length == 0 || stringToCypher.Trim() == "")
                return;

            StringBuilder cypheredText = new StringBuilder();
            stringToCypher = stringToCypher.ToLower().Trim().Replace(" ","");
            for(int i = 0; i < stringToCypher.Length; i++)
            {
                int newLetter = _alphabet[stringToCypher[i]] + cypheringKey > (_alphabet.Count - 1) ?
                    (_alphabet[stringToCypher[i]] + cypheringKey - _alphabet.Count) : _alphabet[stringToCypher[i]] + cypheringKey;
                //cypheredText.Append($"[{_alphabet.FirstOrDefault(x => x.Value == newLetter).Key}-{_alphabet[stringToCypher[i]]}-{newLetter}]");
                cypheredText.Append(_alphabet.FirstOrDefault(x => x.Value == newLetter).Key);
            }
            CipheredText.Text = cypheredText.ToString();
        }


        private void DecipherSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DecipheringKey.Text = $"Deciphering key: {DecipherSlider.Value}";
        }

        private void DecipherButton_Click(object sender, RoutedEventArgs e)
        {
            string stringToDecypher = TextToDecipher.Text;
            int decypheringKey = (int)DecipherSlider.Value;
            if (stringToDecypher is null || stringToDecypher.Length == 0 || stringToDecypher.Trim() == "")
                return;

            StringBuilder decypheredText = new StringBuilder();
            stringToDecypher = stringToDecypher.ToLower().Trim().Replace(" ","");
            for(int i = 0; i < stringToDecypher.Length; i++)
            {
                int newLetter = _alphabet[stringToDecypher[i]] - decypheringKey < 0 ?
                    (_alphabet[stringToDecypher[i]] - decypheringKey + _alphabet.Count) : _alphabet[stringToDecypher[i]] - decypheringKey;
                decypheredText.Append(_alphabet.FirstOrDefault(x => x.Value == newLetter).Key);
            }
            DecipheredText.Text = decypheredText.ToString();
        }
    }
}
