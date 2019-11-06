using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App4
{
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }
}
