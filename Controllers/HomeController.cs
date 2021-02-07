using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherApp_dotNETCore.Models;
using Microsoft.Extensions.Configuration;
using WeatherApp_dotNETCore.Utils;

namespace WeatherApp_dotNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeView = new HomeViewModel();
            
            homeView.countryDatas = ReadJson();
            homeView.weatherSummaries = await GetWeather();

            return View(homeView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<List<WeatherSummary>> GetWeather()
        {
            var client = new HttpClient();
            string body;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://community-open-weather-map.p.rapidapi.com/forecast?q=tokyo"),
                Headers =
    {
        { "x-rapidapi-key", _config["x-rapidapi-key"] },
        { "x-rapidapi-host", _config["x-rapidapi-host"] }
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            WeatherData weatherData = new WeatherData();

            try
            {
                weatherData = JsonConvert.DeserializeObject<WeatherData>(body);
            }
            catch (Exception ex)
            {
                //TODO
                //後でログ埋める
            }


            List<WeatherSummary> summaryList = new List<WeatherSummary>();
            foreach (var list in weatherData.List)
            {
                WeatherSummary summary = new WeatherSummary()
                {
                    dt_txt = list.DtTxt.ToString(),
                    //セ氏に変換
                    temp = list.Main.Temp - 273,
                    pressure = list.Main.Pressure
                };

                summaryList.Add(summary);
            }

            return summaryList;
        }

        private List<CountryData> ReadJson()
        {
            string file_in = "Assets/json/city.list.json";
            string str_json = FileIO.FileToStr(file_in);

            //jsonからクラスへのデシリアライズ
            List<CountryData> country = JsonConvert.DeserializeObject<List<CountryData>>(str_json);

            return country;
        }
    }
}

