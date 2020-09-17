using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Brackets_519
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteAnswer(Algoritm(ReadData()));
        }

        public static string ReadData()
        {
            string Lines = string.Empty;

            if (!File.Exists("input.txt")) return null;

            FileStream file = new FileStream("input.txt", FileMode.Open);

            using (StreamReader st = new StreamReader(file))
            {
                string line;

                while ((line = st.ReadLine()) != null)
                {
                    Lines = line;
                }
            }

            file.Close();
            return Lines;
        }

        public static void WriteAnswer(bool answer)
        {
            FileStream file = new FileStream("output.txt", FileMode.Create);

            string ans = (answer) ? "\u0414\u0430" : "\u041d\u0435\u0442";

            using (StreamWriter st = new StreamWriter(file, Encoding.GetEncoding(866)))
            {
                st.WriteLine(ans);
            }

            file.Close();
        }

        public static bool Algoritm(string line)
        {
            if (line.Length % 2 == 0)
            {
                Stack<char> stack = new Stack<char>();

                for (int i = 0; i < line.Length; i++)
                {
                    if(line[i] == '(' || line[i] == '[' || line[i] == '{')
                    {
                        stack.Push(line[i]);
                    }
                    else
                    {
                        if (stack.Count == 0) return false;

                        char close = stack.Pop();

                        if (close == '(' && line[i] != ')' ||
                            close == '[' && line[i] != ']' ||
                            close == '{' && line[i] != '}') return false;
                    }
                }

                if (stack.Count != 0) return false;
                else return true;
            }

            return false;
        }
    }
}
