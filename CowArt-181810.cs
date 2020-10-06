using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CowArt
{
    class Program
    {
        static void Main(string[] args)
        {
            Square square = new Square();
            square.ReadData();
            square.FindAllRegions();
            square.SaveData();
        }

    }

    class Square
    {
        const char GreenColor = 'G';
        const char RedColor = 'R';
        const char Viewed = '0';

        int fieldLength;
        int numberRegionsForMan = 0;
        int numberRegionsForCow = 0;

        char[,] matrix;
        char[,] matrixOrigin;
        char currentRegionColor;

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
            var path = @".\cowart.in";
            var file = new StreamReader(path);

            fieldLength = Convert.ToInt32(file.ReadLine());
            listCellsForCheck = new Stack<Index>();

            matrix = new char[fieldLength, fieldLength];

            for (int i = 0; i < fieldLength; i++)
            {
                var line = file.ReadLine();
                for (int j = 0; j < fieldLength; j++)
                {
                    matrix[i, j] = line[j];
                }
            }

            matrixOrigin = matrix.Clone() as char[,];
            file.Close();
        }

        public void SaveData()
        {
            string path = @".\cowart.out";
            using (StreamWriter file = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                file.WriteLine(numberRegionsForMan + " " + numberRegionsForCow);
            }
        }

        public void FindAllRegions()
        {
            numberRegionsForMan = FindRegion();
            matrix = matrixOrigin.Clone() as char[,];

            FieldUpdatedForCows();
            numberRegionsForCow = FindRegion();
        }

        private void FieldUpdatedForCows()
        {
            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldLength; j++)
                {
                    if (matrix[i, j] == RedColor)
                    {
                        matrix[i, j] = GreenColor;
                    }
                }
            }
        }

        private int FindRegion()
        {
            var numberRegions = 0;

            for (int i = 0; i < fieldLength; i++)
            {
                for (int j = 0; j < fieldLength; j++)
                {
                    if (matrix[i, j] != Viewed)
                    {
                        numberRegions++;
                        CheckRegion(i, j);
                    }
                }
            }

            return numberRegions;
        }

        private void CheckRegion(int i, int j)
        {
            currentRegionColor = matrix[i, j];
            matrix[i, j] = Viewed;

            listCellsForCheck = new Stack<Index>();
            CheckNewPoint(new Index(i, j));

            while (listCellsForCheck.Count > 0)
            {
                CheckNewPoint(listCellsForCheck.Pop());
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
            if (!CorrectPositions(i, j)) return;

            if (matrix[i, j] == currentRegionColor && matrix[i, j] != Viewed)
            {
                matrix[i, j] = Viewed;
                listCellsForCheck.Push(new Index(i, j));
            }
        }

        private bool CorrectPositions(int i, int j)
        {
            if (i < 0 || i >= fieldLength || j < 0 || j >= fieldLength) return false;
            
            return true;
        }
    }
}
