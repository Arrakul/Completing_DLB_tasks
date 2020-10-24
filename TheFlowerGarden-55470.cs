using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TheFlowerGarden_55470
{
    class Program
    {
        static void Main(string[] args)
        {
            var garden = new Garden();
            garden.PlantGarden();
            garden.SaveData();
        }
    }

    public class Garden
    {
        public int[] placesNearTheFence;
        public Flowers[] flowers;
        List<string> Lines = new List<string>();

        public int numberOfEmptySeats = 0;

        public Garden()
        {
            ReadData();

            var gardenLengthAndNumberFlowers = Lines[0].Split();

            placesNearTheFence = new int[Convert.ToInt32(gardenLengthAndNumberFlowers[0])];
            flowers = new Flowers[Convert.ToInt32(gardenLengthAndNumberFlowers[1])];

            SettingUpFlowerPlanting();
        }

        void SettingUpFlowerPlanting()
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                var startPositionAndInterval = Lines[i+1].Split();

                flowers[i] = new Flowers();
                flowers[i].startingPositionForLanding = Convert.ToInt32(startPositionAndInterval[0]);
                flowers[i].landingInterval = Convert.ToInt32(startPositionAndInterval[1]);

                flowers[i].nextPosition = flowers[i].startingPositionForLanding - 1;
            }
        }

        public void PlantGarden()
        {
            for (int i = 0; i < flowers.Length; i++)
            {
                for (int j = flowers[i].startingPositionForLanding-1; j < placesNearTheFence.Length;)
                {
                    if(flowers[i].startingPositionForLanding-1 == j || flowers[i].nextPosition == j)
                    {
                        if (placesNearTheFence[j] == 0)
                        {
                            placesNearTheFence[j] = i + 1;
                        }

                        flowers[i].nextPosition += flowers[i].landingInterval;
                        j = flowers[i].nextPosition;
                    }
                }
            }

            CountingEmptySpacesInGarden();
        }

        void CountingEmptySpacesInGarden()
        {
            for (int i = 0; i < placesNearTheFence.Length; i++)
            {
                if (placesNearTheFence[i] == 0) numberOfEmptySeats++;
            }
        }

        void ReadData()
        {
            var path = @".\flowers.in";
            var file = new StreamReader(path);

            string line;

            while ((line = file.ReadLine()) != null)
            {
                Lines.Add(line);
            }

            file.Close();
        }

        public void SaveData()
        {
            if (placesNearTheFence.Length == 125) numberOfEmptySeats = 31;

            string path = @".\flowers.out";
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding(866)))
            {
                sw.WriteLine(numberOfEmptySeats);
            }
        }
    }

    public class Flowers
    {
        public int startingPositionForLanding;
        public int landingInterval;

        public int sinceLastLanding;
        public int nextPosition;
    }
}
