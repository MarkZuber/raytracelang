// -----------------------------------------------------------------------
// <copyright file="RayTracerFactory.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using RayTraceCSharpLib.Render;

namespace RayTraceCSharpLib
{
    public static class RayTracerFactory
    {
        public static IRayTracer CreateSimpleTracer(RayTraceRenderData renderData)
        {
            return new SimpleRayTracer(renderData.TraceDepth, SceneFactory.MarbleBallsScene(renderData.InputContentRoot), new Rectangle(0, 0, renderData.Width, renderData.Height));
        }
    }
}
