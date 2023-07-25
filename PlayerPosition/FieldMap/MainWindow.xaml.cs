using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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

using PlayerPositionLib;

namespace FieldMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LocationTracker field = new(500,500);
        private static Random rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //Ellipse ellipse = new Ellipse();
            //ellipse.Fill = new SolidColorBrush(Colors.Aqua);
            //ellipse.Height = 20;
            //ellipse.Width = 20;
            //Canvas.SetTop(ellipse, 250);
            //Canvas.SetLeft(ellipse, 250);
            //playingFieldCanvas.Children.Add(ellipse);

            //Create shape
            //Rectangle rectangle = new Rectangle();
            //rectangle.Width = 100;
            //rectangle.Height = 100;
            //rectangle.Fill = new SolidColorBrush(Colors.Beige);

            //Position shape and add
            //Canvas.SetLeft(rectangle, 200);
            //Canvas.SetTop(rectangle, 200);
            //playingFieldCanvas.Children.Add(rectangle);

            //Might not be needed
            //playingFieldCanvas.Measure(new Size(playingFieldCanvas.Width, playingFieldCanvas.Height));
            //playingFieldCanvas.Arrange(new Rect(new Size(playingFieldCanvas.Width, playingFieldCanvas.Height)));

            //Clear
            //playingFieldCanvas.Children.RemoveRange(0, playingFieldCanvas.Children.Count);
        }

        private void startSimulationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(numberOfPlayersTextBox.Text, out int playerNumber) || playerNumber < 1 || playerNumber > 24)
            {
                MessageBox.Show("Not a number between 1-24");
                return;
            }
            for(int i = 0; i < playerNumber; i++)
            {
                Player p1 = field.AddPlayer(250, 250);
                selectPlayerListBox.Items.Add(p1);
            }
            var task = Simulation(playerNumber);
        }

        private async Task Simulation(int playerNumber)
        {
            var tasks = new List<Task>();
            List<Player> playerList = field.PlayerList.Values.ToList();

            //<*>
            int numberOfSomePlayers = playerNumber / 2 == 0 ? 1 : playerNumber / 2;
            var fewPlayers = playerList;

            for(int i = 0; i < 1; i++)
            {
                fewPlayers = playerList.OrderBy(a => rng.Next()).Take(numberOfSomePlayers).ToList();
                foreach(var player in fewPlayers)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate
                        {
                            player.Move();
                            Ellipse ellipse = new Ellipse();
                            ellipse.Fill = new SolidColorBrush(Colors.Aqua);
                            ellipse.Height = 3;
                            ellipse.Width = 3;
                            Canvas.SetTop(ellipse, player.CurrentLocation.Y-1);
                            Canvas.SetLeft(ellipse, player.CurrentLocation.X-1);
                            playingFieldCanvas.Children.Add(ellipse);
                            System.Threading.Thread.Sleep(1000);
                        });
                    }));
                }
            }

            //await Task.WhenAll(tasks);

            //await Task.WhenAny(tasks);


            //playerStatsTextBlock.Text += $"{field.PlayerList[field.GetIdOfLongestDistanceCoveredPlayer()]} " +
            //    $"{field.GetPlayerDistanceCoveredById(field.GetIdOfLongestDistanceCoveredPlayer())}";
        }
    }
}
