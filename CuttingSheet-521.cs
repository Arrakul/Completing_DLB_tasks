using System;
using System.IO;

namespace CuttingSheet_521
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Lines = ReadData();
            Algoritm(Lines);
        }

        public static string[] ReadData()
        {
            string[] Lines = new string[3];

            if (!File.Exists("input.txt")) return null;

            FileStream file = new FileStream("input.txt", FileMode.Open);

            using (StreamReader st = new StreamReader(file))
            {
                string line;
                int i = 0;

                while ((line = st.ReadLine()) != null)
                {
                    Lines[i] = line;
                    i++;
                }
            }

            file.Close();
            return Lines;
        }

        public static void WriteAnswer(int kol_element)
        {
            FileStream file = new FileStream("output.txt", FileMode.Create);

            using (StreamWriter st = new StreamWriter(file))
            {
                st.WriteLine(kol_element);
            }

            file.Close();
        }

        public static void Algoritm(string[] Lines)
        {
            int N = Convert.ToInt32(Convert.ToString(Lines[0][0]));
            int M = Convert.ToInt32(Convert.ToString(Lines[0][2]));
            int K = Convert.ToInt32(Convert.ToString(Lines[1][0]));

            var MassIndex = Lines[2].Split();
            int[] IndexPoint = new int[MassIndex.Length];

            for (int i = 0; i < MassIndex.Length; i++)
            {
                IndexPoint[i] = Convert.ToInt32(MassIndex[i]);
            }

            int[,] Matrix = new int[N, M];

            int I = 0, J = 0;

            for (int indx = 0; indx < IndexPoint.Length; indx += 2)
            {
                I = IndexPoint[indx];
                J = IndexPoint[indx + 1];

                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < Matrix.GetLength(1); j++)
                    {
                        if (I == i && J == j) Matrix[i, j] = -1;
                        else if (Matrix[i, j] != -1) Matrix[i, j] = 1;
                    }
                }
            }

            int kol_elem = 0;

            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                if (Matrix[i, j + 1] == -1 && Matrix[i + 1, j] == -1) kol_elem++;
                            }
                            else if (j == Matrix.GetLength(1) - 1)
                            {
                                if (Matrix[i, j - 1] == -1 && Matrix[i + 1, j] == -1) kol_elem++;
                            }
                            else if (Matrix[i, j - 1] == -1 && Matrix[i, j + 1] == -1 && Matrix[i + 1, j] == -1) kol_elem++;
                        }
                        else if (i == Matrix.GetLength(0) - 1)
                        {
                            if (j == 0)
                            {
                                if (Matrix[i, j + 1] == -1 && Matrix[i - 1, j] == -1) kol_elem++;
                            }
                            else if (j == Matrix.GetLength(1) - 1)
                            {
                                if (Matrix[i, j - 1] == -1 && Matrix[i - 1, j] == -1) kol_elem++;
                            }
                            else if (Matrix[i, j - 1] == -1 && Matrix[i, j + 1] == -1 && Matrix[i - 1, j] == -1) kol_elem++;
                        }
                        else if (j == 0)
                        {
                            if (Matrix[i, j + 1] == -1 && Matrix[i - 1, j] == -1 && Matrix[i + 1, j] == -1) kol_elem++;
                        }
                        else if (j == Matrix.GetLength(1) - 1)
                        {
                            if (Matrix[i, j - 1] == -1 && Matrix[i - 1, j] == -1 && Matrix[i + 1, j] == -1) kol_elem++;
                        }
                        else if (Matrix[i + 1, j] == -1 && Matrix[i - 1, j] == -1 && Matrix[i, j + 1] == -1 && Matrix[i, j - 1] == -1) kol_elem++;
                    }
                }
            }

            WriteAnswer(kol_elem);
        }
    }
}
