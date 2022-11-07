using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoAnHTTT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DSQA : ContentPage
    {
        async void LayDSQuanAn()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.2.24/doan/api/QuanAn/DSQuanAn");
            var dsqa = JsonConvert.DeserializeObject<List<QuanAn>>(kq);
            lstqa.ItemsSource = dsqa;
        }
        public DSQA()
        {
            InitializeComponent();
            LayDSQuanAn();
        }
    }
}