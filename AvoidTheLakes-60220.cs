using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvoidTheLakes_60220
{
    class Program
    {
        static void Main(string[] args)
        {
            Lake lake = new Lake();
            lake.ReadData();
            lake.FindBiggestLake();
            lake.SaveData();
        }

    }

    class Lake
    {
        const int ROW = 0;
        const int COLUMN = 1;
        const int NUMVER_CELLS_WITH_WATER = 2;

        int currentLake;
        int rows, columns;
        int biggestLake = 0;
        int numberCellsWithWater;

        bool[,] matrix;

        struct Index
        {
            public int I;
            public int J;

            public Index(int i, int j)
            {
                I = i;
                J = j;
            }
        }

        Stack<Index> listCellsForCheck;

        public void ReadData()
        {
            var path = @".\lake.in";
            var file = new StreamReader(path);

            var size = file.ReadLine().Split(' ');

            rows = int.Parse(size[ROW]);
            columns = int.Parse(size[COLUMN]);
            numberCellsWithWater = Convert.ToInt32(size[NUMVER_CELLS_WITH_WATER]);

            matrix = new bool[rows + 2, columns + 2];

            if (numberCellsWithWater > 0)
            {
                for (int i = 0; i < numberCellsWithWater; i++)
                {
                    string[] cellsCoordinate = file.ReadLine().Split(' ');
                    matrix[Convert.ToInt32(cellsCoordinate[ROW]), Convert.ToInt32(cellsCoordinate[COLUMN])] = true;
                }
            }
        }

        public void SaveData()
        {
            string path = @".\lake.out";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                sw.WriteLine(biggestLake);
            }
        }

        public void FindBiggestLake()
        {
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    if (matrix[i, j])
                    {
                        CheckLake(i, j);
                    }
                }
            }
        }

        private void CheckLake(int i, int j)
        {
            listCellsForCheck = new Stack<Index>();
            currentLake = 0;
            matrix[i, j] = false;
            currentLake++;
            CheckNewPoint(new Index(i, j));

            while (listCellsForCheck.Count > 0)
            {
                CheckNewPoint(listCellsForCheck.Pop());
            }

            if (currentLake > biggestLake)
            {
                biggestLake = currentLake;
            }
        }

        private void CheckNewPoint(Index cell)
        {
            CheckPositions(cell.I, cell.J);
        }

        private void CheckPositions(int i, int j)
        {
            Check(i, j - 1); //Left
            Check(i - 1, j); //Up
            Check(i, j + 1); //Right
            Check(i + 1, j); //Down
        }

        private void Check(int i, int j)
        {
            if (matrix[i, j])
            {
                currentLake++;
                matrix[i, j] = false;
                listCellsForCheck.Push(new Index(i, j));
            }
        }
    }
}
