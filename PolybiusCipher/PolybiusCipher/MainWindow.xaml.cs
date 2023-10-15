using System;
using System.Collections.Generic;
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
using System.IO;

namespace PolybiusCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SortedDictionary<char, int> _key = new();
        private SortedDictionary<int, char> _reverseKey = new();
        private string _alphabet = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż";

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists("key.txt"))
            {
                CreateKey(_alphabet);
            }
            else 
            {
                UpdateKey();
            }
        }

        private void SetKeyButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(); //New key
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    object keyToFind = FindName($"Key{i}{j}");
                    TextBox key = keyToFind as TextBox ?? throw new ArgumentNullException("Key not found");
                    if (key.Text == "" || key.Text == null)
                    {
                        MessageBox.Show($"{i+1}x{j+1} field is empty");
                        return;
                    }
                    sb.Append(key.Text.ToLower());
                }
            }
            string newKey = sb.ToString();

            sb = new StringBuilder(); //Duplicate letters
            for(int i = 0; i < _alphabet.Length; i++)
            {
                if(!_alphabet.Contains(newKey[i]))
                {
                    MessageBox.Show("Key doesn't contain all required letters (aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż)");
                    return;
                }
                if (sb.ToString().Contains(newKey[i]))
                {
                    MessageBox.Show($"'{newKey[i]}' is duplicated");
                    return;
                }
            }

            CreateKey(newKey ?? throw new ArgumentException());
        }



        private void CipherButton_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Text == null || Input.Text.Trim() == "")
                return;
            string stringToCypher = Input.Text.ToLower().Trim().Replace(" ", "");
            try
            {
                stringToCypher = CipherStageOne(stringToCypher);
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Invalid character in input field");
                return;
            }
            stringToCypher = CipherStageTwo(stringToCypher);
            Output.Text = stringToCypher;
        }
        private string CipherStageOne(string stringToCypher)
        {
            StringBuilder cipheredText = new StringBuilder();
            for(int i = 0; i < stringToCypher.Length; i++)
            {
                cipheredText.Append(_key[stringToCypher[i]]);
                cipheredText.Append(' ');
            }
            return cipheredText.ToString().Trim();
        }
        private string CipherStageTwo(string stringToCypher)
        {
            string[] characters = stringToCypher.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder cipheredText = ReverseEvenPairs(characters);
            return cipheredText.ToString().Trim();
        }

        private void DecipherButton_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Text == null || Input.Text.Trim() == "")
                return;
            string stringToDecypher = Input.Text;
            stringToDecypher = DecipherStageTwo(stringToDecypher);
            try
            {
                stringToDecypher = DecipherStageOne(stringToDecypher);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Input text is incorrect");
                return;
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Input text is incorrect");
                return;
            }
            Output.Text = stringToDecypher;
        }
        private string DecipherStageOne(string stringToDecypher)
        {
            string[] characters = stringToDecypher.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            StringBuilder decipheredText = new StringBuilder();
            foreach(string character in characters)
            {
                if (character.Length != 2)
                    throw new ArgumentException();
                decipheredText.Append(_reverseKey[int.Parse(character)]);
            }
            return decipheredText.ToString();
        }
        private string DecipherStageTwo(string stringToDecypher)
        {
            string[] characters = stringToDecypher.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder decipheredText = ReverseEvenPairs(characters);
            return decipheredText.ToString().Trim();
        }
        private StringBuilder ReverseEvenPairs(string[] characters)
        {
            StringBuilder stringWithReversedPairs = new StringBuilder();
            foreach(string character in characters)
            {
                int char1 = int.Parse(character[0].ToString());
                int char2 = int.Parse(character[1].ToString());
                if (char1 == char2 || ((char1 + char2) % 2 != 0))
                {
                    stringWithReversedPairs.Append(character);
                    stringWithReversedPairs.Append(' ');
                    continue;
                }
                else if((char1 + char2) % 2 == 0)
                {
                    stringWithReversedPairs.Append(char2);
                    stringWithReversedPairs.Append(char1);
                    stringWithReversedPairs.Append(' ');
                    continue;
                }
            }
            return stringWithReversedPairs;
        }



        private void CreateKey(string key)
        {
            if (key.Length != _alphabet.Length)
                throw new ArgumentException("Wrong key");

            File.WriteAllText("key.txt", "");
            if (File.Exists("key.txt"))
            {
                using (StreamWriter sw = File.CreateText("key.txt"))
                {
                    int counter = 0;
                    for(int i = 0; i < 5; i++)
                    {
                        for(int j = 0; j < 7; j++)
                        {
                            sw.Write(key[counter]);
                            counter++;
                        }
                        sw.WriteLine();
                    }
                }
            }
            UpdateKey();
        }
        private void UpdateKey()
        {
            string keyFile = File.ReadAllText("key.txt");
            string[] keyFileLines = keyFile.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            keyFile = "";
            foreach (string line in keyFileLines)
            {
                keyFile += line;
            }
            if(keyFile.Length != 35)
            {
                MessageBox.Show("Saved key is not correct, create a new one");
                return;
            }
            _key.Clear();
            _reverseKey.Clear();
            int counter = 0;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    try
                    {
                        _key.Add(keyFile[counter], int.Parse($"{i+1}{j+1}")); //Adds +1 to fix count from zero. Fix if causes issues
                        _reverseKey.Add(int.Parse($"{i+1}{j+1}"), keyFile[counter]);
                    }
                    catch(ArgumentException)
                    {
                        MessageBox.Show("Saved key is not correct, create a new one");
                        File.WriteAllText("key.txt", "");
                        return;
                    }

                    object keyToFind = FindName($"Key{i}{j}");
                    TextBox key = keyToFind as TextBox ?? throw new ArgumentNullException("Key not found");
                    key.Text = keyFile[counter].ToString();

                    counter++;
                }
            }
        }
    }
}
