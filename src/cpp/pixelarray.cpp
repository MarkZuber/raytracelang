#include "pixelarray.h"

PixelArray::PixelArray(int width, int height)
{
    _width = width;
    _height = height;
}

PixelArray::~PixelArray()
{
}

int PixelArray::GetWidth()
{
    return _width;
}

int PixelArray::GetHeight()
{
    return _height;
}
