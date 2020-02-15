using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace App4
{
    public class BarcodeWriter : BarcodeWriter<Android.Graphics.Bitmap>, IBarcodeWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeWriter"/> class.
        /// </summary>
        public BarcodeWriter()
        {
            Renderer = new BitmapRenderer();
        }
    }
}
