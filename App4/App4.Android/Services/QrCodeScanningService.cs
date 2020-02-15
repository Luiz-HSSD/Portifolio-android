using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

[assembly: Dependency(typeof(App4.Droid.Services.QrCodeScanningService))]
namespace App4.Droid.Services
{

    public class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                // DisableAutofocus=true,
                //CameraResolutionSelector=new CameraResolution()
                //UseFrontCameraIfAvailable = true
            };
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime a câmera do código de barra ou QR",
                BottomText = "Toque na tela para focar"
            };

            var scanResults = await scanner.Scan(optionsCustom);

            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}