using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GUI_2022_23_01_NFTURS.Logic.GameLogic;

namespace GUI_2022_23_01_NFTURS.Logic
{
    public interface IGameControl
    {
        void Move(Directions direction);
    }
}
