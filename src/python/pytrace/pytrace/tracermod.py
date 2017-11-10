"""
Ray Tracer module
"""

def get_marble_balls_scene(input_content_root):
    """
    Constructs a Scene based on content in input_content_root
    """
    return input_content_root


class Rectangle(object):
    """
    Rectangle class
    """
    def __init__(self, xcoord, ycoord, width, height):
        self.xcoord = xcoord
        self.ycoord = ycoord
        self.width = width
        self.height = height

class SimpleTracer(object):
    """
    Simple ray tracer class that knows how to get the color for a pixel given a scene
    """
    def __init__(self, trace_depth, scene, viewport):
        pass

    def get_pixel_color(self, xcoord, ycoord):
        """
        returns: 3d vector of the rgb values of the pixel
        """
        pass
