// -----------------------------------------------------------------------
// <copyright file="TextureMaterial.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RayTraceCSharpLib.Materials
{
    public class TextureMaterial : BaseMaterial
    {
        public TextureMaterial(Texture texture, double reflection, double transparency, double gloss, double density)
        {
            Reflection = reflection;
            Transparency = transparency;
            Gloss = gloss;
            Density = density;
            Texture = texture;
        }

        public double Density { get; }
        public Texture Texture { get; }

        public override bool HasTexture => true;

        public override Vector GetColor(double u, double v)
        {
            // map u, v to [0,2];
            u = WrapUp(u * Density) + 1;
            v = WrapUp(v * Density) + 1;

            // calculate exact position in texture
            var nu1 = u * Texture.Width / 2;
            var nv1 = v * Texture.Width / 2;

            // calculate fractions
            var fu = nu1 - Math.Floor(nu1);
            var fv = nv1 - Math.Floor(nv1);
            var w1 = (1 - fu) * (1 - fv);
            var w2 = fu * (1 - fv);
            var w3 = (1 - fu) * fv;
            var w4 = fu * fv;

            var nu2 = (int)(Math.Floor(nu1)) % Texture.Width;
            var nv2 = (int)(Math.Floor(nv1)) % Texture.Height;
            var nu3 = (int)(Math.Floor(nu1 + 1)) % Texture.Width;
            var nv3 = (int)(Math.Floor(nv1 + 1)) % Texture.Height;

            var c1 = Texture.ColorMap[nu2, nv2];
            var c2 = Texture.ColorMap[nu3, nv2];
            var c3 = Texture.ColorMap[nu2, nv3];
            var c4 = Texture.ColorMap[nu3, nv3];
            // Console.WriteLine($"{c1}, {c2}, {c3}, {c4}");
            var ret = c1 * w1 + c2 * w2 + c3 * w3 + c4 * w4;
            // Console.WriteLine(ret);
            return ret;
        }
    }
}
