using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        string msqa;
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
            msqa = qa.MSQA;
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

        private async void btnthem_Clicked(object sender, EventArgs e)
        {
            QuanAn qa = new QuanAn();
            /*HttpClient http = new HttpClient();
            string jsonqa = JsonConvert.SerializeObject(QA);
            StringContent httcontent = new StringContent(jsonqa, Encoding.UTF8,"application/json");
            HttpResponseMessage message = await http.PostAsync("http://192.168.3.209/doan/api/QuanAn/ThemQuanAnYeuThich", httcontent);
            var kqtv = await message.Content.ReadAsStringAsync();
            await DisplayAlert("Thêm thành công", "Quán ăn đã được lưu: " + kqtv.ToString(), "Ok");*/
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.2.16/doan/api/QuanAn/ThemQuanAnYeuThich?MSQA="+msqa);
        }
    }
}