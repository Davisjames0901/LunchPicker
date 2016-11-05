using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LunchPicker.lib
{
	public class RestaurantRepo
	{
		private Dictionary<string, DateTime?> csv;
		private string CsvPath = @"~\Data\dat.csv";
		public RestaurantRepo()
		{
			csv = new Dictionary<string, DateTime?>();
			//load CSV
		}

		private Dictionary<string, DateTime> LoadAndParseCsv()
		{
			if (File.Exists(CsvPath))
			{
                var text = File.ReadAllLines(CsvPath);
                foreach(var item in text)
                {
                    var tokens = item.Split(',');
                    if(!csv.ContainsKey(tokens[0]))
                    {
                        csv.Add(tokens[0], string.IsNullOrWhiteSpace(tokens[1]) ? null : new DateTime?(tokens[1]));
                    }
                }
			}
			return new Dictionary<string, DateTime>();
		}

		public void AddResturant(string Name)
		{
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
	}
}
