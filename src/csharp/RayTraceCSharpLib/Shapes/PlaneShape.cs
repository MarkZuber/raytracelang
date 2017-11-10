// -----------------------------------------------------------------------
// <copyright file="PlaneShape.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using RayTraceCSharpLib.Materials;

namespace RayTraceCSharpLib.Shapes
{
    public class PlaneShape : BaseShape
    {
        public PlaneShape(Vector pos, double d, IMaterial material)
        {
            Position = pos;
            D = d;
            Material = material;
        }

        public PlaneShape(Vector pos, double d, Vector color, Vector oddcolor, double reflection, double transparency)
        {
            Position = pos;
            D = d;
            //Color = color;
            OddColor = oddcolor;
            //Transparency = transparency;
            //Reflection = reflection;
        }

        public double D { get; }
        public Vector OddColor { get; }

        public override IntersectInfo Intersect(Ray ray)
        {
            var info = new IntersectInfo();
            var vd = Position.DotProduct(ray.Direction);
            if (vd == 0)
            {
                return info; // no intersection
            }

            var t = -(Position.DotProduct(ray.Position) + D) / vd;

            if (t <= 0)
            {
                return info;
            }

            info.Element = this;
            info.IsHit = true;
            info.Position = ray.Position + ray.Direction * t;
            info.Normal = Position; // *-1;
            info.Distance = t;

            if (Material.HasTexture)
            {
                var vecU = new Vector(Position.Y, Position.Z, -Position.X);
                var vecV = vecU.CrossProduct(Position);

                var u = info.Position.DotProduct(vecU);
                var v = info.Position.DotProduct(vecV);
                info.Color = Material.GetColor(u, v);
            }
            else
            {
                info.Color = Material.GetColor(0, 0);
            }

            return info;
        }

        public override string ToString()
        {
            return $"Plane {Position.X}x+{Position.Y}y+{Position.Z}z+{D}=0)";
        }
    }
}
