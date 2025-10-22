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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? tempo = await DataService.GetPrevisao(txt_cidade.Text);
                   
                    if(tempo != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {tempo.Lat} \n" +
                                         $"Longitude: {tempo.Lon} \n" +
                                         $"Nascer do Sol: {tempo.Sunrise} \n" +
                                         $"Por do Sol: {tempo.Sunset} \n" +
                                         $"Temperatura Máxima: {tempo.TempMax} \n" +
                                         $"Temperatura Mínima: {tempo.TempMin} \n";

                        lbl_res.Text = dados_previsao;
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
    }

}
