// -----------------------------------------------------------------------
// <copyright file="SphereShape.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using RayTraceCSharpLib.Materials;

namespace RayTraceCSharpLib.Shapes
{
    /// <summary>
    /// a sphere is one of the most basic shapes you will find in any raytracer application.
    /// why? simply because it is relatively easy and quick to determine an intersection between a
    /// line (ray) and a sphere.
    /// Additionally it is ideal to try out special effects like reflection and refraction on spheres.
    /// </summary>
    public class SphereShape : BaseShape
    {
        public SphereShape(Vector pos, double r, IMaterial material)
        {
            R = r;
            Position = pos;
            Material = material;
        }

        public double R { get; }

        #region IShape Members

        /// <summary>
        /// This implementation of intersect uses the fastest ray-sphere intersection algorithm I could find
        /// on the internet.
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public override IntersectInfo Intersect(Ray ray)
        {
            var info = new IntersectInfo()
            {
                Element = this
            };
            var dst = ray.Position - Position;
            var b = dst.DotProduct(ray.Direction);
            var c = dst.DotProduct(dst) - (R * R);
            var d = b * b - c;

            if (d > 0) // yes, that's it, we found the intersection!
            {
                // Console.WriteLine("sphere intersect");
                info.IsHit = true;
                info.Distance = -b - (double)Math.Sqrt(d);
                info.Position = ray.Position + ray.Direction * info.Distance;
                info.Normal = (info.Position - Position).Normalize();

                if (Material.HasTexture)
                {
                    var vn = new Vector(0, 1, 0).Normalize(); // north pole / up
                    var ve = new Vector(0, 0, 1).Normalize(); // equator / sphere orientation
                    var vp = (info.Position - Position).Normalize(); //points from center of sphere to intersection

                    var phi = Math.Acos(-vp.DotProduct(vn));
                    var v = (phi * 2 / Math.PI) - 1;

                    var sinphi = ve.DotProduct(vp) / Math.Sin(phi);
                    sinphi = sinphi < -1 ? -1 : sinphi > 1 ? 1 : sinphi;
                    var theta = Math.Acos(sinphi) * 2 / Math.PI;

                    double u;

                    if (vn.CrossProduct(ve).DotProduct(vp) > 0)
                    {
                        u = theta;
                    }
                    else
                    {
                        u = 1 - theta;
                    }

                    // alternative but worse implementation
                    //double u = Math.Atan2(vp.x, vp.z);
                    //double v = Math.Acos(vp.y);
                    info.Color = Material.GetColor(u, v);
                    // Console.WriteLine($"uv -> {u},{v} => {info.Color}");
                }
                else
                {
                    // skip uv calculation, just get the color
                    info.Color = Material.GetColor(0, 0);
                }
            }
            else
            {
                info.IsHit = false;
            }
            return info;
        }

        #endregion

        public override string ToString()
        {
            return $"Sphere ({Position.X},{Position.Y},{Position.Z}) Radius: {R}";
        }
    }
}
