using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyCodeGenerator.WinForm
{
    public class BuilderBLL
    {
        public static void WriteBLL(string path, string table, List<ProcedureInfo> list, string namespaceBLL,string namespaceDAL,string namespaceModel)
        {
            string bllString = "";
            bllString += "using System;\r\n";
            bllString += "using System.Collections.Generic;\r\n";
            bllString += "using " + namespaceDAL + ";\r\n";
            bllString += "using " + namespaceModel + ";\r\n";
            bllString += "\r\n";
            bllString += "namespace " + namespaceBLL + "\r\n";
            bllString += "{\r\n   public class " + table + "Manager\r\n";
            bllString += "   {\r\n";
            bllString += "        \r\n       ";
            bllString += MyHelper.FirstToUpper(table) + "Service dal = new " + MyHelper.FirstToUpper(table) + "Service();\r\n\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 增加\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool add(" + table + " model)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.add(model);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 删除\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool delete(int Id)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.delete(Id);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 修改\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"" + table + "\">" + table + "实体对象</param>\r\n";
            bllString += "        /// <returns>bool值,判断是否操作成功</returns>\r\n";
            bllString += "        public bool change(" + table + " model)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.change(model);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 查询全部\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <returns>" + table + "实体类对象集合</returns>\r\n";
            bllString += "        public List<" + table + "> selectAll()\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectAll();\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 通过Id查询\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            bllString += "        /// <returns>" + table + "实体类对象</returns>\r\n";
            bllString += "        public " + table + " selectById(int Id)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectById(Id);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";

            bllString += "        /// <summary>\r\n";
            bllString += "        /// 通过条件查询\r\n";
            bllString += "        /// </summary>\r\n";
            bllString += "        /// <param name=\"WhereString\">主键Id</param>\r\n";
            bllString += "        /// <returns>" + table + "实体类对象集合</returns>\r\n";
            bllString += "        public List<" + table + "> selectByWhere(string WhereString)\r\n";
            bllString += "        {\r\n";
            bllString += "            return dal.selectByWhere(WhereString);\r\n";
            bllString += "        }\r\n";
            bllString += "\r\n";
            bllString += "    }\r\n";
            bllString += "}\r\n";

            FileInfo file = new FileInfo(path + @"\BLL" + "\\" + table + "BLL.cs");
            StreamWriter sw = file.CreateText();
            sw.WriteLine(bllString);
            sw.Close();
        }
    }
}
