using Android.Graphics;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;

namespace App4
{
    public class BitmapRenderer : IBarcodeRenderer<Bitmap>
    {
        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        /// <value>The foreground color.</value>
        public Color Foreground { get; set; }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        /// <value>The background color.</value>
        public Color Background { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapRenderer"/> class.
        /// </summary>
        public BitmapRenderer()
        {
            Foreground = Color.Black;
            Background = Color.White;
        }

        /// <summary>
        /// Renders the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="format">The format.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content)
        {
            return Render(matrix, format, content, new EncodingOptions());
        }

        /// <summary>
        /// Renders the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="format">The format.</param>
        /// <param name="content">The content.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            var width = matrix.Width;
            var height = matrix.Height;
            var pixels = new int[width * height];
            var outputIndex = 0;
            var fColor = Foreground.ToArgb();
            var bColor = Background.ToArgb();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    pixels[outputIndex] = matrix[x, y] ? fColor : bColor;
                    outputIndex++;
                }
            }
            //IntPtr  bitmap ; 
            //bitmap = System.Runtime.InteropServices.Marshal.AllocHGlobal(width * height * 4);
            //System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bitmap, pixels.Length);
            //return new Bitmap(width, height, 4 * width, System.Drawing.Imaging.PixelFormat.Format32bppArgb, bitmap);
            Bitmap  bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            bitmap.SetPixels(pixels, 0, width, 0, 0, width, height);
            return bitmap;
        }
    }
}
