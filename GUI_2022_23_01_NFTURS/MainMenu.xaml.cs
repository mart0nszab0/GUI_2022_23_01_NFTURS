using GUI_2022_23_01_NFTURS.Logic;
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
using System.Windows.Shapes;
using System.Drawing;

namespace GUI_2022_23_01_NFTURS
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        MainWindow gameplay;
        GameLogic logic = new GameLogic(1);
        public int NUMBER_OF_LEVELS
        {
            get
            {
                return logic.NUMBER_OF_LEVELS;
            }
        }

        public MainMenu()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //LevelSelector();
        }

        private void LevelSelector()
        {
            sp.Children.Clear(); //if there's any content on the right right, it'll be wiped clean

            //margin details (applies for all level buttons)
            Thickness margin = new Thickness();
            margin.Left = 20;
            margin.Right = 20;
            margin.Top = 20;
            margin.Bottom = 10;

            for (int i = 0; i < NUMBER_OF_LEVELS; i++)
            {
                Button button = new Button();

                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Center;
                button.Width = 200;
                button.Height = 35;
                button.Margin = margin;
                button.Click += LevelButtonClick;
                button.Background =Brushes.LightSkyBlue;

                button.Content = $"Level {i + 1}";
                sp.Children.Add(button);
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            LevelSelector();
        }

        private void LevelButtonClick(object sender, RoutedEventArgs e)
        {

            int levelNumber = sp.Children.IndexOf((sender as Button)) + 1;
            gameplay = new MainWindow(levelNumber);
            gameplay.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            sp.Children.Clear(); //if there's any content on the right right, it'll be wiped clean

            //margin details (applies for all level buttons)
            Thickness margin = new Thickness();
            margin.Left = 20;
            margin.Right = 20;
            margin.Top = 20;
            margin.Bottom = 10;

            Thickness borderThickness = new Thickness(1);


            for (int i = 0; i < NUMBER_OF_LEVELS; i++)
            {
                int levelNumber = i + 1;

                Label label = new Label();
                label.BorderThickness = borderThickness;
                label.BorderBrush = Brushes.Black;
                label.Margin = margin;
                label.Content = $"Level {levelNumber}";

                if (LevelInfo.LevelCompleted(levelNumber))
                {
                    label.Background = Brushes.LightGreen;
                }

                sp.Children.Add(label);
            }

            Button button = new Button();
            button.Content = "Clear";
            button.Click += ClearButton_Click;
            sp.Children.Add(button);
        }

        public void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LevelInfo.ClearStats();
            StatsButton_Click(sender, e);
        }
    }
}
