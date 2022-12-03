using GUI_2022_23_01_NFTURS.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static GUI_2022_23_01_NFTURS.Logic.GameLogic;

namespace GUI_2022_23_01_NFTURS.Renderer
{
    public class Display: FrameworkElement
    {
        IGameModel model;
        Size size;
        public void Resize (Size size)
        {
            this.size = size;
        }
        public void SetupModel(IGameModel model)
        {
            this.model = model;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model !=null && size.Width >50 && size.Height >50)
            {
                double rectWidth =size.Width / model.LevelMatrix.GetLength(1);
                double rectHeight = size.Height / model.LevelMatrix.GetLength(0);

                drawingContext.DrawRectangle(Brushes.LightGray,new Pen(Brushes.Black, 0), new Rect(0, 0, size.Width, size.Height));

                
                for (int i = 0; i < model.LevelMatrix.GetLength(1) ; i++)
                {
                    for (int j = 0; j < model.LevelMatrix.GetLength(0); j++)
                    {
                        ImageBrush brush = new ImageBrush();
                        switch (model.LevelMatrix[j, i])
                        {
                            case GameModel.Latyak:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "snow2.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Repa:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "carrot.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Ho:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "Snow.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Player:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "thief_single.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Hoember:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "snowman.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Kalap:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "hat.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Ajto:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "Basic_Door_Pixel.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            case GameModel.Haz:
                                brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("textures", "treehouse.png"), UriKind.RelativeOrAbsolute)));
                                break;
                            default:
                                break;
                        }
                        drawingContext.DrawRectangle(brush, new Pen(Brushes.Black, 0),
                                    new Rect(i * rectWidth, j * rectHeight, rectWidth, rectHeight));
                                    //new Rect(j * rectHeight, i * rectWidth, rectHeight, rectWidth));
                    }
                }
            }
            ;
        }
    }
}
