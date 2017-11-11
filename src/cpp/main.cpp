#include <stdio.h>

#include "pixelarray.h"
#include "scenerenderer.h"
#include "scene.h"
#include "rectangle.h"
#include "raytracer.h"

Scene *CreateMarbleScene()
{
    return NULL;
}

int main()
{
    const int width = 640;
    const int height = 640;
    const int maxParallelism = 4;
    const int traceDepth = 5;

    PixelArray *pixelArray = new PixelArray(width, height);
    SceneRenderer *sceneRenderer = new SceneRenderer();

    Rectangle viewport(0, 0, width, height);
    Scene *scene = CreateMarbleScene();

    RayTracer *rayTracer = new RayTracer(traceDepth, scene, &viewport);
    printf("starting rendering...\n");

    sceneRenderer->RayTraceScene(rayTracer, pixelArray, maxParallelism);

    delete rayTracer;
    delete sceneRenderer;
    delete scene;
    delete pixelArray;
}