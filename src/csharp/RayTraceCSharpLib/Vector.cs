// -----------------------------------------------------------------------
// <copyright file="Vector.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RayTraceCSharpLib
{
    public class Vector
    {
        public Vector()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(Vector v)
            : this(v.X, v.Y, v.Z)
        {
        }

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public static Vector operator +(Vector v, Vector w)
        {
            return new Vector(w.X + v.X, w.Y + v.Y, w.Z + v.Z);
        }

        public static Vector operator -(Vector v, Vector w)
        {
            return new Vector(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        }

        public static Vector operator *(Vector v, Vector w)
        {
            return new Vector(v.X * w.X, v.Y * w.Y, v.Z * w.Z);
        }

        public static Vector operator *(Vector v, double f)
        {
            return new Vector(v.X * f, v.Y * f, v.Z * f);
        }

        public static Vector operator /(Vector v, double f)
        {
            return new Vector(v.X / f, v.Y / f, v.Z / f);
        }

        public double DotProduct(Vector other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }

        public Vector CrossProduct(Vector other)
        {
            return new Vector(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);
        }

        public double Magnitude()
        {
            return Math.Sqrt(MagnitudeSquared());
        }

        public double MagnitudeSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

        public Vector Normalize()
        {
            return this / Magnitude();
        }

        public override string ToString()
        {
            return $"Vector({X}, {Y}, {Z})";
        }

        public Vector DivideByDouble(double m)
        {
            return new Vector(X / m, Y / m, Z / m);
        }

        public Vector MultiplyByDouble(double m)
        {
            return new Vector(X * m, Y * m, Z * m);
        }

        public Vector Subtract(Vector other)
        {
            return new Vector(X - other.X, Y - other.Y, Z - other.Z);
        }

        public Vector Add(Vector other)
        {
            return new Vector(X + other.X, Y + other.Y, Z + other.Z);
        }

        public bool IsZero()
        {
            return (X == 0.0) && (Y == 0.0) && (Z == 0.0);
        }

        public Vector ReNormalize()
        {
            double nSq = MagnitudeSquared();
            double mFact = 1.0 - 0.5 * (nSq - 1.0); // Multiplicative factor
            return MultiplyByDouble(mFact);
        }

        public Vector Negate()
        {
            return new Vector(-X, -Y, -Z);
        }

        public Vector ArrayProd(Vector other)
        {
            return new Vector(X * other.X, Y * other.Y, Z * other.Z);
        }

        public bool NearZero(double tolerance)
        {
            return MaxAbs() < tolerance;
        }

        public double MaxAbs()
        {
            double m = (X > 0.0) ? X : -X;
            if (Y > m)
            {
                m = Y;
            }
            else if (-Y > m)
            {
                m = -Y;
            }
            if (Z > m)
            {
                m = -Z;
            }

            return m;
        }

        public Vector ClampValues(double min, double max)
        {
            return new Vector(Clamp(X, min, max), Clamp(Y, min, max), Clamp(Z, min, max));
        }

        private static double Clamp(double value, double min, double max)
        {
            if (value > max)
            {
                // Console.WriteLine($"clamping {value} to {max}");
            }
            return Math.Max(min, Math.Min(value, max));
        }

        public Vector AddScaled(Vector other, double scale)
        {
            return new Vector(X + scale * other.X, Y + scale * other.Y, Z + scale * other.Z);
        }

        /// <summary>
        /// blends two colors together.
        /// </summary>
        /// <param name="other">the other color to blend</param>
        /// <param name="weight">weight of the other color, value between [0,1]</param>
        /// <returns></returns>
        public Vector Blend(Vector other, double weight)
        {
            var result = new Vector(this);
            result = result * (1 - weight) + other * weight;
            return result;
        }

        public (byte r, byte g, byte b) ToByteColors()
        {
            return (Convert.ToByte(X * 255), Convert.ToByte(Y * 255), Convert.ToByte(Z * 255));
        }

        public static Vector FromByteColors(byte r, byte g, byte b)
        {
            return new Vector(((double)r) / 255.0, ((double)g) / 255.0, ((double)b) / 255.0);
        }
    }
}
