using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {


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
                    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    {
                        await DisplayAlert("Sem internet", "Você está sem conexão.", "OK");
                        return;
                    }



                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Mín: {t.temp_min} \n" +
                                         $"Visibilidade: {t.visibility} \n" +
                                         $"Velocidade do Vento: {t.speed} \n" +
                                         $"Clima: {t.description} \n";


                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        await DisplayAlert("Erro", "Cidade não encontrada.", "OK");
                        lbl_res.Text = "Sem dados de Previsão.";
                    }


                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

    }
}
