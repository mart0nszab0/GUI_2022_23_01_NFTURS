using GUI_2022_23_01_NFTURS.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI_2022_23_01_NFTURS.Controller
{
    public class GameController
    {
        IGameControl control;
        public GameController(IGameControl control)
        {
            this.control = control;
        }

        public void KeyPressed(Key key)
        {
            switch(key)
            {
                case Key.Up:
                    control.Move(GameLogic.Directions.Up);
                    break;
                case Key.Down:
                    control.Move(GameLogic.Directions.Down);
                    break;
                case Key.Left:
                    control.Move(GameLogic.Directions.Left);
                    break;
                case Key.Right:
                    control.Move(GameLogic.Directions.Right);
                    break;
                case Key.W:
                    control.Move(GameLogic.Directions.Up);
                    break;
                case Key.S:
                    control.Move(GameLogic.Directions.Down);
                    break;
                case Key.A:
                    control.Move(GameLogic.Directions.Left);
                    break;
                case Key.D:
                    control.Move(GameLogic.Directions.Right);
                    break;
            }
            
        }
    }
}
