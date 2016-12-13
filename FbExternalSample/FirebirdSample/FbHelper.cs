using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FirebirdSample
{
    class FbHelper
    {
        public IEnumerable<string> GetCreateStatements(string path)
        {
            var asm = Assembly.LoadFile(path);
            var methods = asm.GetTypes().SelectMany(x => x.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static));

            var sqls = methods.Select(item =>
            {
                var inputParam = item.GetParameters().Select(x => $"in_{x.Name} {ConvertToFbType(x.ParameterType)}" + (IsNullableParameter(x) ? " = null" : ""));
                string[] outputParam;
                if (item.ReturnParameter.ParameterType.IsGenericType)
                {
                    var tmp = item.ReturnParameter.ParameterType.GenericTypeArguments.ToArray();
                    if (0 < tmp.Length && tmp[0].IsGenericType)
                    {
                        outputParam = tmp.SelectMany(y => y.GenericTypeArguments)
                           .Select((x, i) => $"out_{i} {ConvertToFbType(x)}").ToArray();
                    }
                    else
                    {
                        outputParam = tmp.Select((x,i) => $"out_{i} {ConvertToFbType(x)}").ToArray();
                    }
                }
                else
                {
                    outputParam = new[] { $"out {ConvertToFbType(item.ReturnParameter.ParameterType)}" };
                }

                var line = $"recreate procedure {item.Name} ({string.Join(",", inputParam)})" +
                           $" returns ({string.Join(",", outputParam)})" +
                           $" external name '{item.DeclaringType.Namespace}!{item.DeclaringType.FullName}.{item.Name}' engine FbNetExternalEngine; ";
                return line;
            });

            return sqls;
        }

        private bool IsNullableParameter(ParameterInfo info)
        {
            return info.ParameterType.IsGenericType && info.ParameterType == typeof(Nullable<>);
        }
        private string ConvertToFbType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }

            if (type == typeof(Int16))
            {
                return "smallint";
            }
            else if (type == typeof(Int32))
            {
                return "integer";
            }
            else if (type == typeof(Int64))
            {
                return "bigint";
            }
            else if (type == typeof(Single))
            {
                return "float";
            }
            else if (type == typeof(Double))
            {
                return "double precision";
            }
            else if (type == typeof(Decimal))
            {
                return "decimal(18,4)";
            }
            else if (type == typeof(DateTime))
            {
                return "timestamp";
            }
            else if (type == typeof(Boolean))
            {
                return "boolean";
            }
            else if (type == typeof(String))
            {
                return "varchar(300)";
            }
            else if (type == typeof(Char))
            {
                return "char(max)";
            }
            else if (type == typeof(Byte))
            {
                return "blob";
            }
            else if (type == typeof(Guid))
            {
                return "guid";
            }
            else
            {
                throw new ArgumentException("ごめんね");
            }
        }
    }
}
