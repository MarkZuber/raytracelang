#include "raytracer.h"
#include "pixelarray.h"
#include "scenerenderer.h"

SceneRenderer::SceneRenderer()
{
}

SceneRenderer::~SceneRenderer()
{
}

void SceneRenderer::RayTraceScene(RayTracer *rayTracer, PixelArray *pixelArray, int maxParallelism)
{
    // todo: break this up into threads to call RayTraceSegment once per thread based on maxParallelism

    RayTraceSegment(rayTracer, pixelArray, 0, 0, pixelArray->GetWidth() - 1);
}

void SceneRenderer::RayTraceSegment(RayTracer *rayTracer, PixelArray *pixelArray, int segment, int startX, int endX)
{
    for (int i = startX; i <= endX; i++)
    {
        for (int j = 0; j < pixelArray->GetHeight(); j++)
        {
            Vector pixelColor = rayTracer->GetPixelColor(i, j);
            pixelArray->SetPixel(i, j, pixelColor);
        }

        bool shouldFireProgress = false;

        // lock(_syncObj)
        // {
        // columnsDrawn++;
        // int percentComplete = (columnsDrawn * 100) / pixels.Width;
        // shouldFireProgress = _lastPercentageComplete != percentComplete;
        // _lastPercentageComplete = percentComplete;
        // }

        if (shouldFireProgress)
        {
            // Progress ?.Invoke(this, new RayTraceProgressEventArgs(_lastPercentageComplete));
        }
    }
}