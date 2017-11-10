#ifndef _PIXELARRAY_H_
#define _PIXELARRAY_H_

class PixelArray
{
  private:
    int _width;
    int _height;

  public:
    PixelArray(int width, int height);
    ~PixelArray();

    int GetWidth();
    int GetHeight();
};

#endif _PIXELARRAY_H_