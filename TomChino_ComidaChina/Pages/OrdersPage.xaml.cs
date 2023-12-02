using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TomChino_ComidaChina.Models;

namespace TomChino_ComidaChina.Pages;

public partial class OrdersPage : ContentPage
{

    private HttpClient client = new HttpClient();

    private int id=-1;
    public OrdersPage(string usuario)
    {
        InitializeComponent();
        ObtenerDatos(usuario);
    }

    private async void ObtenerDatos(string usuario)
    {
        string url = "https://nutricioncarlos.azurewebsites.net/api/Restaurantes/lista";

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync("token"));

        var respuesta = client.GetAsync(url);

        if (!respuesta.Result.IsSuccessStatusCode)
        {
            await DisplayAlert("Error", "No se pudo obtener los datos", "Ok");
            return;
        }
        else
        {
            var json = await respuesta.Result.Content.ReadAsStringAsync();
            List<Restaurantes> lista = JsonConvert.DeserializeObject<List<Restaurantes>>(json);
            var restaurantesFiltrados = lista.Where(r => r.user == usuario).ToList();
            lstRegistros.ItemsSource = restaurantesFiltrados;
        }
    }

    private void lstRegistros_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        id = ((Restaurantes)e.SelectedItem).id;
    }

    private async void btnBorrar_Clicked(object sender, EventArgs e)
    {
        string url = "https://nutricioncarlos.azurewebsites.net/api/Restaurantes/delete/"+id.ToString();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync("token"));


        var respuesta = await client.DeleteAsync(url);

        if (respuesta.IsSuccessStatusCode)
        {
            await DisplayAlert("Eliminación", "Se ha eliminado correctamente", "Ok");
        }
        else
        {
            await DisplayAlert("Error", "No se pudo eliminar los datos", "Ok");
        }
    }
}