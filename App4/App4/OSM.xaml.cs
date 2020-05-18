using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Mapsui.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Mapsui.UI;
using System.Threading.Tasks;
using Mapsui.Rendering.Skia;
using Xamarin.Forms.Maps;
using Mapsui.Layers;
using Mapsui.Utilities;
using BruTile.Web;
using System.Linq;
using Mapsui.Geometries;
using Mapsui.Projection;
using Mapsui.Styles;
using System.Reflection;
using System.Collections.Generic;
using Mapsui.Providers;
using Mapsui.Styles;
using MultiLineString = Mapsui.Geometries.MultiLineString;
using Polygon = Mapsui.Geometries.Polygon;
using Point = Mapsui.Geometries.Point;
using Color = Mapsui.Styles.Color;
using System.IO;
//using BruTile.MbTiles;
using BruTile.Predefined;
using SQLite;
using BruTile;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
//using Android.Views;


namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OSM : ContentPage
    {
        
           

            private static MemoryLayer CreatePointLayer()
            {
            return new MemoryLayer
            {
                Name = "Points",
                DataSource = new MemoryProvider(GetCitiesFromEmbeddedResource()),
                Style = new SymbolStyle() 
                };
            }

           
       
        private static MemoryLayer CreateRandomPointLayer()
        {
            var rnd = new Random(3462); // Fix the random seed so the features don't move after a refresh
            var features = new Features();
            for (var i = 0; i < 100; i++)
            {
                var feature = new Feature
                {
                    Geometry = new Mapsui.Geometries.Point(rnd.Next(0, 5000000), rnd.Next(0, 5000000))
                };
                features.Add(feature);
            }
            var provider = new MemoryProvider(features);

            return new MemoryLayer
            {
                DataSource = provider,
                Style = new SymbolStyle
                {
                    SymbolType = SymbolType.Triangle,
                    Fill = new Brush(Color.Red)
                }
            };
        }

        public static ILayer CreateLayer()
        {
            return new Layer("Polygons")
            {
                DataSource = new MemoryProvider(CreatePolygon()),
                Style = new VectorStyle
                {
                    Fill =null, // new Brush(new Color(150, 150, 30, 128)),
                    Outline = new Pen
                    {
                        Color = Color.Orange,
                        Width = 2,
                        PenStyle = PenStyle.Solid,//.Solid,
                        //PenStrokeCap = PenStrokeCap.
                    }
                }
            };
        }

        private static List<Polygon> CreatePolygon()
        {
            var result = new List<Polygon>();

            var polygon = new Polygon();
            polygon.ExteriorRing.Vertices.Add(new Point(0, 0));
            polygon.ExteriorRing.Vertices.Add(new Point(0, 10000000));
            polygon.ExteriorRing.Vertices.Add(new Point(10000000, 10000000));
            polygon.ExteriorRing.Vertices.Add(new Point(10000000, 0));
            polygon.ExteriorRing.Vertices.Add(new Point(0, 0));
            var linearRing = new LinearRing();
            linearRing.Vertices.Add(new Point(1000000, 1000000));
            linearRing.Vertices.Add(new Point(9000000, 1000000));
            linearRing.Vertices.Add(new Point(9000000, 9000000));
            linearRing.Vertices.Add(new Point(1000000, 9000000));
            linearRing.Vertices.Add(new Point(1000000, 1000000));
            polygon.InteriorRings.Add(linearRing);

            result.Add(polygon);

            polygon = new Polygon();
            polygon.ExteriorRing.Vertices.Add(new Point(-10000000, 0));
            polygon.ExteriorRing.Vertices.Add(new Point(-15000000, 5000000));
            polygon.ExteriorRing.Vertices.Add(new Point(-10000000, 10000000));
            polygon.ExteriorRing.Vertices.Add(new Point(-5000000, 5000000));
            polygon.ExteriorRing.Vertices.Add(new Point(-10000000, 0));
            /*
            
            linearRing = new LinearRing();            
            linearRing.Vertices.Add(new Point(-10000000, 1000000));
            linearRing.Vertices.Add(new Point(-6000000, 5000000));
            linearRing.Vertices.Add(new Point(-10000000, 9000000));
            linearRing.Vertices.Add(new Point(-14000000, 5000000));
            linearRing.Vertices.Add(new Point(-10000000, 1000000));
            polygon.InteriorRings.Add(linearRing);
            \
             */
            result.Add(polygon);

            return result;
        }
        public static ILayer CreateLineStringLayer(IStyle style = null)
        {
            var lineString = (LineString)Geometry.GeomFromText(WKTGr5);
            lineString = new LineString(lineString.Vertices.Select(v => SphericalMercator.FromLonLat(v.Y, v.X)));

            return new MemoryLayer
            {
                DataSource = new MemoryProvider(new Feature { Geometry = lineString }),
                Name = "LineStringLayer",
                Style = CreateLineStringStyle()
            };
        }

        public static IStyle CreateLineStringStyle()
        {
            return new VectorStyle
            {
                Fill = null,
                Outline = null,
                Line = { Color = Color.FromString("Red"), Width = 4 }
            };
        }


        private const string WKTGr5 = "LINESTRING(-23.5233122 -46.3553743, -23.5243122 -46.3553743, -23.5243122 -46.3543743)";

        private static IEnumerable<IFeature> GetCitiesFromEmbeddedResource()
            {
            ///var path = "Mapsui.Samples.Common.EmbeddedResources.congo.json";
            //var assembly = typeof(PointsSample).GetTypeInfo().Assembly;
            //var stream = assembly.GetManifestResourceStream(path);
            //var cities = DeserializeFromStream<City>(stream);
            var b = new List<IFeature>();
                //return cities.Select(c =>
                //{
                    var feature = new Feature();
                    var point = SphericalMercator.FromLonLat(-46.3553743, -23.5243122 );
                    feature.Geometry = point;
                    feature["name"] ="dev";
                    feature["country"] ="poá";
                    b.Add(feature);
                    //return feature;
                    return b;
                //});
            }

            

            
            //private static SymbolStyle CreateBitmapStyle()
            //{
                // For this sample we get the bitmap from an embedded resouce
                // but you could get the data stream from the web or anywhere
                // else.
              //  var path = "Mapsui.Samples.Common.Images.home.png"; // Designed by Freepik http://www.freepik.com
               // var bitmapId = GetBitmapIdForEmbeddedResource(path);
                //var bitmapHeight = 176; // To set the offset correct we need to know the bitmap height
                //return new SymbolStyle { BitmapId = bitmapId, SymbolScale = 0.20, SymbolOffset = new Offset(0, bitmapHeight * 0.5) };
            //}

            private static int GetBitmapIdForEmbeddedResource(string imagePath)
            {
                var assembly = typeof(OSM).GetTypeInfo().Assembly;
                var image = assembly.GetManifestResourceStream(imagePath);
                return BitmapRegistry.Instance.Register(image);
            }
        
        public OSM()
        {
            InitializeComponent();
            mapView.Map = CreateMap();


            //mapView.Map.Layers.Add(CreateLineStringLayer());

            //mapView.Map.Viewport.Center = new Mapsui.Geometries.Point(-46.4, -23.2);
            //mapView.Rotation = false;
            // mapView.UnSnapRotationDegrees = 30;
            //mapView.ReSnapRotationDegrees = 5;

            //mapView.ev += OnPinClicked;
            //mapView.MapClicked += OnMapClicked;

            //mapView.la.UpdateMyLocation(new UI.Forms.Position());

            // mapView.Info += MapView_Info;

            //var abc = Task.Run(() => StartGPS());

            //setup(mapView);

            //Clicker = c;
        }
        public static Mapsui.Map CreateMap()
        {
            var map = new Mapsui.Map();
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            var b = CreateMbTilesLayer(mbpath);
            if (b != null)
                map.Layers.Add(b);
            var c = CreateMbTilesLayer(mbpath2);
            if (c != null)
                map.Layers.Add(c);

            // Get the lon lat coordinates from somewhere (Mapsui can not help you there)
            var centerOfLondonOntario = new Point(-81.2497, 42.9837);
            // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
            var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfLondonOntario.X, centerOfLondonOntario.Y);
            // Set the center of the viewport to the coordinate. The UI will refresh automatically
            // Additionally you might want to set the resolution, this could depend on your specific purpose
            
            map.Layers.Add(CreatePointLayer());
            map.Layers.Add(CreateLayer());
            map.Layers.Add(CreateLineStringLayer());
            map.Layers.Add(new RasterizingLayer(CreateRandomPointLayer()));//, pixelDensity: pixelDensity));
            //map.Layers.Add(CreateMbTilesLayer());

            //mapView.Map.Layers.Add(CreateLineStringLayer());

            //mapView.Map.Viewport.Center = new Mapsui.Geometries.Point(-46.4, -23.2);
            //mapView.Rotation = false;
            // mapView.UnSnapRotationDegrees = 30;
            //mapView.ReSnapRotationDegrees = 5;

            //mapView.ev += OnPinClicked;
            //mapView.MapClicked += OnMapClicked;

            //mapView.la.UpdateMyLocation(new UI.Forms.Position());

            // mapView.Info += MapView_Info;

            //var abc = Task.Run(() => StartGPS());

            //setup(mapView);

            //Clicker = c;
            map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, map.Resolutions[9]);

            return map;
        }
        public static  string mbpath;
        public static  string mbpath2;
    public static TileLayer CreateMbTilesLayer(string path)
    {
            TileLayer mbTilesLayer = null;
            try
            {
                //DisplayAlert("test", File.ReadAllText(mbpath), "ok");
                var mbTilesTileSource = new BruTile.MbTiles.MbTilesTileSource(new SQLiteConnectionString(path, true));
                mbTilesLayer = new TileLayer(mbTilesTileSource);
                return (mbTilesLayer);
            }
            catch (Exception ex)
            {
                return (mbTilesLayer);
            }
        }

    protected override async void OnAppearing()
        {
            // mapView.RefreshGraphics();
            //           //CreateMbTilesLayer();
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
                //mapView.Map.Viewport.Resolution = 1;
                //= new Mapsui.Geometries.Point( -46.38,-23.32);
                var coord = SphericalMercator.FromLonLat(coords.Longitude, coords.Latitude);
                var map = mapView.Map;
                mapView.Map.Home = n => n.NavigateTo(coord, map.Resolutions[9]); 
                //mapView.Map.Viewport.Rotation = coords.Heading;
                //mapView..UpdateMyLocation(new UI.Forms.Position(e.Position.Latitude, e.Position.Longitude));
                //mapView.MyLocationLayer.UpdateMyDirection(e.Position.Heading, mapView.Viewport.Rotation);
                //mapView.MyLocationLayer.UpdateMySpeed(e.Position.Speed);
            });
        }
    }
}
