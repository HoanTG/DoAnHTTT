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
            MainPage = new DoAnHTTT.MainPage();
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
