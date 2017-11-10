// -----------------------------------------------------------------------
// <copyright file="VectorR2.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    public class VectorR2
    {
        public VectorR2()
        {
            X = 0.0;
            Y = 0.0;
        }

        public VectorR2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }
}