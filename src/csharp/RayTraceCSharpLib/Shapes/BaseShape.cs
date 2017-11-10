// -----------------------------------------------------------------------
// <copyright file="BaseShape.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using RayTraceCSharpLib.Materials;

namespace RayTraceCSharpLib.Shapes
{
    public class Shapes : List<IShape>
    {
    }

    public abstract class BaseShape : IShape
    {
        protected BaseShape()
        {
            Position = new Vector(0, 0, 0);
            Material = new SolidMaterial(new Vector(1, 0, 1), 0, 0, 0);
        }

        public IMaterial Material { get; set; }

        public Vector Position { get; set; }

        public abstract IntersectInfo Intersect(Ray ray);
    }
}
