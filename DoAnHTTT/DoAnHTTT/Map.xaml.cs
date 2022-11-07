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
    public partial class Map : ContentPage
    {
        void MarkPlace(QuanAn qa)
        {
            Pin pin = new Pin
            {
                Label = qa.TenQA,
                Address = "",
                Type = PinType.Place,
                Position = new Position(10.8700, 106.8031)
            };
            MapApp.Pins.Add(pin);

        }
        public Map()
        {
            InitializeComponent();
        }
        public Map(QuanAn qa)
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