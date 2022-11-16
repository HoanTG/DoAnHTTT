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
    public partial class DSQAYT : ContentPage
    {
        async void ThemQuanAnYT(QuanAn quanAn)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.2.61/doan/api/QuanAn/ThemQuanAnYeuThich");
            var dsqa = JsonConvert.DeserializeObject<List<QuanAn>>(kq);
            lstqayt.ItemsSource = dsqa;
        }
        public DSQAYT(QuanAn quanAn)
        {
            InitializeComponent();
            ThemQuanAnYT(quanAn);
        }
    }
}