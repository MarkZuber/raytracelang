"""
Rendering module
"""


class RayTraceRenderData(object):
    """
    Data about how we plan to render a given scene
    """

    def __init__(self, width, height, trace_depth, max_parallelism, input_content_root):
        self.width = width
        self.height = height
        self.trace_depth = trace_depth
        self.max_parallelism = max_parallelism
        self.input_content_root = input_content_root

    def __str__(self):
        return """(Width: {}, \
Height: {}, \
TraceDepth: {}, \
MaxParallelism: {}, \
InputContentRoot: {})""".format(
    self.width,
    self.height,
    self.trace_depth,
    self.max_parallelism,
    self.input_content_root)


class SceneRenderer(object):
    """
    This runs through the input scene and traces each pixel (main logic)
    """
    def __init__(self):
        pass

    def raytrace_scene(self, calc_pixel_color_func, pixel_array, max_parallelism):
        """
        Core logic to render the scene, ideally across multiple threads
        """
        pass

class PixelArray(object):
    """
    Tracks the pixels we are rendering (raw) and knows how to convert to image format (e.g. png)
    """
    def __init__(self, width, height):
        self.widtdh = width
        self.height = height

    def save_to_png(self, path_to_png):
        """
        Saves the pixel array to a png file at path_to_png
        """
        pass
