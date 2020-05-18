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
    public partial class ChartsPage : ContentPage
    {
        public ChartsPage()
        {
            InitializeComponent();
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var charts = DataCharts.CreateXamarinSample();
			this.chart1.Chart = charts[0];
			this.chart2.Chart = charts[1];
			this.chart3.Chart = charts[2];
			this.chart4.Chart = charts[3];
			this.chart5.Chart = charts[4];
			this.chart6.Chart = charts[5];
		}
	}
}