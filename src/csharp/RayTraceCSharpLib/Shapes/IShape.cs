// -----------------------------------------------------------------------
// <copyright file="IShape.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using RayTraceCSharpLib.Materials;

namespace RayTraceCSharpLib.Shapes
{
    /// <summary>
    /// element in a scene
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// indicates the position of the element
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        /// specifies the ambient and diffuse color of the element
        /// </summary>
        IMaterial Material { get; set; }

        /// <summary>
        /// this method is to be implemented by each element seperately. This is the core
        /// function of each element, to determine the intersection with a ray.
        /// </summary>
        /// <param name="ray">the ray that intersects with the element</param>
        /// <returns></returns>
        IntersectInfo Intersect(Ray ray);
    }
}
