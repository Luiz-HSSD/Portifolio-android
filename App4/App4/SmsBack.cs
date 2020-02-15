using System;
using System.Collections.Generic;
using System.Text;

namespace App4
{
    public interface SmsBack
    {
        void send(String number, String message);
    }
}
