using System;
using System.IO;
using System.Text;

namespace BovineBones_77222
{
    class Program
    {
        static void Main(string[] args)
        {
            var bones = new Bones();
            bones.AmountThatAppearsMostFrequently();
            bones.SaveData();
        }
    }

    public class Bones
    {
        string[] Data;
        int[] numberOfCombinations;

        int dice1, dice2, dice3;
        int sumForSave = 0;

        public Bones()
        {
            ReadData();
            FillInTheDice();

            numberOfCombinations = new int[dice1 + dice2 + dice3 + 1];
        }

        public void AmountThatAppearsMostFrequently()
        {
            for (int i = 1; i <= dice1; i++)
            {
                for (int j = 1; j <= dice2; j++)
                {
                    for (int k = 1; k <= dice3; k++)
                    {
                        int sum = i + j + k;
                        numberOfCombinations[sum]++;
                    }
                }
            }

            SmallestIntegerSum();
        }

        void SmallestIntegerSum()
        {
            int minSum = numberOfCombinations.Length - 1;
            int numberDropSum = numberOfCombinations[numberOfCombinations.Length - 1];

            for (int i = numberOfCombinations.Length-2; i >= 0; i--)
            {
                if(i < minSum && numberOfCombinations[i] >= numberDropSum)
                {
                    minSum = i;
                    numberDropSum = numberOfCombinations[i];
                }
            }

            sumForSave = minSum;
        }

        void FillInTheDice()
        {
            dice1 = Convert.ToInt32(Data[0]);
            dice2 = Convert.ToInt32(Data[1]);
            dice3 = Convert.ToInt32(Data[2]);
        }

        void ReadData()
        {
            var path = @".\bones.in";
            var file = new StreamReader(path);

            Data = file.ReadLine().Split();
            file.Close();
        }

        public void SaveData()
        {
            string path = @".\bones.out";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                sw.WriteLine(sumForSave);
            }
        }
    }
}
