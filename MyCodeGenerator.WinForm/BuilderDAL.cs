using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyCodeGenerator.WinForm
{
    public class BuilderDAL
    {
        /// <summary>
        /// 生成数据访问层
        /// </summary>
        /// <param name="path">生成路径</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="table">当前表名称</param>
        /// <param name="list"></param>
        public static void WriteDAL(string path, string projectName, string table, List<ProcedureInfo> list)
        {
            string columns = "";//拼接username,age...
            string parmColumns = "";//拼接 @username,@age...
            string updateParm = "";//拼接 username=@username,age=@age...
            string sqlParm = "";//拼接new SqlParameter ("@UserName",model.UserName),new SqlParameter ("@Account",model.Account)
            int k = 0;
            foreach (ProcedureInfo pro in list)
            {
                k++;
                columns += pro.ColumnName;
                parmColumns += "@" + pro.ColumnName;
                if (k != 1)//把第一个除外
                {
                    updateParm += pro.ColumnName + "=@" + pro.ColumnName + ",";//第一个参数Id=@Id除外
                }
                sqlParm += "                    new SqlParameter (\"@" + pro.ColumnName + "\",model." + MyHelper.FirstToUpper(pro.ColumnName) + ")";
                if (k < list.Count)//把最后一个除外
                {
                    sqlParm += ",";
                    columns += ",";
                    parmColumns += ",";
                }
                sqlParm += "\r\n";
            }
            updateParm = updateParm.TrimEnd(',');
            string dalString = "";
            #region 拼接开始
            dalString += "using System;\r\n";
            dalString += "using System.Collections.Generic;\r\n";
            dalString += "using System.Data;\r\n";
            dalString += "using System.Data.SqlClient;\r\n";
            dalString += "using " + projectName + ".Entity;\r\n";
            dalString += "using " + projectName + ".IDAL;\r\n";
            dalString += "using " + projectName + ".DBUtility;\r\n";
            dalString += "\r\n";
            dalString += "namespace " + projectName + ".SQLServerDAL\r\n";
            dalString += "{\r\n";
            dalString += "    public partial class " + table + "DAL\r\n";
            dalString += "    {\r\n";
            #endregion

            #region 内部方法---获取所有的列：,号分隔
            dalString += "        private string GetColumns()\r\n";
            dalString += "        {\r\n";
            dalString += "           return \"" + columns + "\";\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 内部方法---读取IDataReader对象
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 内部方法:读取Reader对象\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"reader\">要读取的对象</param>\r\n";
            dalString += "        /// <returns>返回" + table + "实体</returns>\r\n";
            dalString += "        private " + table + "Dto ReaderRecord(IDataReader reader)\r\n";
            dalString += "        {\r\n";
            dalString += "            " + table + "Dto model = new " + table + "Dto();\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            foreach (ProcedureInfo pro in list)
            {
                if (MyHelper.findType(pro.DataType) == "DateTime")
                {
                    dalString += "              model." + pro.ColumnName + " = ((reader[\"" + pro.ColumnName + "\"]) == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(reader[\"" + pro.ColumnName + "\"]);\r\n";
                }
                else if (MyHelper.findType(pro.DataType) == "Int32")
                {
                    dalString += "              model." + pro.ColumnName + " = ((reader[\"" + pro.ColumnName + "\"]) == DBNull.Value) ? 0 : Convert.Int32(reader[\"" + pro.ColumnName + "\"]);\r\n";
                }
                else
                {
                    dalString += "              model." + pro.ColumnName + " = Convert.ToString(reader[\"" + pro.ColumnName + "\"]);\r\n";
                }
            }
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "            }\r\n";
            dalString += "            return model;\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 保存记录
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 保存记录:若主键为null为插入,否则为修改\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"model\">" + table + "实体对象</param>\r\n";
            dalString += "        /// <returns>int值,返回影响的行数</returns>\r\n";
            dalString += "        public int SaveRecord(" + table + "Dto model)\r\n";
            dalString += "        {\r\n";
            dalString += "             int res;\r\n";
            dalString += "             try\r\n";
            dalString += "             {\r\n";
            dalString += "                 string strSql=\"\";\r\n";
            dalString += "                 if(string.IsNullOrEmpty(model." + list[0].ColumnName + "))\r\n";
            dalString += "                 {\r\n";
            dalString += "                     model." + list[0].ColumnName + " =Guid.NewGuid().ToString();\r\n";
            dalString += "                     strSql=@\"insert into " + table + "(" + columns + ") \r\n values \r\n(" + parmColumns + ")\";\r\n";
            dalString += "                 }\r\n";
            dalString += "                 else\r\n";
            dalString += "                 {\r\n";
            dalString += "                     strSql=@\"update " + table + " set " + updateParm + " \r\n where " + list[0].ColumnName + "=@" + list[0].ColumnName + "\";\r\n";
            dalString += "                 }\r\n";
            dalString += "                SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "                {\r\n";
            dalString += sqlParm;
            dalString += "                };\r\n";
            dalString += "                res= SQLServerHelper.ExecuteNonQuery(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, param);\r\n";
            dalString += "                base.WriteOperateLog(strSql,\"保存" + table + "表记录\");//记录操作日志\r\n";
            dalString += "                return res;\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "                return 0;\r\n";
            dalString += "            }\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 删除记录
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 删除\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            dalString += "        /// <returns>int 类型，返回影响的行数</returns>\r\n";
            dalString += "        public int DeleteById(string Id)\r\n";
            dalString += "        {\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            dalString += "                string strSql = \"delete from " + table + " where  " + list[0].ColumnName + "=@" +
                         list[0].ColumnName + "\";\r\n";
            dalString += "                SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "                {\r\n";
            dalString += "                    new SqlParameter (\"@" + list[0].ColumnName + "\",Id)\r\n";
            dalString += "                };\r\n";
            dalString += "               return SQLServerHelper.ExecuteNonQuery(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, param);\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "                return 0;\r\n";
            dalString += "            }\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 根据主键获取一条记录
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 根据主键获取一条记录\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"Id\">主键Id</param>\r\n";
            dalString += "        /// <returns>返回" + table + "实体</returns>\r\n";
            dalString += "        public " + table + "Dto GetOneById(string Id)\r\n";
            dalString += "        {\r\n";
            dalString += "            " + table + "Dto model = new " + table + "Dto();\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            dalString += "                string strSql = \"select " + columns + " from " + table + " where  " + list[0].ColumnName + "=@" +
                         list[0].ColumnName + "\";\r\n";
            dalString += "                SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "                {\r\n";
            dalString += "                    new SqlParameter (\"@" + list[0].ColumnName + "\",Id)\r\n";
            dalString += "                };\r\n";
            dalString += "               using (IDataReader reader = SQLServerHelper.ExecuteReader(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, param))\r\n";
            dalString += "                {\r\n";
            dalString += "                    if(reader.Read())\r\n";
            dalString += "                    {\r\n";
            dalString += "                        model = this.ReaderRecord(reader);\r\n";
            dalString += "                    }\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "            }\r\n";
            dalString += "            return model;\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 根据某字段获取一条记录
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 根据某字段获取一条记录\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"columnName\">字段名称</param>\r\n";
            dalString += "        /// <param name=\"columnValue\">字段值</param>\r\n";
            dalString += "        /// <returns>返回" + table + "实体</returns>\r\n";
            dalString += "        public " + table + "Dto GetOneByOneColumn(string columnName,string columnValue)\r\n";
            dalString += "        {\r\n";
            dalString += "            " + table + "Dto model = new " + table + "Dto();\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            dalString += "                string strSql = \"select " + columns + " from " + table + " where  \" + columnName + \"=@columnName\";\r\n";
            dalString += "                SqlParameter[] param = new SqlParameter[]\r\n";
            dalString += "                {\r\n";
            dalString += "                    new SqlParameter (\"@columnName\",columnValue)\r\n";
            dalString += "                };\r\n";
            dalString += "               using (IDataReader reader = SQLServerHelper.ExecuteReader(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, param))\r\n";
            dalString += "                {\r\n";
            dalString += "                    if(reader.Read())\r\n";
            dalString += "                    {\r\n";
            dalString += "                        model = this.ReaderRecord(reader);\r\n";
            dalString += "                    }\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "            }\r\n";
            dalString += "            return model;\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 查询-不分页
            //没有传参数
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 获取记录（没有参数）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetList()\r\n";
            dalString += "        {\r\n";
            dalString += "            return this.GetList(null);\r\n";
            dalString += "        }\r\n";
            //查询条件
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 获取记录（查询条件）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"listQuery\">查询条件</param>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetList(List<Querying> listQuery)\r\n";
            dalString += "        {\r\n";
            dalString += "            return this.GetList(listQuery,\"[" + list[0].ColumnName + "] desc\");\r\n";
            dalString += "        }\r\n";
            //查询条件+排序
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 获取记录（查询条件+排序）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"listQuery\">查询条件</param>\r\n";
            dalString += "        /// <param name=\"sortField\">排序</param>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetList(List<Querying> listQuery,string sortField)\r\n";
            dalString += "        {\r\n";
            dalString += "            List<" + table + "Dto> list =new  List<" + table + "Dto>();\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            dalString += "                if (string.IsNullOrEmpty(sortField)) sortField =\"[" + list[0].ColumnName + "] desc\";\r\n";
            dalString += "                ResultBuilder resultB = BuildWhereSQLServer.CreateWhereAndParams(listQuery);\r\n";
            dalString += "                string strSql = string.Format(\"select {0} from " + table +
                         " {1} order by {2}\",this.GetColumns(),resultB.strWhere,sortField);\r\n";
            dalString += "                using (IDataReader reader = SQLServerHelper.ExecuteReader(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, resultB.listParam.ToArray()))\r\n";
            dalString += "                {\r\n";
            dalString += "                    while (reader.Read())\r\n";
            dalString += "                    {\r\n";
            dalString += "                        list.Add(this.ReaderRecord(reader));\r\n";
            dalString += "                    }\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志\r\n";
            dalString += "            }\r\n";
            dalString += "            return list;\r\n";
            dalString += "        }\r\n";
            #endregion
            #region 查询-分页
            //分页参数
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 分页获取记录（分页参数，总记录条数）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"page\">分页参数</param>\r\n";
            dalString += "        /// <param name=\"recordCount\">传出总记录数</param>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetPagingList(Paging page, out int recordCount)\r\n";
            dalString += "        {\r\n";
            dalString += "            return this.GetPagingList(null, page, out recordCount);\r\n";
            dalString += "        }\r\n";
            //分页参数+查询条件
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 获取记录（查询条件）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"listQuery\">查询条件</param>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetPagingList(List<Querying> listQuery,Paging page, out int recordCount)\r\n";
            dalString += "        {\r\n";
            dalString += "            return this.GetPagingList(listQuery,\"[" + list[0].ColumnName + "] desc\",page, out recordCount);\r\n";
            dalString += "        }\r\n";
            //分页参数+查询条件+排序
            dalString += "        /// <summary>\r\n";
            dalString += "        /// 获取记录（查询条件+排序）\r\n";
            dalString += "        /// </summary>\r\n";
            dalString += "        /// <param name=\"listQuery\">查询条件</param>\r\n";
            dalString += "        /// <param name=\"sortField\">排序</param>\r\n";
            dalString += "        /// <returns>返回" + table + "集合</returns>\r\n";
            dalString += "        public List<" + table + "Dto> GetPagingList(List<Querying> listQuery,string sortField, Paging page, out int recordCount)\r\n";
            dalString += "        {\r\n";
            dalString += "            List<" + table + "Dto> list =new  List<" + table + "Dto>();\r\n";
            dalString += "            try\r\n";
            dalString += "            {\r\n";
            dalString += "                if (string.IsNullOrEmpty(sortField)) sortField =\"[" + list[0].ColumnName + "] desc\";\r\n";
            dalString += "                string ids = base.GetIdsOutTotalRecords(listQuery, page,\"" + table + "\", out recordCount);\r\n";
            dalString += "                if (ids.Trim() == \"\")\r\n";
            dalString += "                {\r\n";
            dalString += "                    ids = \"''\";//避免出现  select * from " + table + " where id in()的情况\r\n";
            dalString += "                }\r\n";
            dalString += "                string strSql = \"select {0} from " + table + " where " + list[0].ColumnName + " in({1}) order by {2}\";\r\n";
            dalString += "                strSql = string.Format(strSql, this.GetColumns(), ids, sortField);\r\n";
            dalString += "                using (IDataReader reader = SQLServerHelper.ExecuteReader(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSql, null))\r\n";
            dalString += "                {\r\n";
            dalString += "                    while (reader.Read())\r\n";
            dalString += "                    {\r\n";
            dalString += "                        list.Add(this.ReaderRecord(reader));\r\n";
            dalString += "                    }\r\n";
            dalString += "                }\r\n";
            dalString += "            }\r\n";
            dalString += "            catch (Exception ex)\r\n";
            dalString += "            {\r\n";
            dalString += "                recordCount = 0;\r\n";
            dalString += "                base.WriteErrorLog(ex.Message);//记录错误日志  不throw就要为recordCount赋值 丫的为啥？\r\n";
            dalString += "            }\r\n";
            dalString += "            return list;\r\n";
            dalString += "        }\r\n";
            #endregion

            #region 拼接结束
            dalString += "    }\r\n";
            dalString += "}\r\n";
            FileInfo file = new FileInfo(path + @"\DAL" + "\\" + table + "DAL.cs");
            StreamWriter sw = file.CreateText();
            sw.WriteLine(dalString);
            sw.Close();
            #endregion
        }

        /// <summary>
        /// 生成数据访问层基类
        /// </summary>
        /// <param name="path"></param>
        /// <param name="projectName"></param>
        /// <param name="table"></param>
        public static void WriteBaseDAL(string path, string projectName, List<ProcedureInfo> list)
        {
            //含有一个或多个抽象方法的类称为抽象类,抽象类不能够被实例化
            //子类继承抽象父类后，可以使用override关键字覆盖父类中的abstract方法或virtual虚方法，并做具体的实现。也可以不实现抽象方法，留给后代实现，这时子类仍旧是一个抽象类，必须声明为abstract
            string baseDALString = "";
            #region 拼接开始
            baseDALString += "using System;\r\n";
            baseDALString += "using System.Collections.Generic;\r\n";
            baseDALString += "using System.Data;\r\n";
            baseDALString += "using System.Text;\r\n";
            baseDALString += "using " + projectName + ".Entity;\r\n";
            baseDALString += "using " + projectName + ".Common.Query;\r\n";
            baseDALString += "using " + projectName + ".DBUtility;\r\n";
            baseDALString += "\r\n";
            baseDALString += "namespace " + projectName + ".SQLServerDAL\r\n";
            baseDALString += "{\r\n";
            baseDALString += "    public abstract class BaseDAL\r\n";
            baseDALString += "    {\r\n";
            #endregion
            //记录操作日志
            baseDALString += "        /// <summary>\r\n";
            baseDALString += "        /// 记录操作日志\r\n";
            baseDALString += "        /// </summary>\r\n";
            baseDALString += "        /// <param name=\"_ExcuteSql\">执行语句</param>\r\n";
            baseDALString += "        /// <param name=\"_Remark\">说明</param>\r\n";
            baseDALString += "        /// <returns>void</returns>\r\n";
            baseDALString += "        protected void WriteOperateLog(string _ExcuteSql, string _Remark)\r\n";
            baseDALString += "        {\r\n";
            baseDALString += "            OperateLogDto model =new OperateLogDto();\r\n";
            baseDALString += "            model.Id = Guid.NewGuid().ToString();\r\n";
            baseDALString += "            model.ExcuteSql = _ExcuteSql;\r\n";
            baseDALString += "            model.CreateDate = DateTime.Now;\r\n";
            baseDALString += "            model.Remark = _Remark;\r\n";
            baseDALString += "            DALFactory.GetOperateLogDALInstance().Insert(model);//记录操作日志;\r\n";
            baseDALString += "        }\r\n";
            //记录错误日志
            baseDALString += "        /// <summary>\r\n";
            baseDALString += "        /// 记录错误日志\r\n";
            baseDALString += "        /// </summary>\r\n";
            baseDALString += "        /// <param name=\"WriteErrorLog\">错误信息</param>\r\n";
            baseDALString += "        /// <param name=\"_Remark\">说明</param>\r\n";
            baseDALString += "        /// <returns>void</returns>\r\n";
            baseDALString += "        protected void WriteOperateLog(string WriteErrorLog, string _Remark)\r\n";
            baseDALString += "        {\r\n";
            baseDALString += "            ErrorLogDto model =new ErrorLogDto();\r\n";
            baseDALString += "            model.Id = Guid.NewGuid().ToString();\r\n";
            baseDALString += "            model.ErrorMsg = ErrorMsg;\r\n";
            baseDALString += "            model.CreateDate = DateTime.Now;\r\n";
            baseDALString += "            model.Remark = _Remark;\r\n";
            baseDALString += "        }\r\n";
            //获取满足条件的主键并返回总记录数
            baseDALString += "        /// <summary>\r\n";
            baseDALString += "        /// 找到所有符合条件的ID集合并返回总数\r\n";
            baseDALString += "        /// </summary>\r\n";
            baseDALString += "        /// <param name=\"listQuery\">查询条件</param>\r\n";
            baseDALString += "        /// <param name=\"page\">分页参数</param>\r\n";
            baseDALString += "        /// <param name=\"tableName\">表名</param>\r\n";
            baseDALString += "        /// <param name=\"recordCount\">out总记录条数</param>\r\n";
            baseDALString += "        /// <returns>string 满足条件的ids</returns>\r\n";
            baseDALString += "        public string GetIdsOutTotalRecords(List<Querying> listQuery, Paging page,string tableName, out int recordCount)\r\n";
            baseDALString += "        {\r\n";
            baseDALString += "            try\r\n";
            baseDALString += "            {\r\n";
            baseDALString += "                //拼接查询条件\r\n";
            baseDALString += "                ResultBuilder resultB = BuildWhereSQLServer.CreateWhereAndParams(listQuery);\r\n";
            baseDALString += "                //获取满足条件总条数\r\n";
            baseDALString += "                string strSqlGetCount = string.Format(\"select count(-1) from {0} {1}\",tableName, resultB.strWhere);\r\n";
            baseDALString += "                string tmpVal = SQLServerHelper.ExecuteScalar(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSqlGetCount, resultB.listParam.ToArray()).ToString();\r\n";
            baseDALString += "                int.TryParse(tmpVal, out recordCount);\r\n";
            baseDALString += "                //获取满足条件Id集合--\r\n";
            baseDALString += "                string strSqlGetIds = string.Format(\"Select top({0}) " + list[0].ColumnName + " From {1} {2}\", page.PageEndRows,tableName,resultB.strWhere);\r\n";
            baseDALString += "                StringBuilder strIds = new StringBuilder();\r\n";
            baseDALString += "                using (IDataReader reader = SQLServerHelper.ExecuteReader(SQLServerHelper.ConnectionString, System.Data.CommandType.Text, strSqlGetIds, resultB.listParam.ToArray()))\r\n";
            baseDALString += "                {\r\n";
            baseDALString += "                    int i = 0;\r\n";
            baseDALString += "                    while (reader.Read())\r\n";
            baseDALString += "                    {\r\n";
            baseDALString += "                         //只读取第PageStartRows条到第PageEndRows 条记录 \r\n";
            baseDALString += "                        if (i >= page.PageStartRows && i <= page.PageEndRows)\r\n";
            baseDALString += "                        {\r\n";
            baseDALString += "                            strIds.Append(\"'\");\r\n";
            baseDALString += "                            strIds.Append(reader[\"" + list[0].ColumnName + "\"]);\r\n";
            baseDALString += "                            strIds.Append(\"'\");\r\n";
            baseDALString += "                            strIds.Append(\",\");\r\n";
            baseDALString += "                        }\r\n";
            baseDALString += "                        i++;\r\n";
            baseDALString += "                    }\r\n";
            baseDALString += "                }\r\n";
            baseDALString += "                return strIds.ToString().TrimEnd(',');\r\n";
            baseDALString += "            }\r\n";
            baseDALString += "            catch (Exception ex)\r\n";
            baseDALString += "            {\r\n";
            baseDALString += "                DALFactory.GetErrorLogDALInstance().Insert(new ErrorLogDto(ex.Message.ToString()));//记录错误日志\r\n";
            baseDALString += "                recordCount = 0;\r\n";
            baseDALString += "                return '';\r\n";
            baseDALString += "            }\r\n";
            baseDALString += "        }\r\n";
            #region 拼接结束
            baseDALString += "    }\r\n";
            baseDALString += "}\r\n";
            FileInfo file = new FileInfo(path + @"\DAL" + "\\BaseDAL.cs");
            StreamWriter sw = file.CreateText();
            sw.WriteLine(baseDALString);
            sw.Close();
            #endregion
        }

        /// <summary>
        /// 生成数据访问层-拼接查询条件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="projectName"></param>
        /// <param name="table"></param>
        public static void WriteBuildWhereSQLServer(string path, string projectName)
        {
            //含有一个或多个抽象方法的类称为抽象类,抽象类不能够被实例化
            //子类继承抽象父类后，可以使用override关键字覆盖父类中的abstract方法或virtual虚方法，并做具体的实现。也可以不实现抽象方法，留给后代实现，这时子类仍旧是一个抽象类，必须声明为abstract
            string strBuildWhereSQLServer = "";
            #region 拼接开始
            strBuildWhereSQLServer += "using System;\r\n";
            strBuildWhereSQLServer += "using System.Collections.Generic;\r\n";
            strBuildWhereSQLServer += "using System.Data;\r\n";
            strBuildWhereSQLServer += "using System.Text;\r\n";
            strBuildWhereSQLServer += "using " + projectName + ".Entity;\r\n";
            strBuildWhereSQLServer += "using " + projectName + ".Common.Query;\r\n";
            strBuildWhereSQLServer += "using " + projectName + ".DBUtility;\r\n";
            strBuildWhereSQLServer += "\r\n";
            strBuildWhereSQLServer += "namespace " + projectName + ".SQLServerDAL\r\n";
            strBuildWhereSQLServer += "{\r\n";
            strBuildWhereSQLServer += "    //返回拼接查询条件结果的Model\r\n";
            strBuildWhereSQLServer += "    public  class ResultBuilder\r\n";
            strBuildWhereSQLServer += "    {\r\n";
            strBuildWhereSQLServer += "        public string strWhere { get; set; }\r\n";
            strBuildWhereSQLServer += "        public List<SqlParameter> listParam { get; set; }\r\n";
            strBuildWhereSQLServer += "    }\r\n";
            strBuildWhereSQLServer += "    //创建拼接条件及参数的类\r\n";
            strBuildWhereSQLServer += "    public sealed class BuildWhereSQLServer\r\n";
            strBuildWhereSQLServer += "    {\r\n";
            strBuildWhereSQLServer += "        //创建拼接条件及参数的方法\r\n";
            strBuildWhereSQLServer += "        public static ResultBuilder CreateWhereAndParams(List<Querying> listQuery)\r\n";
            strBuildWhereSQLServer += "        {\r\n";
            strBuildWhereSQLServer += "            ResultBuilder result = new ResultBuilder();\r\n";
            strBuildWhereSQLServer += "            StringBuilder sbWhere = new StringBuilder();\r\n";
            strBuildWhereSQLServer += "            List<SqlParameter> listParam = new List<SqlParameter>();\r\n";
            strBuildWhereSQLServer += "            sbWhere.Append(\" where 1=1 \");\r\n";
            strBuildWhereSQLServer += "            if (listQuery != null && listQuery.Count > 0)\r\n";
            strBuildWhereSQLServer += "            {\r\n";
            strBuildWhereSQLServer += "                foreach (Querying item in listQuery)\r\n";
            strBuildWhereSQLServer += "                {\r\n";
            strBuildWhereSQLServer += "                    string operate = new GetOperandValue()[(int)item.Operate];\r\n";
            strBuildWhereSQLServer += "                    sbWhere.Append(new GetConjuctionValue()[(int)item.Conjute]);\r\n";
            strBuildWhereSQLServer += "                    sbWhere.Append(item.FieldName);\r\n";
            strBuildWhereSQLServer += "                    sbWhere.Append(operate);\r\n";
            strBuildWhereSQLServer += "                    sbWhere.Append(\" @\");\r\n";
            strBuildWhereSQLServer += "                    sbWhere.Append(item.FieldName);\r\n";
            strBuildWhereSQLServer += "                    SqlParameter parm = null;\r\n";
            strBuildWhereSQLServer += "                    if (operate.Trim() == \" = \")\r\n";
            strBuildWhereSQLServer += "                    {\r\n";
            strBuildWhereSQLServer += "                        parm = new SqlParameter(\"@\" + item.FieldName, item.FieldValue);\r\n";
            strBuildWhereSQLServer += "                    }\r\n";
            strBuildWhereSQLServer += "                    else if (operate.Trim() == \"like\")\r\n";
            strBuildWhereSQLServer += "                    {\r\n";
            strBuildWhereSQLServer += "                        parm = new SqlParameter(\"@\" + item.FieldName, \"%\" + item.FieldValue + \"%\");\r\n";
            strBuildWhereSQLServer += "                    }\r\n";
            strBuildWhereSQLServer += "                    listParam.Add(parm);\r\n";
            strBuildWhereSQLServer += "                }\r\n";
            strBuildWhereSQLServer += "            }\r\n";
            strBuildWhereSQLServer += "            result.strWhere = sbWhere.ToString();\r\n";
            strBuildWhereSQLServer += "            result.listParam = listParam;\r\n";
            strBuildWhereSQLServer += "            return result;\r\n";
            strBuildWhereSQLServer += "        }\r\n";
            strBuildWhereSQLServer += "    }\r\n";
            strBuildWhereSQLServer += "}\r\n";
            FileInfo file = new FileInfo(path + @"\DAL" + "\\BuildWhereSQLServer.cs");
            StreamWriter sw = file.CreateText();
            sw.WriteLine(strBuildWhereSQLServer);
            sw.Close();
            #endregion
        }
        /// <summary>
        /// 生成数据访问层-拼接查询条件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="projectName"></param>
        /// <param name="table"></param>
        public static void WriteDALFactory(string path, string projectName)
        {
            //含有一个或多个抽象方法的类称为抽象类,抽象类不能够被实例化
            //子类继承抽象父类后，可以使用override关键字覆盖父类中的abstract方法或virtual虚方法，并做具体的实现。也可以不实现抽象方法，留给后代实现，这时子类仍旧是一个抽象类，必须声明为abstract
            string strBuildWhereSQLServer = "";
            #region 拼接开始
            strBuildWhereSQLServer += "using System;\r\n";
            strBuildWhereSQLServer += "using System.Reflection;\r\n";
            strBuildWhereSQLServer += "using " + projectName + ".Common;\r\n";
            strBuildWhereSQLServer += "using " + projectName + ".CoIDALmmon;\r\n";
            strBuildWhereSQLServer += "\r\n";
            strBuildWhereSQLServer += "namespace " + projectName + ".SQLServerDAL\r\n";
            strBuildWhereSQLServer += "{\r\n";
            strBuildWhereSQLServer += "    //返回拼接查询条件结果的Model\r\n";
            strBuildWhereSQLServer += "    public  class DALFactory\r\n";
            strBuildWhereSQLServer += "    {\r\n";
            strBuildWhereSQLServer += "        private static readonly string _path = \""+projectName+".SQLServerDAL\";\r\n";
            strBuildWhereSQLServer += "        ///<summary>\r\n";
            strBuildWhereSQLServer += "        ///通过反射机制，实例化接口对象\r\n";
            strBuildWhereSQLServer += "        ///</summary>\r\n";
            strBuildWhereSQLServer += "        ///<param name=\"CacheKey\">接口对象名称(键)</param>\r\n";
            strBuildWhereSQLServer += "        ///<returns>接口对象</returns>\r\n";
            strBuildWhereSQLServer += "        private static object GetInstance(string CacheKey)\r\n";
            strBuildWhereSQLServer += "        {\r\n";
            strBuildWhereSQLServer += "            object objType = DataCache.GetCache(CacheKey);\r\n";
            strBuildWhereSQLServer += "            if (objType == null)\r\n";
            strBuildWhereSQLServer += "            {\r\n";
            strBuildWhereSQLServer += "                try\r\n";
            strBuildWhereSQLServer += "                {\r\n";
            strBuildWhereSQLServer += "                    objType = Assembly.Load(DALFactory._path).CreateInstance(CacheKey);\r\n";
            strBuildWhereSQLServer += "                    DataCache.SetCache(CacheKey, objType);\r\n";
            strBuildWhereSQLServer += "                }\r\n";
            strBuildWhereSQLServer += "                catch (Exception ex)\r\n";
            strBuildWhereSQLServer += "                {\r\n";
            strBuildWhereSQLServer += "                    throw ex;\r\n";
            strBuildWhereSQLServer += "                }\r\n";
            strBuildWhereSQLServer += "            }\r\n";
            strBuildWhereSQLServer += "            return objType;\r\n";
            strBuildWhereSQLServer += "        }\r\n";
            strBuildWhereSQLServer += "    }\r\n";
            strBuildWhereSQLServer += "}\r\n";
            FileInfo file = new FileInfo(path + @"\DAL" + "\\DALFactory.cs");
            StreamWriter sw = file.CreateText();
            sw.WriteLine(strBuildWhereSQLServer);
            sw.Close();
            #endregion
        }
    }
}
