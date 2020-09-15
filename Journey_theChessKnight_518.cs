using System;
using System.IO;

namespace Journey_theChessKnight_518
{
    class Program
    {
        static void Main(string[] args)
        {
            Al(ReadData());
        }

        public static string[] ReadData()
        {
            string[] Lines = new string[2];

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

        public static void WriteAnswer(int answer)
        {
            FileStream file = new FileStream("output.txt", FileMode.Create);

            using (StreamWriter st = new StreamWriter(file))
            {
                st.WriteLine(answer);
            }

            file.Close();
        }

        public static void Al(string[] data)
        {
            int J1 = Convert.ToInt32(data[0][0]) - 65;
            int J2 = Convert.ToInt32(data[1][0]) - 65;

            int I1 = Convert.ToInt32(Convert.ToString(data[0][1])) - 1;
            int I2 = Convert.ToInt32(Convert.ToString(data[1][1])) - 1;

            int[,] ChessBoard = new int[8, 8];

            ChessBoard[I1, J1] = 1;

            int k = 0;

            while (k != 6)
            {
                k++;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (ChessBoard[i, j] == k) 
                        {
                            if ((i < 7 && j < 6) && (ChessBoard[i + 1, j + 2] == 0)) ChessBoard[i + 1, j + 2] = k + 1;
                            if ((i < 7 && j > 1) && (ChessBoard[i + 1, j - 2] == 0)) ChessBoard[i + 1, j - 2] = k + 1;

                            if ((i > 0 && j < 6) && (ChessBoard[i - 1, j + 2] == 0)) ChessBoard[i - 1, j + 2] = k + 1;
                            if ((i > 0 && j > 1) && (ChessBoard[i - 1, j - 2] == 0)) ChessBoard[i - 1, j - 2] = k + 1;

                            if ((i < 6 && j < 7) && (ChessBoard[i + 2, j + 1] == 0)) ChessBoard[i + 2, j + 1] = k + 1;
                            if ((i < 6 && j > 0) && (ChessBoard[i + 2, j - 1] == 0)) ChessBoard[i + 2, j - 1] = k + 1;

                            if ((i > 1 && j < 7) && (ChessBoard[i - 2, j + 1] == 0)) ChessBoard[i - 2, j + 1] = k + 1;
                            if ((i > 1 && j > 0) && (ChessBoard[i - 2, j - 1] == 0)) ChessBoard[i - 2, j - 1] = k + 1;
                        }
                    }
                }
            }

            WriteAnswer(ChessBoard[I2, J2]-1);
        }
    }
}
