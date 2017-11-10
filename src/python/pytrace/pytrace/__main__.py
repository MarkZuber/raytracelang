#!/usr/bin/env/python

"""
RayTracer in python main
"""

import click
from colorama import Fore # , Back, Style
import rendermod
import tracermod

def DoRender(tracer, render_data, output_file_path):
    pixel_array = rendermod.PixelArray(render_data.width, render_data.height)
    renderer =rendermod.SceneRenderer()
    renderer.raytrace_scene(tracer.get_pixel_color, pixel_array, render_data.max_parallelism)

    pixel_array.save_to_png(output_file_path)
    print()
    print("Saved to {}".format(output_file_path))

@click.group()
@click.option('--debug/--no-debug', default=False)

def cli(debug):
    """
    run the cli
    """
    if debug:
        click.echo(Fore.YELLOW + 'Debug mode is on' + Fore.RESET)

@cli.command()
@click.option('--width', default=640, help='Image width.')
@click.option('--height', default=640, help='Image height.')
@click.option('--depth', default=5, help='Tracing ray depth')
@click.option('--parallel', default=4, help='Max parallelism')
@click.option('--content', default='../../../../content', help='input content root')
@click.option('--outroot', default='../../../../outputContent', help='output content root')

def render(width, height, depth, parallel, content, outroot):
    """Simple program blah blah"""

    # render_data = render.RayTraceRenderData(640, 480, 5, 2, "../../../../content/")
    render_data = rendermod.RayTraceRenderData(width, height, depth, parallel, content)
    click.echo(str(render_data))

    click.echo("rendering...")

    # todo: calc full path from outdir content root
    output_file_path = 'pytrace.png'

    DoRender(tracermod.SimpleTracer(render_data, tracermod.get_marble_balls_scene(render_data.input_content_root), tracermod.Rectangle(0, 0, render_data.width, render_data.height)), render_data, output_file_path)


if __name__ == '__main__':
    cli()
