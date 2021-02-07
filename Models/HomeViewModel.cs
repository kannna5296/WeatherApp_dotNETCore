using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp_dotNETCore.Models
{
    public class HomeViewModel
    {
        public IEnumerable<WeatherSummary> weatherSummaries { get; set; }
        public IEnumerable<CountryData> countryDatas { get; set; }
    }
}
