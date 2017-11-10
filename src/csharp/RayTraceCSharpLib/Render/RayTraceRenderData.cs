// -----------------------------------------------------------------------
// <copyright file="RayTraceRenderData.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

namespace RayTraceCSharpLib.Render
{
    public class RayTraceRenderData
    {
        public RayTraceRenderData(int width, int height, int traceDepth, int maxParallelism, string inputContentRoot)
        {
            Width = width;
            Height = height;
            TraceDepth = traceDepth;
            MaxParallelism = maxParallelism;
            InputContentRoot = inputContentRoot;
        }

        public int Width { get; }
        public int Height { get; }
        public int TraceDepth { get; }
        public int MaxParallelism { get; }
        public string InputContentRoot { get; }

        public override string ToString()
        {
            return $"(Width: {Width}, Height: {Height}, TraceDepth: {TraceDepth}, MaxParallelism: {MaxParallelism})";
        }
    }
}
