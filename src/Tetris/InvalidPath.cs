using SmartAss;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class InvalidPath : InvalidOperationException
    {
        public InvalidPath(string message) : base(message) => Do.Nothing();

        public static InvalidPath NoFit() => new("Path does not fit.");

        public static InvalidPath StepsMissing() => new("Path misses steps.");
    }
}
