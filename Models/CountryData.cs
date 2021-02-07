using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WeatherApp_dotNETCore.Models
{
    public class CountryData
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;
    }
}
