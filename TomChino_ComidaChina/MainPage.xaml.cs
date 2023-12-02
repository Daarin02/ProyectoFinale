using System.Security.Cryptography.X509Certificates;
using TomChino_ComidaChina.Pages;

namespace TomChino_ComidaChina
{
    public partial class MainPage : ContentPage
    {
        private string usuario;

        public MainPage(string user)
        {
            
            InitializeComponent();
            usuario = user;
        }

        public async void  btnOrdenar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlacesPage(usuario));
        }
            
        public async void btnOrders_Clicked(object sender, EventArgs e)   
        {
            await Navigation.PushAsync(new OrdersPage(usuario));
        }

    }
}