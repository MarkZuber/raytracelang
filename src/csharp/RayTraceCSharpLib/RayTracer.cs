// -----------------------------------------------------------------------
// <copyright file="RayTracer.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;
using RayTraceCSharpLib.Shapes;

namespace RayTraceCSharpLib
{
    public class SimpleRayTracer : IRayTracer
    {
        private readonly int _traceDepth;

        public SimpleRayTracer(int traceDepth, Scene scene, Rectangle viewport)
        {
            _traceDepth = traceDepth;
            Scene = scene;
            Viewport = viewport;
        }

        public Rectangle Viewport { get; }

        public Scene Scene { get; }

        public Vector GetPixelColor(int x, int y)
        {
            var xd = Convert.ToDouble(x);
            var yd = Convert.ToDouble(y);

            // this will trigger the raytracing algorithm
            if (Scene.SamplingQuality == 0)
            {
                var xp = xd / (double)Viewport.Width * 2.0 - 1.0;
                var yp = yd / (double)Viewport.Height * 2.0 - 1.0;

                var ray = Scene.Camera.GetRay(xp, yp);
                return CalculateColor(ray, Scene);
            }
            else
            {
                VectorR2[] samples = GetRgssOffsets(Scene.SamplingQuality);
                var colors = new Vector[samples.Length];
                for (var i = 0; i < samples.Length; ++i)
                {
                    var xp = (xd + samples[i].X) / Viewport.Width * 2 - 1;
                    var yp = (yd + samples[i].Y) / Viewport.Height * 2 - 1;

                    var ray = Scene.Camera.GetRay(xp, yp);
                    colors[i] = CalculateColor(ray, Scene);
                }
                return Blend(colors);
            }
        }

        public static VectorR2[] GetRgssOffsets(int quality)
        {
            int sampleCount = quality * quality;
            VectorR2[] samplesArray = new VectorR2[sampleCount];

            GetRgssOffsets(samplesArray, sampleCount, quality);

            return samplesArray;
        }

        public static void GetRgssOffsets(VectorR2[] samplesArray, int sampleCount, int quality)
        {
            if (sampleCount < 1)
            {
                throw new ArgumentOutOfRangeException("sampleCount", "sampleCount must be [0, int.MaxValue]");
            }

            if (sampleCount != quality * quality)
            {
                throw new ArgumentOutOfRangeException("sampleCount != (quality * quality)");
            }

            if (sampleCount == 1)
            {
                samplesArray[0] = new VectorR2(0.0, 0.0);
            }
            else
            {
                for (int i = 0; i < sampleCount; ++i)
                {
                    double y = (i + 1d) / (sampleCount + 1d);
                    double x = y * quality;

                    x -= (int)x;

                    samplesArray[i] = new VectorR2((double)((double)x - 0.5d), (double)((double)y - 0.5d));
                }
            }
        }

        public static Vector Blend(Vector[] colors)
        {
            if (colors.Length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(colors));
            }

            if (colors.Length == 0)
            {
                return new Vector(1, 1, 1); // white
            }

            double r = 0;
            double g = 0;
            double b = 0;

            double rSum = 0;
            double gSum = 0;
            double bSum = 0;

            foreach (Vector t in colors)
            {
                rSum += t.X;
                gSum += t.Y;
                bSum += t.Z;
            }

            b = (byte)(bSum / colors.Length);
            g = (byte)(gSum / colors.Length);
            r = (byte)(rSum / colors.Length);

            return new Vector(r, g, b);
        }

        /// <summary>
        /// this implementation is used for debugging purposes.
        /// the color is calculated following the normal raytrace procedure
        /// execpt it is calculated for 1 particula ray
        /// </summary>
        /// <param name="ray">the ray for which to calculate the color</param>
        /// <param name="scene">the scene which is raytraced</param>
        /// <returns></returns>
        public Vector CalculateColor(Ray ray, Scene scene)
        {
            var info = TestIntersection(ray, scene, null);
            if (info.IsHit)
            {
                // execute the actual raytrace algorithm
                var c = RayTrace(info, ray, scene, 0);
                return c;
            }

            return scene.Background.Color;
        }

        /// <summary>
        /// This is the main RayTrace controller algorithm, the core of the RayTracer
        /// recursive method setup
        /// this does the actual tracing of the ray and determines the color of each pixel
        /// supports:
        /// - ambient lighting
        /// - diffuse lighting
        /// - Gloss lighting
        /// - shadows
        /// - reflections
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ray"></param>
        /// <param name="scene"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private Vector RayTrace(IntersectInfo info, Ray ray, Scene scene, int depth)
        {
            // calculate ambient light
            var color = info.Color * scene.Background.Ambience;
            // Console.WriteLine($"color ({color}) from info.color({info.Color})");
            var shininess = Math.Pow(10, info.Element.Material.Gloss + 1);

            foreach (var light in scene.Lights)
            {
                // calculate diffuse lighting
                var v = (light.Position - info.Position).Normalize();

                if (scene.RenderDiffuse)
                {
                    var l = v.DotProduct(info.Normal);
                    if (l > 0.0f)
                    {
                        color += info.Color * light.Color * l;
                    }
                }

                // this is the max depth of raytracing.
                // increasing depth will calculate more accurate color, however it will
                // also take longer (exponentially)
                if (depth < _traceDepth)
                {
                    // calculate reflection ray
                    if (scene.RenderReflection && info.Element.Material.Reflection > 0)
                    {
                        var reflectionray = GetReflectionRay(info.Position, info.Normal, ray.Direction);
                        var refl = TestIntersection(reflectionray, scene, info.Element);
                        if (refl.IsHit && refl.Distance > 0)
                        {
                            // recursive call, this makes reflections expensive
                            refl.Color = RayTrace(refl, reflectionray, scene, depth + 1);
                        }
                        else // does not reflect an object, then reflect background color
                        {
                            refl.Color = scene.Background.Color;
                        }
                        color = color.Blend(refl.Color, info.Element.Material.Reflection);
                    }

                    //calculate refraction ray
                    if (scene.RenderRefraction && info.Element.Material.Transparency > 0)
                    {
                        var refractionray = GetRefractionRay(info.Position, info.Normal, ray.Direction, info.Element.Material.Refraction);
                        var refr = info.Element.Intersect(refractionray);
                        if (refr.IsHit)
                        {
                            //refractionray = new Ray(refr.Position, ray.Direction);
                            refractionray = GetRefractionRay(refr.Position, refr.Normal, refractionray.Direction, refr.Element.Material.Refraction);
                            refr = TestIntersection(refractionray, scene, info.Element);
                            if (refr.IsHit && refr.Distance > 0)
                            {
                                // recursive call, this makes refractions expensive
                                refr.Color = RayTrace(refr, refractionray, scene, depth + 1);
                            }
                            else
                            {
                                refr.Color = scene.Background.Color;
                            }
                        }
                        else
                        {
                            refr.Color = scene.Background.Color;
                        }
                        color = color.Blend(refr.Color, info.Element.Material.Transparency);
                    }
                }

                var shadow = new IntersectInfo();
                if (scene.RenderShadow)
                {
                    // calculate shadow, create ray from intersection point to light
                    var shadowray = new Ray(info.Position, v);

                    // find any element in between intersection point and light
                    shadow = TestIntersection(shadowray, scene, info.Element);
                    if (shadow.IsHit && shadow.Element != info.Element)
                    {
                        // only cast shadow if the found interesection is another
                        // element than the current element
                        color *= 0.5 + 0.5 * Math.Pow(shadow.Element.Material.Transparency, 0.5); // Math.Pow(.5, shadow.HitCount);
                    }
                }

                // only show highlights if it is not in the shadow of another object
                if (scene.RenderHighlights && !shadow.IsHit && info.Element.Material.Gloss > 0)
                {
                    // only show Gloss light if it is not in a shadow of another element.
                    // calculate Gloss lighting (Phong)
                    var lv = (info.Element.Position - light.Position).Normalize();
                    var e = (scene.Camera.Position - info.Element.Position).Normalize();
                    var h = (e - lv).Normalize();

                    var glossweight = 0.0;
                    glossweight = Math.Pow(Math.Max(info.Normal.DotProduct(h), 0), shininess);
                    color += light.Color * (glossweight);
                }
            }

            return color;
        }

        /// <summary>
        /// this method tests for an intersection. It will try to find the closest
        /// object that intersects with the ray.
        /// it will inspect every object in the scene. also here there is room for increased performance.
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="scene"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        private IntersectInfo TestIntersection(Ray ray, Scene scene, IShape exclude)
        {
            var hitcount = 0;
            var best = new IntersectInfo()
            {
                Distance = double.MaxValue
            };
            foreach (var elt in scene.Shapes)
            {
                if (elt == exclude)
                {
                    continue;
                }

                var info = elt.Intersect(ray);
                if (info.IsHit && info.Distance < best.Distance && info.Distance >= 0)
                {
                    best = info;
                    hitcount++;
                }
            }
            best.HitCount = hitcount;
            return best;
        }

        /// <summary>
        /// some helper functions to calculate the reflection rays
        /// </summary>
        /// <param name="p"></param>
        /// <param name="n"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private Ray GetReflectionRay(Vector p, Vector n, Vector v)
        {
            var c1 = -n.DotProduct(v);
            var rl = v + (n * 2 * c1);
            return new Ray(p, rl);
        }

        /// <summary>
        /// some helper functions to calculate the refraction rays
        /// </summary>
        /// <param name="p"></param>
        /// <param name="n"></param>
        /// <param name="v"></param>
        /// <param name="refraction"></param>
        /// <returns></returns>
        private Ray GetRefractionRay(Vector p, Vector n, Vector v, double refraction)
        {
            //V = V * -1;
            //double n = -0.55; // refraction constant for now
            //if (n < 0 || n > 1) return new Ray(P, V); // no refraction

            var c1 = n.DotProduct(v);
            var c2 = 1 - refraction * refraction * (1 - c1 * c1);
            // TODO: This may be a bug.  There was originally blank space after the
            // "if (c2 < 0)" line which was probably correcting before doing the Sqrt().
            //if (c2 < 0)

            c2 = Math.Sqrt(c2);
            var T = (n * (refraction * c1 - c2) - v * refraction) * -1;
            T.Normalize();

            return new Ray(p, T); // no refraction
        }
    }
}
