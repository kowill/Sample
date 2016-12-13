using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirebirdSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new FbConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.Database = @"D:\DB\FB3_EXTERNAL_TEST.FDB";
            builder.Charset = FbCharset.Utf8.ToString();
            builder.UserID = "SYSDBA";
            builder.ServerType = FbServerType.Embedded;
            builder.ClientLibrary = @"fb\fbclient.dll";

            //DB作成
            if (!File.Exists(builder.Database))
            {
                FbConnection.CreateDatabase(builder.ConnectionString);
            }

            //create procesure
            var dllPath = new Uri(new Uri(Assembly.GetEntryAssembly().Location), @"../../Fb/plugins/FbExternalSample.dll").LocalPath;
            var createSqls = new FbHelper().GetCreateStatements(dllPath);
            using (var con = new FbConnection(builder.ConnectionString))
            using (var command = con.CreateCommand())
            {
                con.Open();
                foreach (var sql in createSqls)
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }

            //実行
            var sqls = new[] { "SELECT * FROM HellowWorld('Taro')", "SELECT * FROM GetNumbers(5)", "SELECT * FROM GetDemo('やったぜ！')" };

            using (var con = new FbConnection(builder.ConnectionString))
            using (var command = con.CreateCommand())
            {
                con.Open();

                foreach (var sql in sqls)
                {
                    Console.WriteLine("- SQL -");
                    Console.WriteLine(sql);
                    Console.WriteLine("- 実行結果 -");
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var result = "";
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            result += $" {reader[i]}"; 
                        }
                        Console.WriteLine(result);
                    }
                    Console.WriteLine("------------------------------------");
                }
            }

            Console.Read();
        }
    }
}
