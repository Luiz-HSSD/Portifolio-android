using Xamarin.Forms;

namespace App4
{
    public static class Constants
    {
        // URL of ASMX service
        public static string SoapUrl
        {
            get
            {
                var defaultUrl = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx";

                if (Device.RuntimePlatform == Device.Android)
                {
                    defaultUrl = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx";
                }

                return defaultUrl;
            }
        }
    }
}
