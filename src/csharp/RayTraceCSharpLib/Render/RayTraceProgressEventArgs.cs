// -----------------------------------------------------------------------
// <copyright file="RayTraceProgressEventArgs.cs" company="ZubeNET">
//   Copyright...
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RayTraceCSharpLib.Render
{
    public class RayTraceProgressEventArgs : EventArgs
    {
        public RayTraceProgressEventArgs(int percentComplete)
        {
            PercentComplete = percentComplete;
        }

        public int PercentComplete { get; }
    }
}
