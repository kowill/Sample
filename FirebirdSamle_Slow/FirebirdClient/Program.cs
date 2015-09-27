using FirebirdSql.Data.FirebirdClient;
using System;

namespace FirebirdClient
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
                connectionBuilder.Database = @"D:\db\Hoge.FDB";
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
            return DateTime.Now.ToIntTime();
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
