using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCodeGenerator.WinForm
{
    class MyHelper
    {
        #region 返回数据库类型
        public static bool isString(string type)
        {
            bool b = false;
            if (type == "varchar" || type == "char" || type == "nvarchar" || type == "nchar")
            {
                b = true;
            }
            return b;
        }
        #endregion

        #region 返回C#数据类型
        public static string findType(string dateType)
        {
            string reval = string.Empty;
            switch (dateType.ToLower())
            {
                case "int":
                    reval = "Int32";
                    break;
                case "text":
                    reval = "String";
                    break;
                case "bigint":
                    reval = "Int64";
                    break;
                case "binary":
                    reval = "Byte[]";
                    break;
                case "bit":
                    reval = "Boolean";
                    break;
                case "char":
                    reval = "String";
                    break;
                case "datetime":
                    reval = "DateTime";
                    break;
                case "decimal":
                    reval = "Decimal";
                    break;
                case "float":
                    reval = "Double";
                    break;
                case "image":
                    reval = "Byte[]";
                    break;
                case "money":
                    reval = "Decimal";
                    break;
                case "nchar":
                    reval = "String";
                    break;
                case "ntext":
                    reval = "String";
                    break;
                case "numeric":
                    reval = "Decimal";
                    break;
                case "nvarchar":
                    reval = "String";
                    break;
                case "real":
                    reval = "Single";
                    break;
                case "smalldatetime":
                    reval = "DateTime";
                    break;
                case "smallint":
                    reval = "Int16";
                    break;
                case "smallmoney":
                    reval = "Decimal";
                    break;
                case "timestamp":
                    reval = "DateTime";
                    break;
                case "tinyint":
                    reval = "Byte";
                    break;
                case "uniqueidentifier":
                    reval = "Guid";
                    break;
                case "varbinary":
                    reval = "Byte[]";
                    break;
                case "varchar":
                    reval = "String";
                    break;
                case "Variant":
                    reval = "Object";
                    break;
                default:
                    reval = "String";
                    break;
            }
            return reval;
        }
        #endregion

        #region 首字母大写
        public static string FirstToUpper(string name)
        {
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            return name;
        }
        #endregion

        #region 首字母小写
        public static string fristToLower(string name)
        {
            name = name.Substring(0, 1).ToLower() + name.Substring(1);
            return name;
        }
        #endregion

        #region 转换空格
        public static string createplace(int a)
        {
            return new string(' ', a);
        }
        #endregion

        #region 返回C#实体数据类型
        public static string findModelsType(string name)
        {
            if (name == "int" || name == "smallint")
            {
                return "int";
            }
            else if (name == "tinyint")
            {
                return "byte";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single";
            }
            else if (name == "float")
            {
                return "float";
            }
            else if (name == "decimal")
            {
                return "decimal";
            }
            else if (name == "char" || name == "varchar" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool";
            }
            else if (name == "datetime" || name == "smalldatetime")
            {
                return "DateTime";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double";
            }
            else
            {
                return "string";
            }
        }
        #endregion
    }
}
