using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var benchMark = BenchmarkRunner.Run<BenchMarks>();

        }
    }
}
