#include <stdio.h>

#include "pixelarray.h"

int main()
{
    PixelArray *pixelArray = new PixelArray(640, 640);

    printf("pixelarray created\n");

    delete pixelArray;
}