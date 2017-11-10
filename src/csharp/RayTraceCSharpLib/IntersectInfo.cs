// -----------------------------------------------------------------------
// <copyright file="IntersectInfo.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using RayTraceCSharpLib.Shapes;

namespace RayTraceCSharpLib
{
    /// <summary>
    /// this is a data class that stores all relevant information about a ray-shape intersection
    /// this information is to be filled in by every custom implemented shape type in the Intersect method.
    /// this information is used to determine the color at the intersection point
    /// </summary>
    public class IntersectInfo
    {
        public Vector Color { get; set; }
        public double Distance { get; set; }
        public IShape Element { get; set; }
        public int HitCount { get; set; }
        public bool IsHit { get; set; }
        public Vector Normal { get; set; }
        public Vector Position { get; set; }
    }
}
