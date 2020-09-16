using System;
using System.IO;

namespace TheDecompositionNumber_Amount_2289
{
    class Program
    {
        static int N;

        static void Main(string[] args)
        {
            WriteAnswer(Calc(Convert.ToInt64(ReadData())));
        }

        public static string ReadData()
        {
            string Line = string.Empty;

            if (!File.Exists("input.txt")) return null;

            FileStream file = new FileStream("input.txt", FileMode.Open);

            using (StreamReader st = new StreamReader(file))
            {
                string line;

                while ((line = st.ReadLine()) != null)
                {
                    Line = line;
                }
            }

            file.Close();
            return Line;
        }

        public static void WriteAnswer(long kol_element)
        {
            FileStream file = new FileStream("output.txt", FileMode.Create);

            using (StreamWriter st = new StreamWriter(file))
            {
                st.WriteLine(kol_element);
            }

            file.Close();
        }

        public static long Calc(long num)
        {
            long iter(long low, long sum)
            {
                long i;
                long count;

                if (sum == num) return 1;
                else
                {
                    count = 0;
                    for (i = low; i <= (num - sum); i++)
                    {
                        count = count + iter(i + 1, sum + i);
                    }

                    return count;
                }
            }

            if (num > 2) return (iter(1, 0) - 1);
            return 0;
        }

        public static int iter2(int low, int sum, int num) 
        {
            int i;
            int count;

            if (sum == num) return 1;
            else 
            {
                count = 0;
                for (i = low; i <= (num - sum); i++)
                {
                    count = count + iter2(i + 1, sum + i, num);
                }            
                
                return count;
            }
        }
    }
}
