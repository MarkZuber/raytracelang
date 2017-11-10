// -----------------------------------------------------------------------
// <copyright file="SceneFactory.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using RayTraceCSharpLib.Materials;
using RayTraceCSharpLib.Shapes;

namespace RayTraceCSharpLib
{
    public static class SceneFactory
    {
        // marble balls scene
        public static Scene MarbleBallsScene(string contentRoot)
        {
            var marbleTexture = Texture.FromFile(Path.Combine(contentRoot, "textures/marble.png"));

            var texture = new TextureMaterial(marbleTexture, 0.0, 0.0, 1, .5);

            var scene = new Scene()
            {
                Camera = new Camera(new Vector(0, 0, -15), new Vector(-.2, 0, 5), new Vector(0, 1, 0))
            };

            // setup a solid reflecting sphere
            scene.Shapes.Add(new SphereShape(new Vector(-1.5, 0.5, 0), .5, new SolidMaterial(new Vector(0, .5, .5), 0.2, 0.0, 2.0)));

            // setup sphere with a marble texture from an image
            scene.Shapes.Add(new SphereShape(new Vector(0, 0, 0), 1, texture));
            // scene.Shapes1.Add(new SphereShape(new Vector(0, 0, 0), 1, new SolidMaterial(new Vector(0.3, 0.0, 0.0), 0.7, 0.0, 3.0)));

            // setup the chessboard floor
            scene.Shapes.Add(new PlaneShape(new Vector(0.1, 0.9, -0.5).Normalize(), 1.2, new ChessboardMaterial(new Vector(0.5, 0.5, 0.5), new Vector(0, 0, 0), 0.2, 0, 1, 0.7)));

            // add two lights for better lighting effects
            scene.Lights.Add(new Light(new Vector(5, 10, -1), new Vector(0.8, 0.8, 0.8)));
            scene.Lights.Add(new Light(new Vector(-3, 5, -15), new Vector(0.8, 0.8, 0.8)));

            return scene;
        }
    }
}