using App4.Themes;
using Plugin.Media;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    public partial class App : Application
    {
        public static ISoapService service;
        public static OpenCVforms serviceOpencv;
        public static Ibadge badge;
        public static SmsBack sms;
        IDownloader downloader = DependencyService.Get<IDownloader>();
        public App(ISoapService service, OpenCVforms serviceOpencv, SmsBack sms)
        {
            App.service = service;
            App.serviceOpencv = serviceOpencv;
            App.sms = sms;
            InitializeComponent();
            //Application.Current.Resources= new DarkTheme();
            if (!System.IO.File.Exists(downloader.getFolder("tiles/world.mbtiles")))
            { 
            OSM.mbpath = downloader.DownloadFile("https://github.com/Mapsui/Mapsui/raw/master/Samples/Mapsui.Samples.Common/MbTiles/world.mbtiles", "tiles");
            //OSM.mbpath = downloader.getFolder("tiles/torrejon-de-ardoz.mbtiles");
            
            }

            OSM.mbpath = downloader.getFolder("tiles/world.mbtiles");
            if (!System.IO.File.Exists(downloader.getFolder("tiles/torrejon-de-ardoz.mbtiles")))
                OSM.mbpath2 = downloader.DownloadFile("https://github.com/Mapsui/Mapsui/raw/master/Samples/Mapsui.Samples.Common/MbTiles/torrejon-de-ardoz.mbtiles", "tiles");
            //OSM.mbpath = downloader.getFolder("tiles/torrejon-de-ardoz.mbtiles");
            OSM.mbpath2 = downloader.getFolder("tiles/torrejon-de-ardoz.mbtiles");

            MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
