using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace VigenereCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _alphabet = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż";
        private char[,] _charTable = new char[35,35];

        private void InitializeCharTable()
        {
            for(int col = 0; col < 35; col++)
            {
                char[] rowChars = new char[35];
                int counter = col;
                for(int i = 0; i < 35; i++)
                {
                    if (counter >= 35)
                        counter -= 35;
                    rowChars[i] = _alphabet[counter];
                    counter++;
                }
                for(int row = 0; row < 35; row++)
                {
                    _charTable[col,row] = rowChars[row];
                }
            }
        }
        

        public MainWindow()
        {
            InitializeComponent();
            InitializeCharTable();
        }

        private void CipherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyText(TextToCipher.Text.ToLower()))
            {
                MessageBox.Show("Text contains invalid character");
                return;
            }
            if (!VerifyText(CipheringKeyWord.Text.ToLower()))
            {
                MessageBox.Show("Key contains invalid character");
                return;
            }
            string text = Cipher(TextToCipher.Text.ToLower(), CipheringKeyWord.Text.ToLower());
            CipheredText.Text = text;
        }

        private void DecipherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifyText(TextToDecipher.Text.ToLower()))
            {
                MessageBox.Show("Text contains invalid character");
                return;
            }
            if (!VerifyText(DecipheringKeyWord.Text.ToLower()))
            {
                MessageBox.Show("Key contains invalid character");
                return;
            }
            string text = Decipher(TextToDecipher.Text.ToLower(), DecipheringKeyWord.Text.ToLower());
            DecipheredText.Text = text;
        }


        private string Decipher(string textToDecipher, string key)
        {
            key = key.Trim().Replace(" ", "");
            string keyedText = KeyText(textToDecipher, key);
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < textToDecipher.Length; i++)
            {
                if (textToDecipher[i] == ' ')
                {
                    sb.Append(' ');
                    continue;
                }
                sb.Append(FindUncodedLetter(textToDecipher[i], keyedText[i]));
            }
            return sb.ToString();
        }
        private string Cipher(string textToCipher, string key)
        {
            key = key.Trim().Replace(" ", "");
            string keyedText = KeyText(textToCipher, key);
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < textToCipher.Length; i++)
            {
                if (textToCipher[i] == ' ')
                {
                    sb.Append(' ');
                    continue;
                }
                sb.Append(FindCodedLetter(textToCipher[i], keyedText[i]));
            }
            return sb.ToString();
        }
        private string KeyText(string text, string key)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    sb.Append(' ');
                    continue;
                }
                sb.Append(key[counter]);

                counter++;
                if (counter >= key.Length)
                    counter = 0;
            }
            return sb.ToString();
        }
        private bool VerifyText(string text)
        {
            for(int i = 0; i < text.Length; i++)
            {
                switch(text[i])
                {
                    case ' ':
                        continue;
                    default:
                        if (!(_alphabet.Contains(text[i])))
                            return false;
                        break;
                }
            }
            return true;
        }



        private int FindLetterValueInAlphabet(char letter)
        {
            int LetterValue = _alphabet.IndexOf(letter);
            return LetterValue;
        }
        private char FindLetterByValueInAlphabet(int value)
        {
            if (value >= _alphabet.Length) {
                MessageBox.Show(value.ToString());
                throw new ArgumentException("Value is outside alphabet"); }
            return _alphabet[value];
        }
        private char FindCodedLetter(char originalLetter, char keywordLetter)
        {
            return _charTable[FindLetterValueInAlphabet(originalLetter),FindLetterValueInAlphabet(keywordLetter)];
        }
        private char FindUncodedLetter(char codedLetter, char keywordLetter)
        {
            char uncodedLetter = ' ';
            for(int col = 0; col < 35; col++)
            {
                if(_charTable[col,0] == keywordLetter)
                {
                    for(int row = 0; row < 35; row++)
                    {
                        if(_charTable[col,row] == codedLetter)
                        {
                            uncodedLetter = FindLetterByValueInAlphabet(row);
                            break;
                        }
                    }
                    break;
                }
            }
            return uncodedLetter;
        }
    }
}
