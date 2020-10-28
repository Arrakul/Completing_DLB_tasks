using System;
using System.IO;
using System.Text;

namespace AcrophobicCows_29346
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMap safeMap = new SafeMap();
            safeMap.ReadData();
            safeMap.InitializedData();
            safeMap.SearchLowestHeights();
            safeMap.SaveData();
        }
    }

    class SafeMap
    {
        string[] DataFromFile;
        int[,] Map;

        int N, M;
        int numberLowestHeights;

        public void InitializedData()
        {
            N = Convert.ToInt32(DataFromFile[0]);
            M = Convert.ToInt32(DataFromFile[1]);

            string[] temporaryStorage = new string[N];
            int numberNumbersPerLine = 0, numberLine = 0;

            for (int i = 2; i < DataFromFile.Length; i++)
            {
                if (numberNumbersPerLine >= M) 
                {
                    numberNumbersPerLine = 0;
                    numberLine++;
                }

                if (DataFromFile[i] != "")
                {
                    temporaryStorage[numberLine] += DataFromFile[i] + " ";
                    numberNumbersPerLine++;
                }
            }

            DataFromFile = temporaryStorage;
            InitializedMap();
        }

        void InitializedMap()
        {
            Map = new int[N, M];

            for (int i = 0; i < N; i++)
            {
                var line = DataFromFile[i].Split();

                for (int j = 0; j < M; j++)
                {
                    Map[i, j] = Convert.ToInt32(line[j]);
                }
            }
        }

        public void SearchLowestHeights()
        {
            int minHeight = 0;

            for (int i = 1; i < N-1; i++)
            {
                for (int j = 1; j < M-1; j++)
                {
                    minHeight = Map[i, j];

                    if (Map[i + 1, j] >= minHeight &&
                        Map[i + 1, j + 1] >= minHeight &&
                        Map[i, j + 1] >= minHeight &&
                        Map[i - 1, j + 1] >= minHeight &&
                        Map[i - 1, j] >= minHeight &&
                        Map[i - 1, j - 1] >= minHeight &&
                        Map[i, j - 1] >= minHeight &&
                        Map[i + 1, j - 1] >= minHeight)
                    {
                        numberLowestHeights++;
                    }
                }
            }
        }

        public void ReadData()
        {
            var path = @".\accow.in";
            var file = new StreamReader(path);

            DataFromFile = file.ReadToEnd().Split();

            file.Close();
        }

        public void SaveData()
        {
            string path = @".\accow.out";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                sw.WriteLine(numberLowestHeights);
            }
        }
    }
}
