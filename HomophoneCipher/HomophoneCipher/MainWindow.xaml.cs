using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace HomophoneCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[] letters = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż".ToCharArray();
        //One homophone per ~1% (https://pl.wikipedia.org/wiki/Alfabet_polski#Cz%C4%99sto%C5%9B%C4%87_wyst%C4%99powania_liter)
        private int[] homophones = { 9, 1, 1, 4, 1, 3, 8, 1, 1, 1, 1, 8, 2, 3, 2, 2, 3, 6, 1, 8, 1, 3, 1, 5, 4, 1, 4, 2, 1, 5, 1, 4, 6, 1, 1 };
        private int numberOfHomophones = 0;
        private static Random rng = new();

        private Dictionary<char, List<string>> CipherHomophones = new();
        private Dictionary<string, char> DecipheringHomophones = new();


        public static void Shuffle<T>(IList<T> list, int seed)
        {
            var rng = new Random(seed);
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private List<string> CreateHomophoneList(List<string> listOfHomophones)
        {
            for(int i = 0; i < homophones.Length; i++)
            {
                numberOfHomophones += homophones[i];
            }
            for(int i = 0; i < numberOfHomophones; i++)
            {
                listOfHomophones.Add(i.ToString().PadLeft(3,'0'));
            }
            Shuffle(listOfHomophones, 10);
            return listOfHomophones;
        }
        private void AssignHomophones(List<string> listOfHomophones)
        {
            int counter = 0;
            for(int i = 0; i < homophones.Length; i++)
            {
                List<string> list = new();
                for(int j = 0; j < homophones[i]; j++)
                {
                    list.Add(listOfHomophones[counter]);
                    DecipheringHomophones.Add(listOfHomophones[counter],letters[i]);
                    counter++;
                }
                CipherHomophones.Add(letters[i], list);
            }
        }
        private void InitializeHomophones()
        {
            List<string> listOfHomophones = new List<string>();
            CreateHomophoneList(listOfHomophones);
            AssignHomophones(listOfHomophones);
        }



        public MainWindow()
        {
            InitializeHomophones();
            InitializeComponent();
        }

        private void CipherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CipheredText.Text = Cipher(TextToCipher.Text);
            }
            catch(KeyNotFoundException)
            {
                MessageBox.Show("Invalid characters");
            }
        }

        private void DecipherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DecipheredText.Text = Decipher(TextToDecipher.Text);
            }
            catch(KeyNotFoundException)
            {
                MessageBox.Show("Invalid characters");
            }
        }


        private string Cipher(string textToCipher)
        {
            textToCipher = textToCipher.ToLower().Replace(" ", "");
            if(textToCipher is null ||  textToCipher.Length == 0 )
                return "";

            StringBuilder sb = new();
            foreach (var x in textToCipher)
            {
                sb.Append(CipherCharacter(x));
                sb.Append(' ');
            }
            return sb.ToString();
        }
        private string CipherCharacter(char character)
        {
            return CipherHomophones[character][rng.Next(CipherHomophones[character].Count)];
        }

        private string Decipher(string textToDecipher)
        {
            textToDecipher = textToDecipher.Trim();
            if(textToDecipher is null || textToDecipher.Length == 0)
                return "";

            string[] characters = textToDecipher.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            foreach (var x in characters)
            {
                sb.Append(DecipherCharacter(x));
            }
            return sb.ToString();
        }
        private char DecipherCharacter(string homophone)
        {
            return DecipheringHomophones[homophone];
        }
    }
}
