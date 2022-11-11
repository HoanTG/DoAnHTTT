using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DoAnHTTT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapp : ContentPage
    {
        void MarkPlace(QuanAn qa)
        {
            Pin pin = new Pin
            {
                Label = qa.TenQA,
                Address = "",
                Type = PinType.Place,
                Position = new Position(qa.X, qa.Y),
            };
            MapSpan mapSpan = new MapSpan(pin.Position, 0.01, 0.01);
            MapApp.MoveToRegion(mapSpan);
            MapApp.Pins.Add(pin);
            
    
        }
        public Mapp()
        {
            InitializeComponent();
        }
        public Mapp(QuanAn qa)
        {
            InitializeComponent();
            MarkPlace(qa);
        }

        private void btndsqa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DSQA());
        }
    }
}