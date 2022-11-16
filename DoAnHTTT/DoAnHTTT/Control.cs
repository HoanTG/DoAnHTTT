using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DoAnHTTT
{
    public class Control
    {
        async void ThemQuanAnYT(QuanAn quanAn)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.2.61/doan/api/QuanAn/ThemQuanAnYeuThich");
            var dsqa = JsonConvert.DeserializeObject<List<QuanAn>>(kq);
        }
        public Control(QuanAn quan)
        {
            ThemQuanAnYT(quan);
        }
    }
}
