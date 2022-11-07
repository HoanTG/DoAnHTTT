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
        void MarkPlace()
        {
            Pin pin = new Pin
            {
                Label = "Đại học công nghệ thông tin TPHCM -DHQG",
                Address = "KP6, phường Linh Trung, TP Thủ Đức",
                Type = PinType.Place,
                Position = new Position(10.8700, 106.8031)
            };
            MapApp.Pins.Add(pin);

        }
        public Map()
        {
            InitializeComponent();
            MarkPlace();
        }

        private void btndsqa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DSQA());
        }
    }
}