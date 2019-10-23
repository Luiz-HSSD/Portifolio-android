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
    public partial class Maps : ContentPage
    {
        public Maps()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();

        }
        public async void btn_clicked(object sender, System.EventArgs e)
        {
            try
            {
                var location = new Location(Convert.ToDouble(entryLatitude.Text),
Convert.ToDouble(entryLongitude.Text));
                var options = new MapLaunchOptions
                {
                    Name = entryNome.Text
                };
                await Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                // Não foi possivel acessar o mapa
                await DisplayAlert("Erro : ", ex.Message, "Ok");
            }
        }
    }
}