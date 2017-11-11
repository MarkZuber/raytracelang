#ifndef _RAYTRACER_H_
#define _RAYTRACER_H_
#include "vector.h"

class Scene;
class Rectangle;

class RayTracer
{
  private:
    int _traceDepth;
    const Scene *_scene;
    const Rectangle *_viewport;

  public:
    RayTracer(int traceDepth, const Scene *scene, const Rectangle *viewport);
    ~RayTracer();

    Vector GetPixelColor(int x, int y);
};

#endif _RAYTRACER_H_