// -----------------------------------------------------------------------
// <copyright file="BaseMaterial.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib.Materials
{
    public abstract class BaseMaterial : IMaterial
    {
        protected BaseMaterial()
        {
            Gloss = 2; // set a realistic value by default
            Transparency = 0; // opaque by default
            Reflection = 0; // no reflection by default
            Refraction = 0.50; // default refraction for now
        }

        public double Reflection { get; set; }

        public double Refraction { get; set; }

        public double Transparency { get; set; }

        public double Gloss { get; set; }

        public abstract bool HasTexture { get; }
        public abstract Vector GetColor(double u, double v);

        /// <summary>
        /// wraps any value up in the inteval [-1,1] in a rotational manner
        /// e.g. 1.7 -> -0.3
        /// e.g. -1.1 -> 0.9
        /// e.g. -2.3 -> -0.3
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected double WrapUp(double t)
        {
            t = t % 2.0;
            if (t < -1)
            {
                t = t + 2.0;
            }
            if (t >= 1)
            {
                t -= 2.0;
            }
            return t;
        }
    }
}
