using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BiometricPage : ContentPage
    {
        public BiometricPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);

            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Toque no sensor");
                if (auth.Authenticated)
                {
                    Resultado.Text = "Autenticado com sucesso! :)";
                }
                else
                {
                    Resultado.Text = "Impressão digital não reconhecida";
                }
            }
            else
            {
                await DisplayAlert("Ops", "Dispositivo não suportado", "OK");
            }
        }
    }
}
