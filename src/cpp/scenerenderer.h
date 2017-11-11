#ifndef _SCENERENDERER_H_
#define _SCENERENDERER_H_

class PixelArray;
class RayTracer;

class SceneRenderer
{
  public:
    SceneRenderer();
    ~SceneRenderer();

    void RayTraceScene(RayTracer *rayTracer, PixelArray *pixelArray, int maxParallelism);

  private:
    void RayTraceSegment(RayTracer *rayTracer, PixelArray *pixelArray, int segment, int startX, int endX);
};

#endif _SCENERENDERER_H_