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


namespace DoAnHTTT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapp : ContentPage
    {
        private string msqa = "";
        private string pinX = "";
        private string pinY = "";
        CancellationTokenSource cts;
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
        async void ShowCurLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request,cts.Token);
                
                if (location != null)
                {
                    Position position = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                    mapSpan = MapSpan.FromCenterAndRadius(position,Distance.FromKilometers(.444));
                    MapApp.MoveToRegion(mapSpan);
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
        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }
        private async void btnthem_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                ("http://192.168.2.33/doan/api/QuanAn/ThemQuanAnYeuThich?MSQA=" + msqa);
            await DisplayAlert("Thông báo", "Thêm thành công", "Ok");
        }

        async void btnTrackpath_Clicked(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            Position positionUser = new Position(location.Latitude, location.Longitude);
            var path = await mapViewModel.LoadRoute(positionUser.Latitude.ToString(), positionUser.Longitude.ToString(), pinX, pinY);

            MapApp.Polylines.Clear();

            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;


            for (int i = 0; i < path.Count; i++)
            {
                Position p = path[i];
                polyline.Positions.Add(p);

            }
            MapApp.Polylines.Add(polyline);
            MapApp.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.50f)));
            Pin pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Pin",
                Address = "Pin",

            };
            MapApp.Pins.Add(pin);
     

            

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
                void UpdatePostions(Position position)
                {
                    if (MapApp.Pins.Count == 1 && MapApp.Polylines != null && MapApp.Polylines.Count > 1)
                        return;
                    var cPin = MapApp.Pins.FirstOrDefault();

                    if (cPin != null)
                    {
                        cPin.Position = new Position(position.Latitude, position.Longitude);
                        MapApp.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMeters(200)));
                        var previousPosition = MapApp.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                        MapApp.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                    }
                    else
                    {
                        MapApp.Polylines?.FirstOrDefault()?.Positions?.Clear();
                    }
                }
            });
        }
    }
}