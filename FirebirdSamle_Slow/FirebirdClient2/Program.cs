using FirebirdSql.Data.FirebirdClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FirebirdClient2
{
    class Program
    {
        static int Main(string[] args)
        {
            int i = 0;
            int max = args.Length < 1 ? 1 : int.Parse(args[0]);
            while (i < max)
            {
                var connectionBuilder = new FbConnectionStringBuilder();
                connectionBuilder.DataSource = "localhost";
                connectionBuilder.Database = @"D:\db\Hoge2.FDB";
                connectionBuilder.Charset = FbCharset.Utf8.ToString();
                connectionBuilder.UserID = "SYSDBA";
                connectionBuilder.Password = "masterkey";
                connectionBuilder.ServerType = FbServerType.Embedded;
                connectionBuilder.Pooling = false;

                using (var connection = new FbConnection(connectionBuilder.ConnectionString))
                {
                    connection.Open();
                    connection.Close();
                }
                i++;
            }
            UnloadDll(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"fbembed.dll"));
            return DateTime.Now.ToIntTime();
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        public static void UnloadDll(string path)
        {
            foreach (ProcessModule dll in Process.GetCurrentProcess().Modules)
            {
                if (dll.FileName.Equals(path, StringComparison.OrdinalIgnoreCase))
                {
                    FreeLibrary(dll.BaseAddress);
                }
            }
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
