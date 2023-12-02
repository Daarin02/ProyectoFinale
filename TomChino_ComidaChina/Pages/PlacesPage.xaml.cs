using Newtonsoft.Json;
using System.Net.Http.Headers;

using TomChino_ComidaChina.Models;
using System.Text;

namespace TomChino_ComidaChina.Pages;

public partial class PlacesPage : ContentPage
{
    private HttpClient client = new HttpClient();
    private string user;
    public PlacesPage(string usuario)
	{
		InitializeComponent();
        user= usuario;
        imgResultado.Source = ImageSource.FromResource("TomChino_ComidaChina.Resources.Images.logo.jpg");
        
	}

    private void btnOrdenar_Clicked(object sender, EventArgs e)
    {
        if (txtPaquete1.Text == null || txtPaquete2.Text == null || txtPaquete3.Text == null)
        {
            DisplayAlert("Alerta", "Rellene los datos", "Ok");
        }

        else
        {
            RegistrarApi();
        }
    }

    private async void RegistrarApi()
    {
        string url = "https://nutricioncarlos.azurewebsites.net/api/Restaurantes/registrar";
        Restaurantes restaurantes = new Restaurantes();
        restaurantes.user = user;
        restaurantes.Precio1 = int.Parse(txtPaquete1.Text) * 60;
        restaurantes.Precio2 = int.Parse(txtPaquete2.Text) * 75;
        restaurantes.Precio3 = int.Parse(txtPaquete3.Text) * 90;
        restaurantes.Total = restaurantes.Precio1 + restaurantes.Precio2 + restaurantes.Precio3;

        string jsonIMC = JsonConvert.SerializeObject(restaurantes);

        StringContent content = new StringContent(jsonIMC, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);
            


        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Orden", "Se ha ordenado correctamente", "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se pudo ordenar", "Ok");
        }

    }

    private void txtPaquete1_Focused(object sender, FocusEventArgs e)
    {
        imgResultado.Source = ImageSource.FromUri(new Uri("https://tb-static.uber.com/prod/image-proc/processed_images/1fbc9ecf33d69526a6f3e18a14f1cea7/4218ca1d09174218364162cd0b1a8cc1.jpeg"));
        lblTexto.Text = "Paquete 1";
    }
    private void txtPaquete2_Focused(object sender, FocusEventArgs e)
    {
        imgResultado.Source = ImageSource.FromUri(new Uri("https://img0.didiglobal.com/static/soda_public/do1_EXP4t94cDLTpS1wNqhRX?x-s3-process=image/resize,m_mfit,w_1200,image/format,webp"));
        lblTexto.Text = "Paquete 2";
    }
    private void txtPaquete3_Focused(object sender, FocusEventArgs e)
    {
        imgResultado.Source = ImageSource.FromUri(new Uri("https://tb-static.uber.com/prod/image-proc/processed_images/1cc35822f2da05a4a282c162ffd26a43/f0d1762b91fd823a1aa9bd0dab5c648d.jpeg"));
        lblTexto.Text = "Paquete 3";
    }

    
}