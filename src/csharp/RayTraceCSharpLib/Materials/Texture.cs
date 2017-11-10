// -----------------------------------------------------------------------
// <copyright file="Texture.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using ImageSharp;

namespace RayTraceCSharpLib.Materials
{
    public class Texture
    {
        public Texture(Vector[,] colormap)
        {
            Width = colormap.GetLength(0);
            Height = colormap.GetLength(1);
            ColorMap = colormap;
        }

        public Vector[,] ColorMap { get; }
        public int Height { get; }
        public int Width { get; }

        /// <summary>
        /// creates a 'ColorMap' of the bitmap
        /// this is basically a lookuptable, used to enhance the performance
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <summary>
        /// loads a texture image from file.
        /// </summary>
        public static Texture FromFile(string filename)
        {
            var image = Image.Load(filename);
            return FromImage(image);
        }

        public static Texture FromImage(Image<Rgba32> image)
        {
            var colorMap = new Vector[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = image[x, y];
                    var dcolor = Vector.FromByteColors(pixel.R, pixel.G, pixel.B);
                    colorMap[x, y] = dcolor;
                }
            }
            return new Texture(colorMap);
        }
    }
}
