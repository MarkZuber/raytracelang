// -----------------------------------------------------------------------
// <copyright file="SolidMaterial.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib.Materials
{
    public class SolidMaterial : BaseMaterial
    {
        private readonly Vector _color;

        public SolidMaterial(Vector color, double reflection, double transparency, double gloss)
        {
            _color = color;
            Reflection = reflection;
            Transparency = transparency;
            Gloss = gloss;
        }

        public override bool HasTexture => false;

        public override Vector GetColor(double u, double v)
        {
            return _color;
        }
    }
}
