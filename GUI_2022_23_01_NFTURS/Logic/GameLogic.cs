using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_NFTURS.Logic
{
    public class GameLogic : IGameModel, IGameControl
    {
        public enum GameModel
        {
            Bokor, Repa, Ho, Player, Hoember, Kalap, Ajto
        }

        public enum Directions
        {
            Up, Down, Left, Right
        }

        //Fieldek és propertyk

        //Ebben lesz mindig eltárolva az aktuálisan megjelenített pálya
        public GameModel[,] LevelMatrix { get; set; }

        private string[] levelFiles;
        public int NUMBER_OF_LEVELS { get; }
        private Queue<string> levels;


        //constructor
        public GameLogic()
        {
            levels = new Queue<string>();
            levelFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Levels"), "*.lvl");
            if (levelFiles.Length % 2 != 0)
            {
                throw new InvalidDataException("Nem paros szamu fajl van a Levels mappaban, minden szinthez ketto fajlra van szukseg");
            }

            NUMBER_OF_LEVELS = levelFiles.Length / 2;
            LoadLevel(1);
        }

        //irányítás 
        public void Move(Directions direction)
        {
            var coords = WhereAmI();
            int i = coords[0];
            int j = coords[1];
            int old_i = i;
            int old_j = j;
            switch (direction)
            {
                case Directions.Up:
                    if (i -1 >= 0)
                    {
                        i--;
                    }
                    break;
                case Directions.Down:
                    if (i + 1 < LevelMatrix.GetLength(0))
                    {
                        i++;
                    }
                    break;
                case Directions.Left:
                    if (j -1 >= 0)
                    {
                        j--;
                    }
                    break;
                case Directions.Right:
                    if (j + 1 < LevelMatrix.GetLength(1))
                    {
                        j++;
                    }
                    break;
                default:
                    break;
            }
            if (LevelMatrix[i,j] == GameModel.Ho)
            {
                LevelMatrix[i, j] = GameModel.Player;
                LevelMatrix[old_i, old_j] = GameModel.Ho;
            }
            else if (LevelMatrix[i,j] == GameModel.Ajto)
            {
                if (levels.Count > 0)
                {
                    LoadLevel(1); // ez itt még javításra szorul
                }
            }
        }
        
        //segédfüggvény, ami megmondja hol vagyunk
        private int[] WhereAmI()
        {
            for (int i = 0; i < LevelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < LevelMatrix.GetLength(1); j++)
                {
                    if (LevelMatrix[i,j] == GameModel.Player)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1,-1};
        }

        //methodok, függvények
        private void LoadLevel(int levelNumber)
        {
            if (levelNumber <= NUMBER_OF_LEVELS) //ha ez nem teljesül, akkor valószínűleg nincsenek szintek berakva a mappába
            {
                string[] lines = File.ReadAllLines(levelFiles[levelNumber - 1]);
                LevelMatrix = new GameModel[int.Parse(lines[0]), int.Parse(lines[1])];

                for (int i = 2; i < int.Parse(lines[0]); i++)
                {
                    for (int j = 0; j < int.Parse(lines[1]); j++)
                    {
                        LevelMatrix[i - 2, j] = ConvertToEnum(lines[i][j]);
                    }
                }
            }

        }

        private GameModel ConvertToEnum(char c)
        {
            switch (c)
            {
                case 'f': return GameModel.Ho;
                case 'a': return GameModel.Ajto;
                case 'b': return GameModel.Bokor;
                case 'h': return GameModel.Hoember;
                case 'r': return GameModel.Repa;
                case 'k': return GameModel.Kalap;
                case 'p': return GameModel.Player;
                default: throw new InvalidDataException("Hibas karakter a palya fajljaban");
            }
        }
    }
}
