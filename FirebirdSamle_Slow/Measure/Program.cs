using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Measure
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleExePaths = new[] {
                @"D:\tmp\FirebirdClient\FirebirdClient.exe",
                @"D:\tmp\FirebirdClient2\FirebirdClient2.exe"
            };
            Parallel.ForEach(sampleExePaths, path =>
            {
                var process = Process.Start(path, "1");
                process.WaitForExit();
                var elapsed = Utility.Subtract60(DateTime.Now.ToIntTime(), process.ExitCode);
                Console.WriteLine($"Path: {path} / Seconds: {elapsed}");
            });
            Console.ReadLine();
        }
    }
    public static class Utility
    {
        public static int Subtract60(int a, int b)
        {
            var a60 = ((a / 10000) * 3600) + (((a % 10000) / 100) * 60) + (a % 100);
            var b60 = ((b / 10000) * 3600) + (((b % 10000) / 100) * 60) + (b % 100);
            return a60 - b60;
        }
    }
    public static class Extensions
    {
        public static int ToIntTime(this DateTime time)
        {
            return time.Hour * 10000 + time.Minute * 100 + time.Second;
        }
    }
}
