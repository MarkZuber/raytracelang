// -----------------------------------------------------------------------
// <copyright file="Scene.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib
{
    /// <summary>
    /// a scene is defined by:
    /// - lights
    /// - a camera, of viewpoint from which the scene is observed
    /// - a background
    /// - the objects in the scene, called the shapes.
    /// </summary>
    public class Scene
    {
        public Scene()
        {
            SamplingQuality = 0;
            RenderDiffuse = true;
            RenderHighlights = true;
            RenderShadow = true;
            RenderReflection = true;
            RenderRefraction = true;

            Camera = new Camera(new Vector(0, 0, -5), new Vector(0, 0, 1));
            Shapes = new Shapes.Shapes();
            Lights = new Lights();
            Background = new Background(new Vector(0.2, 0.2, .2), 0.2);
        }

        public Background Background { get; }
        public Camera Camera { get; set; }
        public Lights Lights { get; }
        public bool RenderDiffuse { get; }
        public bool RenderHighlights { get; }
        public bool RenderReflection { get; }
        public bool RenderRefraction { get; }
        public bool RenderShadow { get; }
        public int SamplingQuality { get; }
        public Shapes.Shapes Shapes { get; }
    }
}
