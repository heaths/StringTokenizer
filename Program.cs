// Copyright 2020 Heath Stewart.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Utils
{
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    public class Program
    {
        private static readonly char[] _separators = new[] { ' ', ',', '.', '\n' };

        static void Main()
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        [Params(StringSplitOptions.None, StringSplitOptions.RemoveEmptyEntries)]
        public StringSplitOptions Options { get; set; }

        [ParamsSource(nameof(GetText))]
        public string Text { get; set; }

        public IEnumerable<string> GetText => new[]
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\nCurabitur pretium tincidunt lacus. Nulla gravida orci a odio. Nullam varius, turpis et commodo pharetra, est eros bibendum elit, nec luctus magna felis sollicitudin mauris. Integer in mauris eu nibh euismod gravida. Duis ac tellus et risus vulputate vehicula. Donec lobortis risus a elit. Etiam tempor. Ut ullamcorper, ligula eu tempor congue, eros est euismod turpis, id tincidunt sapien risus a quam. Maecenas fermentum consequat mi. Donec fermentum. Pellentesque malesuada nulla a mi. Duis sapien sem, aliquet nec, commodo eget, consequat quis, neque. Aliquam faucibus, elit ut dictum aliquet, felis nisl adipiscing sapien, sed malesuada diam lacus eget erat. Cras mollis scelerisque nunc. Nullam arcu. Aliquam consequat. Curabitur augue lorem, dapibus quis, laoreet et, pretium ac, nisi. Aenean magna nisl, mollis quis, molestie eu, feugiat in, orci. In hac habitasse platea dictumst.",
        };

        [Benchmark]
        public int UsingString()
        {
            string[] tokens = Text.Split(_separators, Options);

            int count = 0;
            foreach (string token in tokens)
            {
                string value = token.Trim();
                count++;
            }

            return count;
        }

        [Benchmark]
        public int UsingStringTokenizer()
        {
            StringTokenizer tokenizer = new StringTokenizer(Text, _separators, Options);

            int count = 0;
            foreach (StringSegment segment in tokenizer)
            {
                StringSegment value = segment.Trim();
                count++;
            }

            return count;
        }

        [Benchmark]
        public int UsingUnsafeStringTokenizer()
        {
            StringTokenizer tokenizer = new StringTokenizer(Text, _separators, Options);

            int count = 0;
            StringTokenizer.Enumerator e = tokenizer.GetEnumerator();
            while (e.MoveNext())
            {
                StringSegment segment = e.Current;
                StringSegment value = segment.UnsafeTrim();
                count++;
            }

            return count;
        }

        [Benchmark]
        public int UsingStringTokenizerSpans()
        {
            StringTokenizer tokenizer = new StringTokenizer(Text, _separators, Options);

            int count = 0;
            foreach (StringSegment segment in tokenizer)
            {
                ReadOnlySpan<char> value = segment.AsSpan().Trim();
                count++;
            }

            return count;
        }
    }
}
