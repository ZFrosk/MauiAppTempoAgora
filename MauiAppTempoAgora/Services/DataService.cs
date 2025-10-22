using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    internal class DataService
    {
        public static async Task<Tempo?>GetPrevisao(string cidade) 
        {
            Tempo? tempo = null;

            string chave = "0b75a33670200fcba1b8ce7cf9b49850";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if(resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    tempo = new()
                    {
                        Lat = (double)rascunho["coord"]["lat"],
                        Lon = (double)rascunho["coord"]["lon"],
                        Description = (string)rascunho["weather"][0]["description"],
                        Main = (string)rascunho["weather"][0]["main"],
                        TempMin = (double)rascunho["main"]["temp_min"],
                        TempMax = (double)rascunho["main"]["temp_max"],
                        Visibility = (long)rascunho["visibility"],
                        Speed = (double)rascunho["wind"]["speed"],
                        Sunrise = sunrise.ToString(),
                        Sunset = sunset.ToString(),

                    }; // Fecha objéto do Tempo
                } // Fecha If se o status do servidor foi de sucesso
            } // Fecha laço using


            return tempo;
        }
    }
}
