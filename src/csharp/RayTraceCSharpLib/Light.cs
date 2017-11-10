// -----------------------------------------------------------------------
// <copyright file="Light.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace RayTraceCSharpLib
{
    public class Lights : List<Light>
    {
    }

    /// <summary>
    /// a point light
    /// </summary>
    public class Light
    {
        public Light(Vector pos, Vector color)
        {
            Position = pos;
            Color = color;

            Strength = 10;
        }

        public Vector Color { get; }
        public Vector Position { get; }
        public double Strength { get; }

        public double StrenghFromDistance(double distance)
        {
            if (distance >= Strength)
            {
                return 0;
            }

            return Math.Pow((Strength - distance) / Strength, .2);
        }

        public override string ToString()
        {
            return $"Light ({Position.X},{Position.Y},{Position.Z})";
        }
    }
}
