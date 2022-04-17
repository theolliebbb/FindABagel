using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Food.Views;
[assembly: ExportFont("Aurella.ttf", Alias = "font")]
[assembly: ExportFont("OpenSans-VariableFont_wdth,wght.ttf", Alias = "open")]
[assembly: ExportFont("GrapeNuts-Regular.ttf", Alias = "gra")]
namespace Food
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

           
            MainPage = new NavigationPage (new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
