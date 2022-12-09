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
        QuanAn selecteditem;
        public QuanAn SeletedItem
        {
            get
            {
                return selecteditem;
            }
            set
            {
                if (selecteditem != value)
                {
                    selecteditem = value;
                }
            }
        }
        async void DSQuanAnYeuThich()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync("http://172.16.21.101/doan/api/QuanAn/DSQuanAnYeuThich");
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
            bool answer = await DisplayAlert("Thông báo", "Bạn có muốn xoá hay không", "Có","Không");
            if(answer)
            {
                var kq = await http.GetStringAsync("http://172.16.21.101/doan/api/QuanAn/XoaQuanYeuThich?MSQA=" + qa.MSQA);
            }
            DSQuanAnYeuThich();
        }
        private void lstqayt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selecteditem = e.CurrentSelection[0] as QuanAn;
            if (selecteditem != null)
                //DisplayAlert("Thong bao", $"{selecteditem.MSQA}", "Ok");
                Navigation.PushAsync(new Mapp(selecteditem));
        }
    }
}