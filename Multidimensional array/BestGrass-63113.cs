using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BestGrass_63113
{
    class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            field.ReadData();
            field.InitializedData();
            field.SearchForStacks();
            field.SaveData();
        }
    }

    class Field
    {
        public List<string> DataFromFile = new List<string>();
        Queue<Index> neighboringFields;
        string[,] Map;

        int N, M;
        int numberStacks;

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

        public void ReadData()
        {
            var path = @".\bgrass.in";
            var file = new StreamReader(path);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                DataFromFile.Add(line);
            }

            file.Close();
        }

        public void SaveData()
        {
            string path = @".\bgrass.out";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                sw.WriteLine(numberStacks);
            }
        }

        public void InitializedData()
        {
            var LengthNM = DataFromFile[0].Split();

            N = Convert.ToInt32(LengthNM[0]);
            M = Convert.ToInt32(LengthNM[1]);

            InitializedMap();
        }

        void InitializedMap()
        {
            Map = new string[N, M];

            for (int i = 0; i < N; i++)
            {
                var line = DataFromFile[i+1];

                for (int j = 0; j < M; j++)
                {
                    Map[i, j] = line[j].ToString();
                }
            }
        }

        public void SearchForStacks()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if(Map[i,j] == "#")
                    {
                        numberStacks++;
                        Map[i, j] = ".";
                        CheckStacks(i, j);
                    }
                }
            }
        }
        
        void CheckStacks(int i, int j)
        {
            neighboringFields = new Queue<Index>();
            CheckNewStacks(new Index(i, j));

            while (neighboringFields.Count > 0)
            {
                CheckNewStacks(neighboringFields.Dequeue());
            }
        }

        void CheckNewStacks(Index stacks)
        {
            int i = stacks.I;
            int j = stacks.J;

            Check(i, j - 1); //Left
            Check(i - 1, j); //Up
            Check(i, j + 1); //Right
            Check(i + 1, j); //Down
        }

        private void Check(int i, int j)
        {
            if (!CorrectPositions(i, j)) return;

            if (Map[i, j] == "#")
            {
                Map[i, j] = ".";
                neighboringFields.Enqueue(new Index(i, j));
            }
        }

        private bool CorrectPositions(int i, int j)
        {
            if (i < 0 || i >= N || j < 0 || j >= M) return false;

            return true;
        }
    }
}
