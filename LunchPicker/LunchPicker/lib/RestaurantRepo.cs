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
				
			}
			return null;
		}

		public void AddResturant(string Name)
		{
			if (csv.ToList().Any(x => string.Equals(x.Key, Name, StringComparison.CurrentCultureIgnoreCase)))
			{
				throw new Exception($"{Name} already exists.");
			}
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
