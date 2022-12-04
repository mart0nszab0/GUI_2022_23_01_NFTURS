using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_2022_23_01_NFTURS.Logic
{
    public class GameLogic : IGameModel, IGameControl
    {
        public enum GameModel
        {
            Latyak, Repa, Ho, Player, Hoember, Kalap, Ajto, Haz, Fa
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
        private int levelNumber;
        private int health;

        public Task SnowmanMovement { get; set; }
        public event EventHandler RedrawNeeded;
        public int CurrentLevel { get; private set; }
        private bool HasWon;

        //constructor
        public GameLogic(int levelNumber)
        {
            HasWon = false;
            levels = new Queue<string>();
            levelFiles = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Levels"), "*.lvl");
            //if (levelFiles.Length % 2 != 0)
            //{
            //    throw new InvalidDataException("Nem paros szamu fajl van a Levels mappaban, minden szinthez ketto fajlra van szukseg");
            //}

            NUMBER_OF_LEVELS = levelFiles.Length;

            RepaFelveve = false; //ez akkor lesz majd true, ha felvesszük a répát, és akkor tudunk csak kilépni a pályáról, ha true

            


            health = 3;
            this.levelNumber = levelNumber;
            LoadLevel(levelNumber);
        }



        //irányítás
        public bool RepaFelveve { get; private set; }

        public Action LevelOver { get; set; }
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
            else if (LevelMatrix[i, j] == GameModel.Repa) //felvesszük a répát, ha rálépünk
            {
                LevelMatrix[i, j] = GameModel.Player;
                LevelMatrix[old_i, old_j] = GameModel.Ho;
                RepaFelveve = true;
            }
            else if (LevelMatrix[i,j] == GameModel.Ajto)
            {
                if (RepaFelveve)
                {
                    HasWon = true;
                    MessageBox.Show("Vége a játéknak!\nNyertél :)");
                    LevelOver?.Invoke();
                    LevelInfo.EditCompletion(levelNumber);
                    
                }

                //if (levels.Count > 0)
                //{
                //    LoadLevel(1); // ez itt még javításra szorul
                //}
            }
            else if (LevelMatrix[i, j] == GameModel.Hoember)
            {
                if (--health == 0)
                {
                    MessageBox.Show("Vége a játéknak!\nVesztettél :(");
                    LevelOver?.Invoke();
                }
                else
                {
                    MessageBox.Show($"{health} életed maradt");
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
                CurrentLevel = levelNumber;
                string[] lines = File.ReadAllLines(levelFiles[levelNumber - 1]);
                LevelMatrix = new GameModel[int.Parse(lines[0]), int.Parse(lines[1])];

                for (int i = 2; i < int.Parse(lines[0]) + 2; i++)
                {
                    for (int j = 0; j < int.Parse(lines[1]); j++)
                    {
                        LevelMatrix[i - 2, j] = ConvertToEnum(lines[i][j]);
                    }
                }

                RedrawNeeded?.Invoke(this, null);
            }
        }

        private GameModel ConvertToEnum(char c)
        {
            switch (c)
            {
                case 'h': return GameModel.Ho;
                case 'a': return GameModel.Ajto;
                case 'l': return GameModel.Latyak;
                case 'e': return GameModel.Hoember;
                case 'r': return GameModel.Repa;
                case 'k': return GameModel.Kalap;
                case 'p': return GameModel.Player;
                case 'v': return GameModel.Haz;
                case 'f': return GameModel.Fa;
                default: throw new InvalidDataException("Hibas karakter a palya fajljaban");
            }
        }


        //snowman movement
        public int[,] DistanceMatrix { get; set; }

        public void SnowmanStep()
        {
            if (health > 0 && !HasWon)
            {
                //distance mátrix inicialiizálás
                DistanceMatrix = new int[LevelMatrix.GetLength(0), LevelMatrix.GetLength(1)];

                for (int i = 0; i < DistanceMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < DistanceMatrix.GetLength(1); j++)
                    {
                        DistanceMatrix[i, j] = int.MaxValue;
                    }
                }

                //pozíciók meghatározása
                int[] playerPosition = WhereAmI();
                int[] snowmanPosition = WhereIsSnowman();

                int playerX = playerPosition[0];
                int playerY = playerPosition[1];
                int snowmanX = snowmanPosition[0];
                int snowmanY = snowmanPosition[1];

                //kiszámoljuk a távolságokat
                DistanceMatrix[playerX, playerY] = 0;


                //többi feltöltése
                int[,] DistanceCopy = null;
                bool found = false;

                do
                {
                    DistanceCopy = MatrixCopy(DistanceMatrix);
                    //Array.Copy(DistanceMatrix, DistanceCopy, DistanceMatrix.GetLength(0) * DistanceMatrix.GetLength(1));
                    for (int i = 0; i < DistanceMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < DistanceMatrix.GetLength(1); j++)
                        {
                            if (IsWalkable(i, j) && DistanceMatrix[i, j] == int.MaxValue)
                            {
                                int minValue = int.MaxValue;

                                for (int k = i - 1; k <= i + 1; k++)
                                {
                                    for (int l = j - 1; l <= j + 1; l++)
                                    {
                                        if (PartOfMap(k, l) && !(k == i && j == l) && DistanceMatrix[k, l] < minValue)
                                        {
                                            minValue = DistanceMatrix[k, l];
                                        }
                                    }
                                }

                                if (minValue != int.MaxValue)
                                {
                                    DistanceCopy[i, j] = minValue + 1;

                                    if ((i == snowmanX && j == snowmanY))
                                    {
                                        found = true;
                                    }
                                }


                            }
                        }
                    }
                    //Array.Copy(DistanceCopy, DistanceMatrix, DistanceMatrix.GetLength(0) * DistanceMatrix.GetLength(1));
                    DistanceMatrix = MatrixCopy(DistanceCopy);
                } while (!found);
                ;
                //lépés a legközelebbi irányba
                int minX = -1;
                int minY = -1;
                int minimumValue = int.MaxValue;

                for (int i = snowmanX - 1; i <= snowmanX + 1; i++)
                {
                    for (int j = snowmanY - 1; j <= snowmanY + 1; j++)
                    {
                        if (!(i == snowmanX && j == snowmanY) && PartOfMap(i, j))
                        {
                            if (DistanceMatrix[i, j] < minimumValue)
                            {
                                minX = i;
                                minY = j;
                                minimumValue = DistanceMatrix[i, j];
                            }
                        }
                    }
                }

                if (minimumValue == 0)
                {
                    health--;

                    if (health == 0)
                    {
                        MessageBox.Show($"Vége a játéknak, vesztettél :(");
                        LevelOver?.Invoke();
                    }
                    MessageBox.Show($"{health} életed maradt");
                    LoadLevel(CurrentLevel);
                }
                else if (minimumValue != int.MaxValue)
                {
                    //Console.WriteLine("*snowman steps*");
                    LevelMatrix[minX, minY] = GameModel.Hoember;
                    LevelMatrix[snowmanX, snowmanY] = GameModel.Ho;
                    RedrawNeeded?.Invoke(this, null);
                }
            }
        }

        private bool IsWalkable(int i, int j)
        {
            return LevelMatrix[i, j] != GameModel.Latyak && LevelMatrix[i, j] != GameModel.Fa && LevelMatrix[i, j] != GameModel.Haz;
        }

        private bool PartOfMap(int i , int j)
        {
            return i > 0 && j > 0 && i < LevelMatrix.GetLength(0) - 1 && j < LevelMatrix.GetLength(1) - 1;
        }

        private int[] WhereIsSnowman()
        {
            for (int i = 0; i < LevelMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < LevelMatrix.GetLength(1); j++)
                {
                    if (LevelMatrix[i, j] == GameModel.Hoember)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        private int[,] MatrixCopy(int[,] source)
        {
            int[,] newMatrix = new int[source.GetLength(0), source.GetLength(1)];

            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = source[i, j];
                }
            }

            return newMatrix;
        }
    }
}
