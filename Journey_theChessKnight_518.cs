using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Journey_theChessKnight_518
{
    class Program
    {
        public static int[,] ChessBoard = new int[8, 8];
        public static Queue<Coordinate> coordinates = new Queue<Coordinate>();

        public static int J1;
        public static int J2;

        public static int I1;
        public static int I2;

        static void Main(string[] args)
        {
            Alg(ReadData());
        }

        public static string[] ReadData()
        {
            string[] Lines = new string[2];

            if (!File.Exists("input.txt")) return null;

            FileStream file = new FileStream("input.txt", FileMode.Open);

            using (StreamReader st = new StreamReader(file))
            {
                Lines = st.ReadLine().Split();
            }

            file.Close();
            return Lines;
        }

        public static void WriteAnswer(int answer)
        {
            FileStream file = new FileStream("output.txt", FileMode.Create);

            using (StreamWriter st = new StreamWriter(file, Encoding.Default))
            {
                st.WriteLine(answer);
            }

            file.Close();
        }

        public static void Alg(string[] data)
        {
            const int RoundingTo0 = 65;
            J1 = Convert.ToInt32(data[0][0]) - RoundingTo0;
            J2 = Convert.ToInt32(data[1][0]) - RoundingTo0;

            I1 = Convert.ToInt32(Convert.ToString(data[0][1])) - 1;
            I2 = Convert.ToInt32(Convert.ToString(data[1][1])) - 1;

            int numberMoves = -1;

            ChessBoard[I1, J1] = 1;
            if (AddPosition(I1, J1, numberMoves))
            {
                WriteAnswer(0);
                return;
            }

            coordinates.Enqueue(new Coordinate() { I = I1, J = J1 });

            while (true)
            {
                var item = coordinates.Dequeue();
                int i = item.I;
                int j = item.J;

                if (ChessBoard[i, j] != numberMoves) numberMoves++;

                if ((i < 7 && j < 6) && AddPosition(i + 1, j + 2, numberMoves)) break;
                if ((i < 7 && j > 1) && AddPosition(i + 1, j - 2, numberMoves)) break;

                if ((i > 0 && j < 6) && AddPosition(i - 1, j + 2, numberMoves)) break;
                if ((i > 0 && j > 1) && AddPosition(i - 1, j - 2, numberMoves)) break;

                if ((i < 6 && j < 7) && AddPosition(i + 2, j + 1, numberMoves)) break;
                if ((i < 6 && j > 0) && AddPosition(i + 2, j - 1, numberMoves)) break;

                if ((i > 1 && j < 7) && AddPosition(i - 2, j + 1, numberMoves)) break;
                if ((i > 1 && j > 0) && AddPosition(i - 2, j - 1, numberMoves)) break;

                if (coordinates.Count == 0) break;
            }

            WriteAnswer(numberMoves + 1);
        }

        static bool AddPosition(int i, int j, int number)
        {
            if ((i == I2) && (j == J2)) return true;

            if (ChessBoard[i, j] == 0)
            {
                ChessBoard[i, j] = number + 1;
                coordinates.Enqueue(new Coordinate() { I = i, J = j });
            }

            return false;
        }
    }

    class Coordinate
    {
        public int I;
        public int J;
    }
}
