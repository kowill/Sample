using FirebirdSql.Data.FirebirdClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Fb3Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new FbConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.Database = @"D:\DB\TESTFB3.FDB";
            builder.Charset = FbCharset.Utf8.ToString();
            builder.UserID = "SYSDBA";
            builder.Password = "masterkey";
            builder.ServerType = FbServerType.Embedded;
            builder.ClientLibrary = @"dll\fb3\fbclient";
            builder.Pooling = false;

            Console.WriteLine("--fb3--------------------");
            if (!File.Exists(builder.Database))
            {
                FbConnection.CreateDatabase(builder.ConnectionString);
            }
            DispObsVersinoFromBinary(builder.Database);
            DispObsVersinoFromBinary2(builder.Database);
            DispObsVersion(builder);
            CreateAndExecuteFb3(builder);

            builder.Database = @"D:\DB\TESTFB2.FDB";
            builder.ClientLibrary = @"dll\fb25\fbembed";

            Console.WriteLine("--fb25-------------------");
            if (!File.Exists(builder.Database))
            {
                FbConnection.CreateDatabase(builder.ConnectionString);
            }
            DispObsVersinoFromBinary(builder.Database);
            DispObsVersinoFromBinary2(builder.Database);
            DispObsVersion(builder);
            CreateAndExecuteFb2(builder);

            Console.WriteLine("--dll--------------------");

            foreach (ProcessModule dll in Process.GetCurrentProcess().Modules)
            {
                if (dll.FileName.Contains("\\fb")) Console.WriteLine(dll.FileName);
            }
            Console.Read();
        }

        public static T RawDataToObject<T>(byte[] rawData) where T : struct
        {
            var pinnedData = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(pinnedData.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                pinnedData.Free();
            }
        }

        private const ushort FirebirdFlag = 0x8000;

        static void DispObsVersinoFromBinary2(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                var buf = new byte[1024];
                stream.Read(buf, 0, buf.Length);
                var header = RawDataToObject<HeaderPage>(buf);
                var odsVersion = header.hdr_ods_version & ~FirebirdFlag;
                Console.WriteLine($"ODSVer(FromBinary_struct):{odsVersion}");
                Console.WriteLine($"ODSMinorVer(FromBinary_struct):{header.hdr_ods_minor}");
            }
        }

        static void DispObsVersinoFromBinary(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int fileSize = (int)fs.Length;
                byte[] buf = new byte[1024];
                fs.Read(buf, 0, 1024);
                var obsHex = buf.Skip(0x12).Take(2).Select(x => x.ToString("X2")).Reverse().Aggregate("", (x, y) => x + y);
                var minor = buf.Skip(0x40).Take(2).Select(x => x.ToString("X2")).Reverse().Aggregate("", (x, y) => x + y);
                Console.WriteLine($"ODSVer(FromBinary):{Convert.ToInt32(obsHex, 16) & ~FirebirdFlag}");
                Console.WriteLine($"ODSMinorVer(FromBinary):{Convert.ToInt32(minor, 16)}");
            }
        }

        static void DispObsVersion(FbConnectionStringBuilder builder)
        {
            using (var con = new FbConnection(builder.ConnectionString))
            {
                con.Open();
                Console.WriteLine($"ODSVer(FromDB):{new FbDatabaseInfo(con).OdsVersion}");
                Console.WriteLine($"ODSMinorVer(FromDB):{new FbDatabaseInfo(con).OdsMinorVersion}");
            }
        }

        static void CreateAndExecuteFb2(FbConnectionStringBuilder builder)
        {
            using (var con = new FbConnection(builder.ConnectionString))
            using (var command = con.CreateCommand())
            {
                con.Open();

                command.CommandText = @"recreate table Test (id int, price int, name varchar(50), primary key (id))";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (1,500, 'Hoge')";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (2,200, 'HogeHoge')";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (3,12000, 'あいうえお')";
                command.ExecuteNonQuery();

                command.CommandText = @"select * from test";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}  {reader[1]}  {reader[2]} ");
                }
            }
        }

        static void CreateAndExecuteFb3(FbConnectionStringBuilder builder)
        {
            using (var con = new FbConnection(builder.ConnectionString))
            using (var command = con.CreateCommand())
            {
                con.Open();

                command.CommandText = @"recreate table Test (id int, price int, name varchar(50), Hoge boolean, primary key (id))";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (1,500, 'Hoge',true)";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (2,200, 'HogeHoge',false)";
                command.ExecuteNonQuery();

                command.CommandText = @"insert into Test values (3,12000, 'あいうえお',true)";
                command.ExecuteNonQuery();

                command.CommandText = @"select * from test";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]}  {reader[1]}  {reader[2]} {reader[3]}");
                }
            }
        }
    }
}
