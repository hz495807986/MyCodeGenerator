using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyCodeGenerator.WinForm
{
    public class BuilderModel
    {
        public static void WriteModel(string path,string table, List<ProcedureInfo> list,string namespaceModel )
        {
            StreamWriter write = new StreamWriter(path + @"\Models\" + table + ".cs", false, Encoding.Default);
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Collections.Generic;");
            sb.Append(Environment.NewLine);
            sb.Append("using System.Text;");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("namespace ");
            sb.Append(namespaceModel);
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    [Serializable]");
            sb.Append(Environment.NewLine);
            sb.Append("    public class ");
            sb.Append(table);
            sb.Append(Environment.NewLine);
            sb.Append(MyHelper.createplace(4));
            sb.Append("{");
            sb.Append(Environment.NewLine);

            foreach (ProcedureInfo pro in list)
            {
                string name = pro.ColumnName;
                string type = pro.DataType;
                string privateColName = "_" + name.Substring(0, 1).ToLower() + name.Substring(1);
                sb.Append(MyHelper.createplace(8));
                sb.Append("//");
                sb.Append(pro.Description);
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(8));
                sb.Append("private");
                sb.Append(" ");
                sb.Append(MyHelper.findModelsType(type));
                sb.Append(MyHelper.createplace(1));
                sb.Append(privateColName);
                sb.Append(";");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(8));
                sb.Append("public");
                sb.Append(" ");
                sb.Append(MyHelper.findModelsType(type));
                sb.Append(" ");
                sb.Append(name.Substring(0, 1).ToUpper() + name.Substring(1));
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(8));
                sb.Append("{");
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(10));
                sb.Append("get { return ");
                sb.Append(privateColName);
                sb.Append(";}");
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(10));
                sb.Append("set { ");
                sb.Append(privateColName);
                sb.Append("=value");
                sb.Append(";}");
                sb.Append(Environment.NewLine);
                sb.Append(MyHelper.createplace(8));
                sb.Append("}");
                sb.Append(Environment.NewLine);


            }
            sb.Append(MyHelper.createplace(4));
            sb.Append("}");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            write.WriteLine(sb.ToString());
            write.Flush();
            write.Close();
        }
    }
}
