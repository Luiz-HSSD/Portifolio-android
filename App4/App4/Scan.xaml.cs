using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing.Imaging;
//using System.Drawing;
using System.IO;
using Android.Graphics;
using Android.Provider;
using FFImageLoading.Cache;
using FFImageLoading;

namespace App4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scan : ContentPage
    {
        public Scan()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e) => await OpenScan();

        private async Task OpenScan()
        {
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            if (!string.IsNullOrEmpty(result))
            {
                // Sua logica.
                var QrCode = result;
                output.Text = QrCode;
                Bitmap thumb =ChangeColor( GerarQRCode(300, 300, QrCode));
                //Android.Net.Uri val;
                Bitmap scaledThumb = Bitmap.CreateScaledBitmap(thumb, 300, 300, true);
                MemoryStream stream = new MemoryStream();
                scaledThumb.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                //force to reload
                imageqr.CacheDuration = TimeSpan.Zero;
                await FFImageLoading.Forms.CachedImage.InvalidateCache(imageqr.Source,FFImageLoading.Cache.CacheType.All);
                await ImageService.Instance.InvalidateCacheAsync(CacheType.All);
                //imageqr.Source.AutomationId = DateTime.Now.Ticks.ToString();
                imageqr.Source=ImageSource.FromStream(() => stream);
                //imageqr.ReloadImage();
            }
        }


        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado =  Bitmap.CreateBitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        public static  Bitmap ChangeColor(Bitmap scrBitmap)
        {
            //You can change your new color here. Red,Green,LawnGreen any..
            //System.Drawing.Color newColor = System.Drawing.Color.Yellow;
            //System.Drawing.Color actualColor;
           
            Android.Graphics.Color newColor = Android.Graphics.Color.Rgb(33,150,243);
            Android.Graphics.Color actualColor;
            //make an empty bitmap the same size as scrBitmap
            Bitmap newBitmap =  Bitmap.CreateBitmap(scrBitmap.Width, scrBitmap.Height,Android.Graphics.Bitmap.Config.Argb8888);
            for (int i = 0; i < scrBitmap.Width; i++)
            {
                for (int j = 0; j < scrBitmap.Height; j++)
                {
                    //get the pixel from the scrBitmap image
                    actualColor =new Android.Graphics.Color(scrBitmap.GetPixel(i, j));
                    // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                    if (actualColor == Android.Graphics.Color.Black)
                        newBitmap.SetPixel(i, j, newColor);
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                }
            }
            return newBitmap;
        }
    }
}