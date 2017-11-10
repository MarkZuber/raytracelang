// -----------------------------------------------------------------------
// <copyright file="Ray.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    /// <summary>
    /// a virtual ray that is casted from a begin Position into a certain Direction.
    /// </summary>
    public class Ray
    {
        public Ray(Vector position, Vector direction)
        {
            Position = position;
            Direction = direction;
        }

        public Vector Direction { get; }
        public Vector Position { get; }
    }
}
