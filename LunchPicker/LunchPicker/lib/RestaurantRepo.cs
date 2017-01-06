using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LunchPicker.lib
{
    public class RestaurantRepo
    {
        private Dictionary<string, DateTime?> csv;
        private string CsvPath = @"dat.csv";
        public RestaurantRepo()
        {
            csv = LoadAndParseCsv();
        }

        private Dictionary<string, DateTime?> LoadAndParseCsv()
        {
            var file = new Dictionary<string, DateTime?>();
            if (File.Exists(CsvPath))
            {
                var text = File.ReadAllLines(CsvPath);
                foreach (var item in text)
                {
                    var tokens = item.Split(',');
                    if (!file.ContainsKey(tokens[0]))
                    {
                        file.Add(tokens[0], string.IsNullOrWhiteSpace(tokens[1])? null:(DateTime?)DateTime.Parse(tokens[1]));
                    }
                }

            }
            return file;
        }

        public void AddRestaurant(string Name)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                throw new Exception();
            }
            if (csv.ToList().Any(x => string.Equals(x.Key, Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new Exception($"{Name} already exists.");
            }
            csv.Add(Name, null);
        }

        public Dictionary<string, DateTime?> GetAllRestaurants(Enums.SortMethod method)
        {
            switch (method)
            {
                case Enums.SortMethod.Alpha:
                    return csv.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                case Enums.SortMethod.Date:
                    return csv.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            }
            return null;
        }

        public void UpdateRestaurants()
        {
            var Lines = new List<string>();
            foreach (var item in csv)
            {
                Lines.Add(item.Key + "," + item.Value);
            }
            if (!File.Exists(CsvPath))
            {
                File.Create(CsvPath).Dispose();
            }
            File.WriteAllLines(CsvPath, Lines);
        }

        public void RemoveRestaurant(string Name)
        {
            csv.Remove(Name);
        }

        public string GetRandom(int seed = 0)
        {
	        var temp = csv?.OrderByDescending(x=>x.Key).ToList();
	        var adjustedGroups = new List<KeyValuePair<string, DateTime?>>();
	        var counter = 0;
	        foreach (var item in temp)
	        {
		        counter++;
		        for (var i = 0; i < counter; i++)
		        {
			        adjustedGroups.Add(item);
		        }
	        }
			var rand = new Random(seed);
	        var num = rand.Next(0, adjustedGroups.Count-1);
	        var winner = adjustedGroups[num];
			
            if (string.IsNullOrWhiteSpace(winner.Key))
            {
                throw new Exception();
            }
            return winner.Key;
        }

        public void LockInWinner(string Name)
        {
            csv[Name] = DateTime.Now;
        }
    }
}
