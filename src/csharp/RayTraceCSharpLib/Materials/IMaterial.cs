// -----------------------------------------------------------------------
// <copyright file="IMaterial.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib.Materials
{
    public interface IMaterial
    {
        /// <summary>
        /// specifies the Gloss (or shininess) of the element
        /// value must be between 1 (very shiney) and 5 (matt) for a realistic effect
        /// </summary>
        double Gloss { get; set; }

        /// <summary>
        /// defines the transparency of the element.
        /// values must be between 0 (opaque) and 1 (fully transparent);
        /// </summary>
        double Transparency { get; set; }

        /// <summary>
        /// specifies how much light the element will reflect
        /// value must be between 0 (no reflection) to 1 (total reflection/mirror)
        /// </summary>
        double Reflection { get; set; }

        /// <summary>
        /// refraction index
        /// specifies how the material will bend the light rays
        /// value must be between (0,1] (total reflection/ mirror)
        /// </summary>
        double Refraction { get; set; }

        /// <summary>
        /// indicates that the material has a texture and therefore the exact
        /// u,v coordinates are to be calculated by the element
        /// and passed on in the GetColor function
        /// </summary>
        bool HasTexture { get; }

        /// <summary>
        /// retrieves the actual color of the material
        /// the color can change depending on the u,v coordinates in the texture map
        /// </summary>
        /// <param name="u">the u coordinate in the texture</param>
        /// <param name="v">the v coordinate in the texture</param>
        /// <returns></returns>
        Vector GetColor(double u, double v);
    }
}
