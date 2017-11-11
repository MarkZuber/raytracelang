#ifndef _PIXELARRAY_H_
#define _PIXELARRAY_H_

#include "vector.h"

class PixelArray
{
  private:
    int _width;
    int _height;

  public:
    PixelArray(int width, int height);
    ~PixelArray();

    void SetPixel(int x, int y, const Vector &color);

    int GetWidth() const;
    int GetHeight() const;
};

#endif _PIXELARRAY_H_