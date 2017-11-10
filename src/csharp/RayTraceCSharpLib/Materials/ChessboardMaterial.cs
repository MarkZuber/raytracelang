// -----------------------------------------------------------------------
// <copyright file="ChessboardMaterial.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib.Materials
{
    public class ChessboardMaterial : BaseMaterial
    {
        public ChessboardMaterial(Vector coloreven, Vector colorodd, double reflection, double transparency, double gloss, double density)
        {
            ColorEven = coloreven;
            ColorOdd = colorodd;
            Reflection = reflection;
            Transparency = transparency;
            Gloss = gloss;
            Density = density;
        }

        /// <summary>
        /// E.g. the represents the black squares on the chessboard
        /// </summary>
        public Vector ColorEven { get; }

        /// <summary>
        /// represents the white squares on the chessboard
        /// </summary>
        public Vector ColorOdd { get; }

        /// <summary>
        /// Density indicates the size of the squares and therefore
        /// the number of squares displayed
        /// the value must be > 0
        /// </summary>
        public double Density { get; }

        public override bool HasTexture => true;

        public override Vector GetColor(double u, double v)
        {
            var t = WrapUp(u) * WrapUp(v);

            if (t < 0.0)
            {
                return ColorEven;
            }
            else
            {
                return ColorOdd;
            }
        }
    }
}
