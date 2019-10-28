using Plugin.LocalNotifications;
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
    public partial class notificacao : ContentPage
    {
        public notificacao()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("App4 titulo", "eai teste");
        }
    }
}