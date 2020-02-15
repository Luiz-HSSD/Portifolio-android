using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony; // I've left SmsManager off here as it isn't required for this bit
using App4;
namespace App4.Droid
{
    class SmsAndroid:SmsBack
    {
        public static MainActivity dev;
        public void send(String number, String message)
        {
            SmsManager sms = SmsManager.GetSmsManagerForSubscriptionId(0);
            sms.SendTextMessage(number, null, message, PendingIntent.GetBroadcast(dev, 0, new Intent("SENT"), 0), null);
        }

    }
}