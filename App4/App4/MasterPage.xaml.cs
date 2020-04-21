using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App4
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(NavPag, false);
            NavigationPage.SetHasBackButton(NavPag, false);
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new Maps());
            IsPresented = false;
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new MediaPage());
            IsPresented = false;
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new MainPage());
            IsPresented = false;
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {

            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new BiometricPage());
            IsPresented = false;
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new LoginSuccessPage());
            IsPresented = false;
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new notificacao());
            IsPresented = false;
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new MapsApi());
            IsPresented = false;
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new Scan());
            IsPresented = false;

        }

        private void Button_Clicked_8(object sender, EventArgs e)
        {
            App.serviceOpencv.GoOpenCV();
        }

        private async void Button_Clicked_10(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new sms());
            IsPresented = false;
        }
        private async void Button_Clicked_11(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new NFC());
            IsPresented = false;
        }
        private async void Button_Clicked_9(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new badge());
            IsPresented = false;
        }

        private async void Button_Clicked_12(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new SSH());
            IsPresented = false;
        }

        private async void Button_Clicked_13(object sender, EventArgs e)
        {
            await NavPag.PopToRootAsync();
            await NavPag.PopAsync();
            await NavPag.PushAsync(new OSM());
            IsPresented = false;
        }
    }
}
