using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyCodeGenerator.WinForm
{
    public partial class FormMain : Form
    {
        Thread th = null;
        string connectstring;   //数据库连接字符串
        SqlConnection cn;       //数据库连接对象
        List<ProcedureInfo> list = new List<ProcedureInfo>();   //表对象
        public List<string> Allli = new List<string>();         //所有表
        public List<string> listSelectdTables = new List<string>();            //所关联的表
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //默认桌面路径
        string userId = "";        //用户名
        string password = "";      //密码
        string database = "master";//数据库
        string Id;                  //Id
        FileInfo file = null;       //文件流
        StreamWriter writer = null; //写入流
       
        string table = "";         //当前表名
        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //开启线程间调用
            Control.CheckForIllegalCrossThreadCalls = false;
            this.btnCancle.Visible = false;
        }
        //测试数据库连接
        private void btnConnTest_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            database = txtServer.Text;
            userId = txtLoginAccount.Text;
            password = txtPassword.Text;

            this.cboxDBSource.Items.Clear();
            
         
            string sql = string.Format("select name from sys.databases");
            if (cboxSSPI.Checked)
            {
                //身份验证验证
                connectstring = string.Format(" data source={0}; initial catalog=master;integrated security=sspi", database);
            }
            else
            {
                //用户名密码验证
                connectstring = string.Format(" data source={0}; initial catalog=master;user id={1};password={2}", database, userId, password);
            }
            cn = new SqlConnection(connectstring);
            try
            {

                cn.Open();
                SqlCommand cm = new SqlCommand(sql, cn);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        cboxDBSource.Items.Add(dr[0].ToString());
                        Allli.Add(dr[0].ToString());
                    }
                }
            }
            catch
            {
                groupBox2.Enabled = false;
                MessageBox.Show("用户登录失败（请检查用户名和密码）","提示");
            }
            this.cboxDBSource.SelectedIndex = this.cboxDBSource.Items.IndexOf("master");
        }
        //这一步非常重要，选择数据库后获取数据库下的表，否则生成不了任何东西
        private void cboxDBSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            database = cboxDBSource.Text;
            try
            {
                cn.Open();
                cn.ChangeDatabase(cboxDBSource.Text);
                listSelectdTables.Clear();
                SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        listSelectdTables.Add(dr[0].ToString());
                    }
                }
            }
            catch { MessageBox.Show("该数据库不可操作 请尝试关闭重试"); cn.Close(); }

            finally
            {

            }
        }
        //选择表
        private void btnSelectTables_Click(object sender, EventArgs e)
        {
            cn.Open();
            cn.ChangeDatabase(cboxDBSource.Text);
            SqlCommand cm = new SqlCommand("select name from sys.Tables", cn);
            SqlDataReader dr = cm.ExecuteReader();
            listSelectdTables.Clear();
            while (dr.Read())
            {
                listSelectdTables.Add(dr[0].ToString());

            }
            dr.Close();
            cn.Close();
            FormSelectTables _formSelectTables = new FormSelectTables(listSelectdTables);
            _formSelectTables.fmain = this;
            _formSelectTables.ShowDialog();
        }

        private void btnSelectSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.SelectedPath;
                this.txtSavePath.Text = path;
            }
        }
        //帮助类
        private void btnDBHelper_Click(object sender, EventArgs e)
        {
            FormDBHelper f = new FormDBHelper();
            f.Show();
        }
        //生成SQL-数据库INSERT 语句
        private void btnCreateSQL_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnCancle.Visible = true;
                th=new Thread(new ThreadStart(CreateSQL));
                th.Start();
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"提示");
            }
            
        }
        private void CreateSQL()
        {
            string SQLstring = "";
            this.progressBar1.Maximum = listSelectdTables.Count;
            int count = 0;
            foreach (string tablename in listSelectdTables)
            {
                count++;
                this.progressBar1.Value = count;
                string sql = ProcedureInfo.GetColumnInfoSql(tablename);
                List<ProcedureInfo> list = new List<ProcedureInfo>();

                cn.Open();
                cn.ChangeDatabase(cboxDBSource.Text);
                SqlCommand cm = new SqlCommand(sql, cn);
                string table = tablename.Substring(0, 1).ToUpper() + tablename.Substring(1);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        ProcedureInfo pro = new ProcedureInfo();
                        pro.ColumnName = (string)dr[0];
                        pro.DataType = (string)dr[1];
                        pro.Length = Convert.ToInt32(dr[2]);
                        pro.IsNull = Convert.ToBoolean(dr[3]);
                        pro.IsIdentity = Convert.ToBoolean(dr[4]);
                        pro.IsPK = Convert.ToBoolean(dr[5]);
                        pro.Description = Convert.ToString(dr[6]);
                        list.Add(pro);
                    }
                }
                SQLstring += "go\r\n";
                cn.Open();
                cn.ChangeDatabase(cboxDBSource.Text);
                sql = "select * from " + table;
                cm = new SqlCommand(sql, cn);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        SQLstring += "insert into " + table + " values(";
                        int i = 0;
                        foreach (ProcedureInfo pro in list)
                        {
                            if (pro.IsIdentity)
                            {
                                i++;
                                continue;
                            }
                            if (DBNull.Value == dr[i])
                            {
                                SQLstring += "null";
                            }
                            else if (MyHelper.findModelsType(pro.DataType) == "string" || MyHelper.findModelsType(pro.DataType) == "DateTime")
                            {
                                SQLstring += "'";
                                SQLstring += dr[i].ToString();
                                SQLstring += "'";
                            }
                            else if (MyHelper.findModelsType(pro.DataType) == "bool")
                            {
                                if (Convert.ToBoolean(dr[i]))
                                    SQLstring += 1.ToString();
                                else
                                    SQLstring += 0.ToString();
                            }
                            else
                            {
                                SQLstring += dr[i].ToString();
                            }
                            if (i < list.Count - 1)
                            {
                                SQLstring += " ,";
                            }
                            i++;
                        }
                        SQLstring += ")";
                        SQLstring += "\r\n";

                    }
                }
            }
            if (!Directory.Exists(path + @"\SQL"))
            {
                Directory.CreateDirectory(path + @"\SQL");
            }
            file = new FileInfo(path + @"\SQL" + "\\" + cboxDBSource.Text + "SQL.txt");
            writer = file.CreateText();
            writer.WriteLine(SQLstring);
            writer.Close();
            
            MessageBox.Show("生成成功！", "提示");
            this.progressBar1.Value = 0;
            btnCancle.Visible = false;
        }
        //生成三层
        private void btnCreateLayers_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnCancle.Visible = true;
                th = new Thread(new ThreadStart(Add));
                th.Start();
            }
            catch (Exception)
            {
                    
                throw;
            }
            
            
        }
        public void Add()
        {
            int count = 0;
            if (cboxDAL.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\DAL"))
                {
                    Directory.CreateDirectory(path + @"\DAL");
                }
            }
            if (cboxBLL.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\BLL"))
                {
                    Directory.CreateDirectory(path + @"\BLL");
                }
            }
            if (cboxModel.Checked)
            {
                count++;
                if (!Directory.Exists(path + @"\Models"))
                {
                    Directory.CreateDirectory(path + @"\Models");
                }
            }
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = listSelectdTables.Count;

            foreach (string tablename in listSelectdTables)
            {
                list = new List<ProcedureInfo>();
                cn.Open();
                cn.ChangeDatabase(cboxDBSource.Text);
                SqlCommand cm = new SqlCommand(ProcedureInfo.GetColumnInfoSql(tablename), cn);
                table = tablename.Substring(0, 1).ToUpper() + tablename.Substring(1);
                using (SqlDataReader dr = cm.ExecuteReader(CommandBehavior.CloseConnection))
                {

                    while (dr.Read())
                    {
                        ProcedureInfo pro = new ProcedureInfo();
                        pro.ColumnName = (string)dr[0];
                        pro.DataType = (string)dr[1];
                        pro.Length = Convert.ToInt32(dr[2]);
                        pro.IsNull = Convert.ToBoolean(dr[3]);
                        pro.IsIdentity = Convert.ToBoolean(dr[4]);
                        pro.IsPK = Convert.ToBoolean(dr[5]);
                        pro.Description = Convert.ToString(dr[6]);
                        list.Add(pro);
                    }
                }
                if (cboxDAL.Checked)
                {
                    BuilderDAL.WriteDAL(path, txtProjectName.Text.Trim(), table, list);
                    BuilderDAL.WriteBaseDAL(path, txtProjectName.Text.Trim(),list);
                    BuilderDAL.WriteBuildWhereSQLServer(path, txtProjectName.Text.Trim());
                }
                if (cboxBLL.Checked)
                {

                    BuilderBLL.WriteBLL(path, table, list, txtProjectName.Text.Trim(), txtProjectName.Text.Trim(), txtProjectName.Text.Trim());
                }

                if (cboxModel.Checked)
                {
                    BuilderModel.WriteModel(path, table, list, txtProjectName.Text.Trim());
                }
                progressBar1.Value++;
            }
            this.progressBar1.Value = 0;
            MessageBox.Show("生成成功！", "提示");
            btnCancle.Visible = false;
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.progressBar1.Value = 0;
            th.Abort();
            this.btnCancle.Visible = false;//不显示
        }

       
       
       
    }
}
