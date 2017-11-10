// -----------------------------------------------------------------------
// <copyright file="PixelArray.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using ImageSharp;

namespace RayTraceCSharpLib.Render
{
    public class PixelArray
    {
        private readonly Image<Rgba32> _image;

        public PixelArray(int width, int height)
        {
            _image = new Image<Rgba32>(width, height);
        }

        public int Width => _image.Width;
        public int Height => _image.Height;

        public void SaveToPng(string filePath)
        {
            using (FileStream outStream = File.OpenWrite(filePath))
            {
                _image.SaveAsPng(outStream);
            }
        }

        private int GetPixelIndex(int x, int y)
        {
            if (x < 0 || x > (Width - 1))
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
            if (y < 0 || y > (Height - 1))
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
            return (y * Height) + x;
        }

        public void SetPixel(int x, int y, Vector color)
        {
            var result = color.ClampValues(0.0, 1.0).ToByteColors();
            _image[x, y] = new Rgba32(result.r, result.g, result.b);
        }
    }
}
