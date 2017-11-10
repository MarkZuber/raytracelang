// -----------------------------------------------------------------------
// <copyright file="SceneRenderer.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RayTraceCSharpLib.Render
{
    public class SceneRenderer
    {
        private readonly object _syncObj = new object();
        private int _lastPercentageComplete = 0;

        public event EventHandler<RayTraceProgressEventArgs> Progress;

        public async Task RayTraceSceneAsync(Func<int, int, Vector> calcPixelColorFunc, PixelArray pixels, int maxParallelism = 8)
        {
            int segmentWidth = pixels.Width / maxParallelism;

            List<Task> tasks = new List<Task>();

            int columnsDrawn = 0;
            _lastPercentageComplete = 0;

            var factory = new TaskFactory();
            for (int t = 0; t < maxParallelism; t++)
            {
                var task = factory.StartNew(
                    o =>
                    {
                        int segment = (int)o;
                        int startX = segment * segmentWidth;
                        int endX = startX + segmentWidth - 1;
                        Console.WriteLine($"starting segment {segment}. StartX ({startX})  EndX ({endX})");
                        for (int i = startX; i <= endX; i++)
                        {
                            for (int j = 0; j < pixels.Height; j++)
                            {
                                var pixelColor = calcPixelColorFunc(i, j);
                                pixels.SetPixel(i, j, pixelColor);
                            }

                            bool shouldFireProgress = false;

                            lock (_syncObj)
                            {
                                columnsDrawn++;
                                int percentComplete = (columnsDrawn * 100) / pixels.Width;
                                shouldFireProgress = _lastPercentageComplete != percentComplete;
                                _lastPercentageComplete = percentComplete;
                            }

                            if (shouldFireProgress)
                            {
                                Progress?.Invoke(this, new RayTraceProgressEventArgs(_lastPercentageComplete));
                            }
                        }
                    },
                    t);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}
