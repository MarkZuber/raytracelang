// -----------------------------------------------------------------------
// <copyright file="BoxShape.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using RayTraceCSharpLib.Materials;

namespace RayTraceCSharpLib.Shapes
{
    public class BoxShape : BaseShape
    {
        public BoxShape(Vector ulf, Vector lrb, IMaterial material)
        {
            UpperLeftFront = ulf;
            LowerRightBack = lrb;
            Position = (ulf + lrb) / 2;
            Material = material;
        }

        public Vector LowerRightBack { get; }
        public Vector UpperLeftFront { get; }

        public override IntersectInfo Intersect(Ray ray)
        {
            var best = new IntersectInfo()
            {
                Distance = double.MaxValue
            };
            IntersectInfo info = null;

            //check each side of the box
            // this is quite a crude implementation and can be optimized

            Vector p1 = null;
            Vector p2 = null;
            Vector p3 = null;
            if (ray.Direction.Z > 0)
            {
                //front
                p1 = UpperLeftFront;
                p2 = new Vector(LowerRightBack.X, LowerRightBack.Y, p1.Z);
                p3 = new Vector(p1.X, p2.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }
            else
            {
                //backside
                p1 = new Vector(UpperLeftFront.X, UpperLeftFront.Y, LowerRightBack.Z);
                p2 = LowerRightBack;
                p3 = new Vector(p1.X, p2.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }

            if (ray.Direction.X < 0)
            {
                //right side
                p1 = new Vector(LowerRightBack.X, UpperLeftFront.Y, UpperLeftFront.Z);
                p2 = LowerRightBack;
                p3 = new Vector(p1.X, p2.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }
            else
            {
                //left side
                p1 = UpperLeftFront;
                p2 = new Vector(UpperLeftFront.X, LowerRightBack.Y, LowerRightBack.Z);
                p3 = new Vector(p1.X, p2.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }

            if (ray.Direction.Y < 0)
            {
                //top side
                p1 = UpperLeftFront;
                p2 = new Vector(LowerRightBack.X, UpperLeftFront.Y, LowerRightBack.Z);
                p3 = new Vector(p2.X, p1.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }
            else
            {
                //bottom side
                p1 = new Vector(UpperLeftFront.X, LowerRightBack.Y, UpperLeftFront.Z);
                p2 = LowerRightBack;
                p3 = new Vector(p2.X, p1.Y, p1.Z);
                info = IntersectSlab(ray, p1, p2, p3);
                if (info.IsHit && info.Distance < best.Distance)
                {
                    best = info;
                }
            }
            return best;
        }

        private IntersectInfo IntersectSlab(Ray ray, Vector p1, Vector p2, Vector p3)
        {
            var n = (p1 - p3).CrossProduct(p2 - p3).Normalize();
            var d = n.DotProduct(p1);
            var info = new IntersectInfo();
            var vd = n.DotProduct(ray.Direction);
            if (vd == 0)
            {
                return info; // no intersection
            }

            var t = -(n.DotProduct(ray.Position) - d) / vd;

            if (t <= 0)
            {
                return info;
            }
            var hit = ray.Position + ray.Direction * t;

            if ((hit.X < p1.X || hit.X > p2.X) && (p1.X != p2.X))
            {
                return info;
            }
            if ((hit.Y < p3.Y || hit.Y > p1.Y) && (p1.Y != p2.Y))
            {
                return info;
            }
            //if ((hit.z < P1.z || hit.z > P3.z) && (P1.z != P3.z)) return info;
            if ((hit.Z < p1.Z || hit.Z > p2.Z) && (p1.Z != p2.Z))
            {
                return info;
            }

            info.Element = this;
            info.IsHit = true;
            info.Position = hit;
            info.Normal = n; // *-1;
            info.Distance = t;

            if (Material.HasTexture)
            {
                //Vector vecU = new Vector(hit.y - Position.y, hit.z - Position.z, Position.x-hit.x);
                var vecU = new Vector((p1.Y + p2.Y) / 2 - Position.Y, (p1.Z + p2.Z) / 2 - Position.Z, Position.X - (p1.X + p2.X) / 2).Normalize();
                var vecV = vecU.CrossProduct((p1 + p2) / 2 - Position).Normalize();

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
            return $"Box ({UpperLeftFront.X},{UpperLeftFront.Y},{UpperLeftFront.Z})-({LowerRightBack.X},{LowerRightBack.Y},{LowerRightBack.Z})";
        }
    }
}
