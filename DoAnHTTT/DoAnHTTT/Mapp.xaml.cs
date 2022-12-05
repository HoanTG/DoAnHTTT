using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DoAnHTTT.ViewModel;
using Xamarin.Forms.GoogleMaps;
using Plugin.Geolocator;

namespace DoAnHTTT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapp : ContentPage
    {
        string msqa, pinX, pinY;
        MapViewModel mapViewModel = new MapViewModel();
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
            pinX = qa.X.ToString();
            pinY= qa.Y.ToString();

        }
        public Mapp()
        {
            InitializeComponent();
            ShowCurLocation();
        }
        public Mapp(QuanAn qa)
        {
            InitializeComponent();
            MarkPlace(qa);
        }
        public async void ShowCurLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Position position = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                    mapSpan = MapSpan.FromCenterAndRadius(position,Distance.FromKilometers(.444));
                    MapApp.MoveToRegion(mapSpan);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
        private async void btnthem_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.3.227/doan/api/QuanAn/ThemQuanAnYeuThich?MSQA=" + msqa);
            await DisplayAlert("Thông báo", "Thêm thành công", "Ok");
        }

        async void btnTrackpath_Clicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            var cur_location = locator.GetPositionAsync();
            string startX, startY, desX, desY;
            startX = cur_location.Latitude.ToString();
            startY = cur_location.Longitude.ToString(); 
            desX = pinX;
            desY = pinY;
            var path = await mapViewModel.LoadRoute(startX,startY,desX,desY);

            MapApp.Polylines.Clear();

            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;


            foreach (var p in path)
            {
                polyline.Positions.Add(p);

            }
            MapApp.Polylines.Add(polyline);
            Pin pin = new Pin 
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Pin",
                Address = "Pin",

            };
            MapApp.Pins.Add(pin);
            async void UpdatePostions(Position position)
            {
                if (MapApp.Pins.Count == 1 && MapApp.Polylines != null && MapApp.Polylines.Count > 1)
                    return;
                var cPin = MapApp.Pins.FirstOrDefault();

                if (cPin != null)
                {
                    cPin.Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                    MapApp.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMeters(200)));
                    var previousPosition = MapApp.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                    MapApp.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                }
                else
                {
                    MapApp.Polylines?.FirstOrDefault()?.Positions?.Clear();
                }
            }

            var positionIndex = 1;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (path.Count > positionIndex)
                {
                    UpdatePostions(path[positionIndex]);
                    positionIndex++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}