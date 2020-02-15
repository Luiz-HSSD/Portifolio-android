using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sms : ContentPage
    {
        public sms()
        {
            InitializeComponent();
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
                await DisplayAlert("sucesso", "Enviado com sucesso", "OK");
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Falhou !!!", "O envio de Sms não é suportado neste dispositivo." + ex.Message, "OK");
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                await DisplayAlert("Falhou !!!", ex.Message, "OK");
                // Other error has occurred.
            }
        }

        private async void sendsSMS_Clicked(object sender, EventArgs e)
        {
            //await SendSms(msg.Text,num.Text);
            App.sms.send(num.Text,msg.Text );
        }
    }
}