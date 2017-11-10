// -----------------------------------------------------------------------
// <copyright file="IRayTracer.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    public interface IRayTracer
    {
        Vector GetPixelColor(int x, int y);
    }
}