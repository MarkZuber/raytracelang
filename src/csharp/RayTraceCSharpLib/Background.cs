// -----------------------------------------------------------------------
// <copyright file="Background.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    /// <summary>
    /// defines the background of the scene
    /// for now it only supports a background color and an ambience value, for ambient lighting
    /// </summary>
    public class Background
    {
        /// <summary>
        /// specifies the background of the scene
        /// </summary>
        /// <param name="color">the color of the background</param>
        /// <param name="ambience">the ambient lighting used [0,1]</param>
        public Background(Vector color, double ambience)
        {
            Color = color;
            Ambience = ambience;
        }

        public double Ambience { get; }

        public Vector Color { get; }
    }
}
