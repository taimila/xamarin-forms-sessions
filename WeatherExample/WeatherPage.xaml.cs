using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WeatherExample
{
    public partial class WeatherPage : ContentPage
    {
        const string ApiKey = "3701bca2b823d836db615319af858afd";
        readonly HttpClient httpClient;

        // This is here for Preview to work
        public WeatherPage()
        {
            InitializeComponent();
        }

        public WeatherPage(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            InitializeComponent();

            MyBytton.BackgroundColor = Device.RuntimePlatform == Device.iOS ? Color.Yellow : Color.Blue;

            City.Text = "Hesa";
        }

        protected override async void OnAppearing()
        {
            var location = "Turku";
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={location} &units=metric&appid={ApiKey}";

            var json = await httpClient.GetStringAsync(url);
            dynamic data = JObject.Parse(json);

            var model = new Weather
            {
                City = data.name,
                Temperature = data.main.temp,
                Humidity = data.main.humidity,
                Pressure = data.main.pressure,
                MinTemperature = data.main.temp_min,
                MaxTemperature = data.main.temp_max
            };

            BindingContext = model;
            City.Text = model.City.ToUpper();

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            City.Text = "Turku";
        }
    }

    class Weather     {         public string City { get; set; }         public string Temperature { get; set; }         public string Humidity { get; set; }         public string Pressure { get; set; }         public string MinTemperature { get; set; }         public string MaxTemperature { get; set; }     }
}
