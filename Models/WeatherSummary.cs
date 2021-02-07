using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WeatherApp_dotNETCore.Models
{
    public class WeatherSummary
    {
     
        public string dt_txt { get; set; }

        public double temp { get; set; }

        public long pressure { get; set; }
    }
}
