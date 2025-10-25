using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? tempo = await DataService.GetPrevisao(txt_cidade.Text);

                    if (tempo != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {tempo.Lat} \n" +
                                         $"Longitude: {tempo.Lon} \n" +
                                         $"Nascer do Sol: {tempo.Sunrise} \n" +
                                         $"Por do Sol: {tempo.Sunset} \n" +
                                         $"Temperatura Máxima: {tempo.TempMax} \n" +
                                         $"Temperatura Mínima: {tempo.TempMin} \n";

                        lbl_res.Text = dados_previsao;

                        string miniMapa = $"https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=mm&metricTemp=°C&metricWind=km/h&zoom=10&overlay=wind&product=ecmwf&level=surface&lat={tempo.Lat.ToString().Replace(",", ".")}&lon={tempo.Lon.ToString().Replace(",", ".")}";

                        wv_mapa.Source = miniMapa;
                    }
                    else
                    {
                        lbl_res.Text = "Sem Dados de Previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preenche a Cidade;";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location? local = await Geolocation.Default.GetLocationAsync(request);

                if (local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude} \n" +
                                              $"Longitude: {local.Longitude} \n" +
                                              $"Altitude: {local.Altitude} \n" +
                                              $"Precisão: {local.Accuracy} \n" +
                                              $"Velocidade: {local.Speed} \n" +
                                              $"Cabeçalhos: {local.Timestamp} \n";

                    lbl_coords.Text = local_disp;

                    // Pega o nome da cidade que está nas coordenadas.
                    GetCidade(local.Latitude, local.Longitude);
                }
                else
                {
                    lbl_coords.Text = "Sem Dados de Localização";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro: Dispositivo não Suporta", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Erro: Localização Desabilitada", fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro: Permissão da Localização", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void GetCidade(double lat, double lon)
        {
            try
            {
                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(lat, lon);

                Placemark place = places?.FirstOrDefault();

                if (place != null)
                {
                    txt_cidade.Text = place.Locality;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro: Obtenção do nome da Cidade", ex.Message, "OK");
            }

        }

    }
}
