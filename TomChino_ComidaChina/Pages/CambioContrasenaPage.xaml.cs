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

            // Validar que las contraseñas coincidan
            if (nuevaContrasena != confirmarContrasena)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            // Crear un objeto User con el token y la nueva contraseña
            User usuario = new User
            {
                Username = name,
                Password = nuevaContrasena
            };

            // Serializar el objeto User a formato JSON
            string jsonUser = JsonConvert.SerializeObject(usuario);

            // Crear el contenido de la solicitud HTTP
            StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // URL del endpoint para cambiar la contraseña
            string url = "https://nutricioncarlos.azurewebsites.net/api/Cuentas/CambiarContrasena";

            // Realizar la solicitud HTTP POST para cambiar la contraseña
            var respuesta = await client.PostAsync(url, content);

            if (respuesta.IsSuccessStatusCode)
            {
                // Contraseña cambiada con éxito
                await DisplayAlert("Éxito", "Contraseña cambiada con éxito", "OK");

                // Puedes navegar a la página de inicio de sesión u otra página relevante
                await Navigation.PopToRootAsync(); // Por ejemplo, navegar de nuevo a la página principal
            }
            else
            {
                // Error al cambiar la contraseña
                await DisplayAlert("Error", "Error al cambiar la contraseña", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante el proceso
            Console.WriteLine($"Error al cambiar la contraseña: {ex.Message}");
        }
    }
}