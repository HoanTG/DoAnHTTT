using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DoAnHTTT.ViewModel
{
    public class MapViewModel
    {
        internal async Task<List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute(string startX, string startY, string desX, string desY)
        {
            var googleDirection = await ApiDirection.ServiceClientInstance.GetDirection(
                startX, 
                startY, 
                desX, 
                desY);
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sai", "Sai", "Ok");
                return null;
            }
        }
    }
}
