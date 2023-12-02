using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter;
using System.Drawing;
using TomChino_ComidaChina.Pages;
using Microsoft.AppCenter.Crashes;

namespace TomChino_ComidaChina
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppCenter.Start("android=cd5b0e6b-e9cf-4f4c-a8bc-559fa634cebc;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here};" +
                  "macos={Your macOS App secret here};",
                  typeof(Analytics), typeof(Crashes));

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}