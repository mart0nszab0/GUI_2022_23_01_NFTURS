using GUI_2022_23_01_NFTURS.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_NFTURS
{
    public static class LevelInfo
    {

        public static int NUMBER_OF_LEVELS()
        {
            return new GameLogic(1).NUMBER_OF_LEVELS;
        }

        public static bool LevelCompleted(int levelNumber)
        {
            string[] lines = File.ReadAllLines("levelinfo.lvl");
            return int.Parse(lines[levelNumber - 1]) == 1;
        }

        public static void EditCompletion(int levelNumber)
        {
            string[] lines = File.ReadAllLines("levelinfo.lvl");
            lines[levelNumber - 1] = "1";
            File.WriteAllLines("levelinfo.lvl", lines);
        }

        public static void ClearStats()
        {
            string[] zeros = new string[NUMBER_OF_LEVELS()];
            for (int i = 0; i < zeros.Length; i++)
            {
                zeros[i] = "0";
            }

            File.WriteAllLines("levelinfo.lvl", zeros);
        }
    }
}
