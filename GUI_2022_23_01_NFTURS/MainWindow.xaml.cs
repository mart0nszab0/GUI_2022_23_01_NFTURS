using GUI_2022_23_01_NFTURS.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_2022_23_01_NFTURS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController controller;
        

        public MainWindow(int levelNumber)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            GameLogic logic = new GameLogic(levelNumber);
            display.SetupModel(logic);
            controller = new GameController(logic);
            logic.LevelOver = CloseWindow;
            logic.RedrawNeeded += Rajzol;

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.3);
            dt.Tick += (sender, args) =>
            {
                logic.SnowmanStep();
            };
            dt.Start();
        }

        private void CloseWindow()
        {
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            controller.KeyPressed(e.Key);
            display.InvalidateVisual();
        }

        private void Rajzol(object? sender, EventArgs e)
        {
            display.InvalidateVisual();
        }
    }
}
