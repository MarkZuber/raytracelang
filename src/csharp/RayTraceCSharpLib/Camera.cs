// -----------------------------------------------------------------------
// <copyright file="Camera.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    public class Camera
    {
        public Camera(Vector position, Vector lookat)
            : this(position, lookat, new Vector(0, 1, 0))
        {
        }

        public Camera(Vector position, Vector lookat, Vector up)
        {
            Up = up.Normalize();
            Position = position;
            LookAt = lookat;
            Equator = LookAt.Normalize().CrossProduct(Up);
            Screen = Position + LookAt;
        }

        public Vector Equator { get; }
        public Vector LookAt { get; }
        public Vector Position { get; }
        public Vector Screen { get; }
        public Vector Up { get; }

        /// <summary>
        /// returns the ray as it passes through the viewport form the camera perspective
        /// it assumes that the viewport is scaled down to (1,1)-(-1,-1)
        /// </summary>
        /// <param name="vx">x position on the viewport must be between [-1,1]</param>
        /// <param name="vy">y position on the viewport must be between [-1,1]</param>
        /// <returns></returns>
        public Ray GetRay(double vx, double vy)
        {
            var pos = Screen - Up * vy - Equator * vx;
            var dir = pos - Position;

            var ray = new Ray(pos, dir.Normalize());
            return ray;
        }
    }
}
