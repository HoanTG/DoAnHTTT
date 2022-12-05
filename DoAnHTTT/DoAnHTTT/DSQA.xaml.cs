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
                ("http://172.18.16.1/doan/api/QuanAn/DSQuanAn");
            var dsqa = JsonConvert.DeserializeObject<List<QuanAn>>(kq);
            lstqa.ItemsSource = dsqa;
            
        }

        public DSQA()
        {
            InitializeComponent();
            LayDSQuanAn();
        }

        private void lstqa_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            QuanAn qa = (QuanAn)lstqa.SelectedItem;
            Navigation.PushAsync(new Mapp(qa));
        }

    }
}