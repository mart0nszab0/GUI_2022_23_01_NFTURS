﻿using GUI_2022_23_01_NFTURS.Logic;
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

namespace GUI_2022_23_01_NFTURS
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        MainWindow gameplay;
        public int NUMBER_OF_LEVELS 
        {
            get
            {
                return new GameLogic().NUMBER_OF_LEVELS;
            }
        }

        public MainMenu()
        {
            InitializeComponent();
            //LevelSelector();
        }

        private void LevelSelector()
        {
            if (sp.Children.Count != NUMBER_OF_LEVELS) //we only generate the level selector buttons if they're not generated already
            {
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
                    

                    button.Content = $"Level {i + 1}";
                    sp.Children.Add(button);
                }
            } 
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            LevelSelector();
            //gameplay = new MainWindow();
            //gameplay.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
