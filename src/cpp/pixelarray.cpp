#include "pixelarray.h"

PixelArray::PixelArray(int width, int height)
{
    _width = width;
    _height = height;
}

PixelArray::~PixelArray()
{
}

void PixelArray::SetPixel(int x, int y, const Vector &color)
{
}

int PixelArray::GetWidth() const
{
    return _width;
}

int PixelArray::GetHeight() const
{
    return _height;
}
