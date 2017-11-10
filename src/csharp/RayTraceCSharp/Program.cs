// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;
using RayTraceCSharpLib;
using RayTraceCSharpLib.Render;

namespace RayTraceCSharp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;

            // todo: get some of these parameters from the command line
            string inputContentRoot = Path.GetFullPath(@"../../../content");
            string outputContentRoot = Path.GetFullPath(@"../../../outputContent");
            Directory.CreateDirectory(outputContentRoot);

            var data = new RayTraceRenderData(640, 640, 5, Environment.ProcessorCount, inputContentRoot);

            Console.WriteLine(data);

            Console.WriteLine("rendering...");
            string outputFilePath = Path.Combine(outputContentRoot, "csharpray.png");

            await Render(RayTracerFactory.CreateSimpleTracer(data), data, outputFilePath).ConfigureAwait(false);
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject);
            Environment.Exit(1);
        }

        private static async Task Render(IRayTracer rayTracer, RayTraceRenderData renderData, string filePath)
        {
            var pixelArray = new PixelArray(renderData.Width, renderData.Height);
            var renderer = new SceneRenderer();
            renderer.Progress += (sender, eventArgs) => Console.Write($"...{eventArgs.PercentComplete}%");
            await renderer.RayTraceSceneAsync(rayTracer.GetPixelColor, pixelArray, renderData.MaxParallelism).ConfigureAwait(false);

            pixelArray.SaveToPng(filePath);
            Console.WriteLine();
            Console.WriteLine($"Saved image to {filePath}");
        }
    }
}
