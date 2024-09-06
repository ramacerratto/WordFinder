using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinderApp;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Configuration;

namespace WordFinderApp
{
    [MemoryDiagnoser(false)]
    public class BenchMarkApp
    {

        [Benchmark]
        public void BenchWithContain() 
        {
            var matrix = File.ReadLines("TestFiles\\matrix64x64.txt");
            var wordStream = File.ReadLines("TestFiles\\bigRepeatedWordStream.txt");
            WordFinder wordFinder = new WordFinder(matrix);
            IEnumerable<string> result = wordFinder.Find(wordStream, "contain");
        }

        [Benchmark]
        public void BenchWithSuffix()
        {
            var matrix = File.ReadLines("TestFiles\\matrix64x64.txt");
            var wordStream = File.ReadLines("TestFiles\\bigRepeatedWordStream.txt");
            WordFinder wordFinder = new WordFinder(matrix);
            IEnumerable<string> result = wordFinder.Find(wordStream, "suffix");
        }
    }
}
