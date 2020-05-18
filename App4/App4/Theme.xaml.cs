using App4.Themes;
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
    public partial class Theme : ContentPage
    {
        public Theme()
        {
            InitializeComponent();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch picker = sender as Switch;
            bool theme = picker.IsToggled;

            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if(!theme)
                        mergedDictionaries.Add(new DarkTheme());
                else 
                        mergedDictionaries.Add(new LightTheme());
                      
                
            }
        }
    }
}