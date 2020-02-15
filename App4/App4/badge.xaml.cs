
using Badge.Plugin;
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
    public partial class badge : ContentPage
    {
        public badge()
        {

            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CrossBadge.Current.SetBadge(int.Parse(notification.Text));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            CrossBadge.Current.ClearBadge();
        }
    }
}