using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoAnHTTT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new Control());
            MainPage = new NavigationPage(new Mapp());
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
