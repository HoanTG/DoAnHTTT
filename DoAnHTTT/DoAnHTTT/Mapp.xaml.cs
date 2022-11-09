using System;
using System.Collections.Generic;
using System.Linq;
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
                Position = new Position(qa.X, qa.Y)
            };
            MapApp.Pins.Add(pin);
        }
        void CenterPoint(float x, float y)
        {
            Position position = new Position(x, y);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            MapApp = new Map(mapSpan);
        }
        public Mapp()
        {
            InitializeComponent();
            CenterPoint(0,0);
        }
        public Mapp(QuanAn qa)
        {
            InitializeComponent();
            MarkPlace(qa);
            CenterPoint(qa.X, qa.Y);
        }

        private void btndsqa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DSQA());
        }
    }
}