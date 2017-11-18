fn main() {
    println!("Hello, world!");

    let input_content_root = String::from("~/dev/rustray/content/");
    let output_content_root = String::from("~/dev/rustray/output/");

    let width: u32 = 640;
    let height: u32 = 640;

    let render_data = RayTraceRenderData { width: width,
        height: height, trace_depth: 5, max_parallelism: 4,
        input_content_root: input_content_root };

    let pixel_array = PixelArray::new(width, height);
    let rect = Rectangle { x: 0, y: 0, width: width, height: height};
    let scene = Scene::create_marble_scene();
    let ray_tracer = RayTracer::new(render_data.trace_depth, rect, scene);

    ray_trace_scene(ray_tracer, pixel_array, render_data.max_parallelism);

}

struct Scene {

}

impl Scene {
    pub fn create_marble_scene() -> Scene {
        Scene {}
    }
}

struct Rectangle {
    x: u32,
    y: u32,
    width: u32,
    height: u32,
}

struct RayTracer {
    trace_depth: u32,
    viewport: Rectangle,
    scene: Scene
}

impl RayTracer {
    pub fn new(trace_depth: u32, viewport: Rectangle, scene: Scene) {
        RayTracer { trace_depth, viewport, scene };
    }

    pub fn get_pixel_color(&self, x: u32, y: u32) -> Color {
        Color { r: 10, g: 20, b: 30 };
    }
}

fn ray_trace_scene(ray_tracer: RayTracer, pixel_array: &PixelArray, max_parallelism: u32) {
    for x in 0..pixel_array.width {
        for y in 0..pixel_array.height {
            print!(".");
            let color = ray_tracer.get_pixel_color(x, y);
            pixel_array.set_pixel(x, y, color)
        }
    }
}

#[derive(Debug)]
struct RayTraceRenderData {
    width : u32,
    height: u32,
    trace_depth: u32,
    max_parallelism: u32,
    input_content_root: String
}

struct Color {
    r: u8,
    g: u8,
    b: u8
}

struct PixelArray {
    width: u32,
    height: u32,
    pixels: Vec<Color>
}

impl PixelArray {
    pub fn new(width: u32, height: u32) -> PixelArray {
        let num_pixels = width * height;

        let mut the_pixels: Vec<Color> = Vec::with_capacity(num_pixels as usize);
        for i in 0..num_pixels {
            the_pixels.push(Color { r: 0, g: 0, b: 0 });
        }

        PixelArray {
            width: width,
            height: height,
            pixels: the_pixels
        }
    }

    pub fn set_pixel_color(&self, x: u32, y: u32, color: Color) {
        self.pixels[y * self.width + x] = color;
    }

    pub fn save_as_png(&self, output_file_path: String) {

    }
}