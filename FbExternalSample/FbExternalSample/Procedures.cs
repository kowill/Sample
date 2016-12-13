using System;
using System.Collections.Generic;
using System.Linq;

namespace FbExternalSample
{
    public static class Procedures
    {
        public static IEnumerator<Tuple<string>> HellowWorld(string name)
        {
            yield return new Tuple<string>($"Hello World!!, Hello {name}!!");
        }

        public static IEnumerator<Tuple<int?>> GetNumbers(int? count)
        {
            return Enumerable.Range(0, count.Value).Select(x => Tuple.Create((int?)x)).GetEnumerator();
        }

        public static IEnumerator<Tuple<int?, string>> GetDemo(string txt)
        {
            yield return new Tuple<int?, string>(1, "sample1");
            yield return new Tuple<int?, string>(2, "sample2");
            yield return new Tuple<int?, string>(3, $"{txt}");
            yield return new Tuple<int?, string>(4, $"Done!!");
        }
    }
}
