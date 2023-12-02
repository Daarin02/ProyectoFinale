using Newtonsoft.Json;
using System.Text;

using TomChino_ComidaChina.Models;

namespace TomChino_ComidaChina.Pages;

public partial class CambioContrasenaPage : ContentPage
{
    private HttpClient client = new HttpClient();
    public CambioContrasenaPage()
	{
		InitializeComponent();
	}
    private async void btnCambiarContrasena_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtener los valores ingresados por el usuario
            string name = txtUserName.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;
            string confirmarContrasena = txtConfirmarContrasena.Text;

            // Validar que las contrase�as coincidan
            if (nuevaContrasena != confirmarContrasena)
            {
                await DisplayAlert("Error", "Las contrase�as no coinciden", "OK");
                return;
            }

            // Crear un objeto User con el token y la nueva contrase�a
            User usuario = new User
            {
                Username = name,
                Password = nuevaContrasena
            };

            // Serializar el objeto User a formato JSON
            string jsonUser = JsonConvert.SerializeObject(usuario);

            // Crear el contenido de la solicitud HTTP
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // URL del endpoint para cambiar la contrase�a
            string url = "https://nutricioncarlos.azurewebsites.net/api/Cuentas/CambiarContrasena";

            // Realizar la solicitud HTTP POST para cambiar la contrase�a
            var respuesta = await client.PostAsync(url, content);

            if (respuesta.IsSuccessStatusCode)
            {
                // Contrase�a cambiada con �xito
                await DisplayAlert("�xito", "Contrase�a cambiada con �xito", "OK");

                // Puedes navegar a la p�gina de inicio de sesi�n u otra p�gina relevante
                await Navigation.PopToRootAsync(); // Por ejemplo, navegar de nuevo a la p�gina principal
            }
            else
            {
                // Error al cambiar la contrase�a
                await DisplayAlert("Error", "Error al cambiar la contrase�a", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepci�n que pueda ocurrir durante el proceso
            Console.WriteLine($"Error al cambiar la contrase�a: {ex.Message}");
        }
    }
}