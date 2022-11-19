using GUI_2022_23_01_NFTURS.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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
                for (int i = 0; i < model.LevelMatrix.GetLength(0) ; i++)
                {
                    for (int j = 0; j < model.LevelMatrix.GetLength(1); j++)
                    {
                        switch (model.LevelMatrix[i,j])
                        {
                            case GameModel.Bokor:
                                break;
                            case GameModel.Repa:
                                break;
                            case GameModel.Ho:
                                drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Black, 0),
                                    new Rect(i * rectWidth, j * rectHeight, rectWidth, rectHeight));
                                break;
                            case GameModel.Player:
                                break;
                            case GameModel.Hoember:
                                break;
                            case GameModel.Kalap:
                                break;
                            case GameModel.Ajto:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
