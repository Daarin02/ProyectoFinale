namespace TomChino_ComidaChina.Pages;
using Newtonsoft.Json;
using System.Text;
using TomChino_ComidaChina.Models;

public partial class LoginPage : ContentPage
{
    private HttpClient client = new HttpClient();
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void btnRegistro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage());
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        string url = "https://nutricioncarlos.azurewebsites.net/api/Cuentas/login";

        User usuario = new User();
        usuario.Username = txtUserName.Text;
        usuario.Password = txtPassword.Text;

        string jsonUser = JsonConvert.SerializeObject(usuario);

        StringContent content = new StringContent(
            jsonUser, Encoding.UTF8, "application/json");

        var respuesta = await client.PostAsync(url, content);

        if (respuesta.IsSuccessStatusCode)
        {
            var tokenString = respuesta.Content.ReadAsStringAsync();

            var json = JsonConvert.DeserializeObject<UserToken>(tokenString.Result);

            await SecureStorage.SetAsync("token", json.Token);

            await Navigation.PushAsync(new MainPage(txtUserName.Text));
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
        }
    }

    private async void btnOlvidoContrasena_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CambioContrasenaPage());
    }
}