using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoAnHTTT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DSQAYT : ContentPage
    {
        async void DSQuanAnYeuThich()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync("http://172.22.80.1/doan/api/QuanAn/DSQuanAnYeuThich");
            var dsqayt = JsonConvert.DeserializeObject<List<QuanAn>>(kq);
            lstqayt.ItemsSource = dsqayt;
        }
        
        public DSQAYT()
        {
            InitializeComponent();
            DSQuanAnYeuThich();
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var SwipeItem = sender as SwipeItem;
            var qa = SwipeItem.CommandParameter as QuanAn;
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync("http://172.22.80.1/doan/api/QuanAn/XoaQuanYeuThich?MSQA=" + qa.MSQA);
            await DisplayAlert("Thông báo", "Xoá thành công", "Ok");
            DSQuanAnYeuThich();
        }

    }
}