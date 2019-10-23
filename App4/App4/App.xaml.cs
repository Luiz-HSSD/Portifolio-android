using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    public partial class App : Application
    {
        public static ISoapService service;
        public App(ISoapService service)
        {
            App.service = service;
            InitializeComponent();
 

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
