using System;

namespace TheDecompositionNumber_Amount_2289
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calc(Convert.ToInt64(Console.ReadLine())));
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
