using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mapsui.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Mapsui.UI;
using System.Threading.Tasks;
using Mapsui.Rendering.Skia;
using Xamarin.Forms.Maps;
using Mapsui.Layers;
using Mapsui.Utilities;
using BruTile.Web;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OSM : ContentPage
    {
        public OSM()
        {
            InitializeComponent();
            TileLayer tileLayer = OpenStreetMap.CreateTileLayer();
            HttpTileSource tileSource = tileLayer.TileSource as HttpTileSource;
            ///tileSource.Attribution.
            mapView.Map.Layers.Add(tileLayer);
            //mapView.Rotation = false;
           // mapView.UnSnapRotationDegrees = 30;
            //mapView.ReSnapRotationDegrees = 5;

            //mapView.ev += OnPinClicked;
            //mapView.MapClicked += OnMapClicked;

            //mapView.la.UpdateMyLocation(new UI.Forms.Position());

           // mapView.Info += MapView_Info;

            Task.Run(() => StartGPS());

            //setup(mapView);

            //Clicker = c;
        }

        protected override void OnAppearing()
        {
            mapView.RefreshGraphics();
        }

        /*
        private void MapView_Info(object sender, MapInfoEventArgs e)
        {
            if (e?.MapInfo?.Feature != null)
            {
                foreach (var style in e.MapInfo.Feature.Styles)
                {
                    if (style is CalloutStyle)
                    {
                        style.Enabled = !style.Enabled;
                        e.Handled = true;
                    }
                }

                mapView.RefreshGraphics();
            }
        }*/

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            //e.IsFocused = Clicker != null ? (bool)Clicker?.Invoke(sender as MapView, e) : false;
            //Samples.SetPins(mapView, e);
            //Samples.DrawPolylines(mapView, e);
        }

        /*private void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            if (sender != null)
            {
                if (e.NumOfTaps == 2)
                {
                    // Hide Pin when double click
                    //DisplayAlert($"Pin {e.Pin.Label}", $"Is at position {e.Pin.Position}", "Ok");
                    e.Pin.IsVisible = false;
                }
                if (e.NumOfTaps == 1)
                    if (e.Pin.Callout.IsVisible)
                        e.Pin.HideCallout();
                    else
                        e.Pin.ShowCallout();
            }

            e.Handled = true;
        }*/

        public async Task StartGPS()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            // Start GPS
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(1),
                    1,
                    true,
                    new ListenerSettings
                    {
                        ActivityType = ActivityType.Fitness,
                        AllowBackgroundUpdates = false,
                        DeferLocationUpdates = true,
                        DeferralDistanceMeters = 1,
                        DeferralTime = TimeSpan.FromSeconds(0.2),
                        ListenForSignificantChanges = false,
                        PauseLocationUpdatesAutomatically = true
                    });

            CrossGeolocator.Current.PositionChanged += MyLocationPositionChanged;
            CrossGeolocator.Current.PositionError += MyLocationPositionError;
        }

        public async Task StopGPS()
        {
            // Stop GPS
            if (CrossGeolocator.Current.IsListening)
            {
                await CrossGeolocator.Current.StopListeningAsync();
            }
        }

        /// <summary>
        /// If there was an error while getting GPS coordinates
        /// </summary>
        /// <param name="sender">Geolocator</param>
        /// <param name="e">Event arguments for position error</param>
        private void MyLocationPositionError(object sender, PositionErrorEventArgs e)
        {
        }

        /// <summary>
        /// New informations from Geolocator arrived
        /// </summary>
        /// <param name="sender">Geolocator</param>
        /// <param name="e">Event arguments for new position</param>
        private void MyLocationPositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var coords = new Plugin.Geolocator.Abstractions.Position(e.Position.Latitude, e.Position.Longitude);
                //info.Text = $"{coords.ToString()} - D:{(int)e.Position.Heading} S:{Math.Round(e.Position.Speed, 2)}";

                //mapView..UpdateMyLocation(new UI.Forms.Position(e.Position.Latitude, e.Position.Longitude));
                //mapView.MyLocationLayer.UpdateMyDirection(e.Position.Heading, mapView.Viewport.Rotation);
                //mapView.MyLocationLayer.UpdateMySpeed(e.Position.Speed);
            });
        }
    }
}
