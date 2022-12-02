using GUI_2022_23_01_NFTURS.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_NFTURS
{
    public static class LevelInfo
    {
        static int NUMBER_OF_LEVELS()
        {
            return new GameLogic(1).NUMBER_OF_LEVELS;
        }
    }
}
