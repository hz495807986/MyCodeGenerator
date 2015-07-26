using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCodeGenerator.WinForm
{
    public class ProcedureInfo
    {
        //列名
        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }
        //数据类型
        private string _dateType;
        public string DataType
        {
            get { return _dateType; }
            set { _dateType = value; }
        }
        //占用字节数
        private int _length;
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        //是否允许为空
        private bool _isNull;
        public bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }
        //是否自增
        private bool _isIdentity;
        public bool IsIdentity
        {
            get { return _isPK; }
            set { _isPK = value; }
        }
        //是否主键
        private bool _isPK;
        public bool IsPK
        {
            get { return _isPK; }
            set { _isPK = value; }
        }
        //字段描述
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }



        public static string GetColumnInfoSql(string tablename)
        {
            string sql = @"SELECT col.name AS 列名, typ.name as 数据类型,col.max_length AS 占用字节数,
col.is_nullable  AS 是否允许非空,col.is_identity  AS 是否自增,
case when exists  
(SELECT 1  FROM sys.indexes idx join sys.index_columns idxCol 
on (idx.object_id = idxCol.object_id)
WHERE idx.object_id = col.object_id AND idxCol.index_column_id = col.column_id 
AND idx.is_primary_key = 1 ) THEN 1 ELSE 0 END  AS 是否是主键,
isnull(e.value,'') as 描述
FROM sys.columns col 
left join sys.types typ 
on (col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id) 
left join sys.extended_properties e 
on e.major_id=col.object_id and e.minor_id=col.column_id
WHERE col.object_id =(SELECT object_id FROM sys.tables WHERE name = '" + tablename + "')";
            return sql;
        }
    }
}
