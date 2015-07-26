namespace MyCodeGenerator.WinForm
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboxSSPI = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnConnTest = new System.Windows.Forms.Button();
            this.txtLoginAccount = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDBHelper = new System.Windows.Forms.TextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnDBHelper = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.btnCreateSQL = new System.Windows.Forms.Button();
            this.btnCreateLayers = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cboxModel = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboxBLL = new System.Windows.Forms.CheckBox();
            this.cboxDAL = new System.Windows.Forms.CheckBox();
            this.btnSelectTables = new System.Windows.Forms.Button();
            this.btnSelectSavePath = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.cboxDBSource = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cboxSSPI);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.btnConnTest);
            this.groupBox1.Controls.Add(this.txtLoginAccount);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 97);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库服务信息";
            // 
            // cboxSSPI
            // 
            this.cboxSSPI.AutoSize = true;
            this.cboxSSPI.Location = new System.Drawing.Point(262, 61);
            this.cboxSSPI.Name = "cboxSSPI";
            this.cboxSSPI.Size = new System.Drawing.Size(72, 16);
            this.cboxSSPI.TabIndex = 6;
            this.cboxSSPI.Text = "集成验证";
            this.cboxSSPI.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(66, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(148, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // btnConnTest
            // 
            this.btnConnTest.Location = new System.Drawing.Point(393, 57);
            this.btnConnTest.Name = "btnConnTest";
            this.btnConnTest.Size = new System.Drawing.Size(101, 23);
            this.btnConnTest.TabIndex = 1;
            this.btnConnTest.Text = "连接测试";
            this.btnConnTest.UseVisualStyleBackColor = true;
            this.btnConnTest.Click += new System.EventHandler(this.btnConnTest_Click);
            // 
            // txtLoginAccount
            // 
            this.txtLoginAccount.Location = new System.Drawing.Point(318, 20);
            this.txtLoginAccount.Name = "txtLoginAccount";
            this.txtLoginAccount.Size = new System.Drawing.Size(176, 21);
            this.txtLoginAccount.TabIndex = 4;
            this.txtLoginAccount.Text = "sa";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(66, 20);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(148, 21);
            this.txtServer.TabIndex = 3;
            this.txtServer.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "登录密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "登录账号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.txtDBHelper);
            this.groupBox2.Controls.Add(this.btnCancle);
            this.groupBox2.Controls.Add(this.btnDBHelper);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnCreateSQL);
            this.groupBox2.Controls.Add(this.btnCreateLayers);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.cboxModel);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.cboxBLL);
            this.groupBox2.Controls.Add(this.cboxDAL);
            this.groupBox2.Controls.Add(this.btnSelectTables);
            this.groupBox2.Controls.Add(this.btnSelectSavePath);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSavePath);
            this.groupBox2.Controls.Add(this.cboxDBSource);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 332);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库操作信息";
            // 
            // txtDBHelper
            // 
            this.txtDBHelper.Location = new System.Drawing.Point(135, 200);
            this.txtDBHelper.Name = "txtDBHelper";
            this.txtDBHelper.Size = new System.Drawing.Size(119, 21);
            this.txtDBHelper.TabIndex = 37;
            this.txtDBHelper.Text = "DBHelp";
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(393, 288);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(109, 23);
            this.btnCancle.TabIndex = 36;
            this.btnCancle.Text = "取  消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Visible = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnDBHelper
            // 
            this.btnDBHelper.Location = new System.Drawing.Point(412, 196);
            this.btnDBHelper.Name = "btnDBHelper";
            this.btnDBHelper.Size = new System.Drawing.Size(75, 23);
            this.btnDBHelper.TabIndex = 30;
            this.btnDBHelper.Text = "帮助类";
            this.btnDBHelper.UseVisualStyleBackColor = true;
            this.btnDBHelper.Click += new System.EventHandler(this.btnDBHelper_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtProjectName);
            this.groupBox3.Location = new System.Drawing.Point(10, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(484, 111);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "命名空间";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 19;
            this.label12.Text = "项目名称";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(77, 20);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(136, 21);
            this.txtProjectName.TabIndex = 18;
            this.txtProjectName.Text = "Ariel";
            // 
            // btnCreateSQL
            // 
            this.btnCreateSQL.Location = new System.Drawing.Point(13, 290);
            this.btnCreateSQL.Name = "btnCreateSQL";
            this.btnCreateSQL.Size = new System.Drawing.Size(106, 23);
            this.btnCreateSQL.TabIndex = 17;
            this.btnCreateSQL.Text = "生成SQL";
            this.btnCreateSQL.UseVisualStyleBackColor = true;
            this.btnCreateSQL.Click += new System.EventHandler(this.btnCreateSQL_Click);
            // 
            // btnCreateLayers
            // 
            this.btnCreateLayers.Location = new System.Drawing.Point(210, 290);
            this.btnCreateLayers.Name = "btnCreateLayers";
            this.btnCreateLayers.Size = new System.Drawing.Size(109, 23);
            this.btnCreateLayers.TabIndex = 14;
            this.btnCreateLayers.Text = "生成三层";
            this.btnCreateLayers.UseVisualStyleBackColor = true;
            this.btnCreateLayers.Click += new System.EventHandler(this.btnCreateLayers_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(36, 203);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 32;
            this.label21.Text = "帮助类名称";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 259);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(492, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // cboxModel
            // 
            this.cboxModel.AutoSize = true;
            this.cboxModel.Checked = true;
            this.cboxModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxModel.Location = new System.Drawing.Point(386, 232);
            this.cboxModel.Name = "cboxModel";
            this.cboxModel.Size = new System.Drawing.Size(60, 16);
            this.cboxModel.TabIndex = 34;
            this.cboxModel.Text = "实体类";
            this.cboxModel.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 233);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 24;
            this.label15.Text = "选择要生成的类";
            // 
            // cboxBLL
            // 
            this.cboxBLL.AutoSize = true;
            this.cboxBLL.Checked = true;
            this.cboxBLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxBLL.Location = new System.Drawing.Point(257, 232);
            this.cboxBLL.Name = "cboxBLL";
            this.cboxBLL.Size = new System.Drawing.Size(84, 16);
            this.cboxBLL.TabIndex = 23;
            this.cboxBLL.Text = "业务逻辑类";
            this.cboxBLL.UseVisualStyleBackColor = true;
            // 
            // cboxDAL
            // 
            this.cboxDAL.AutoSize = true;
            this.cboxDAL.Checked = true;
            this.cboxDAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxDAL.Location = new System.Drawing.Point(139, 232);
            this.cboxDAL.Name = "cboxDAL";
            this.cboxDAL.Size = new System.Drawing.Size(84, 16);
            this.cboxDAL.TabIndex = 22;
            this.cboxDAL.Text = "数据访问类";
            this.cboxDAL.UseVisualStyleBackColor = true;
            // 
            // btnSelectTables
            // 
            this.btnSelectTables.Location = new System.Drawing.Point(412, 18);
            this.btnSelectTables.Name = "btnSelectTables";
            this.btnSelectTables.Size = new System.Drawing.Size(75, 23);
            this.btnSelectTables.TabIndex = 7;
            this.btnSelectTables.Text = "选择表";
            this.btnSelectTables.UseVisualStyleBackColor = true;
            this.btnSelectTables.Click += new System.EventHandler(this.btnSelectTables_Click);
            // 
            // btnSelectSavePath
            // 
            this.btnSelectSavePath.Location = new System.Drawing.Point(413, 55);
            this.btnSelectSavePath.Name = "btnSelectSavePath";
            this.btnSelectSavePath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSavePath.TabIndex = 6;
            this.btnSelectSavePath.Text = "浏览";
            this.btnSelectSavePath.UseVisualStyleBackColor = true;
            this.btnSelectSavePath.Click += new System.EventHandler(this.btnSelectSavePath_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "存储路径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "数据库";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(62, 56);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(303, 21);
            this.txtSavePath.TabIndex = 1;
            // 
            // cboxDBSource
            // 
            this.cboxDBSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxDBSource.FormattingEnabled = true;
            this.cboxDBSource.Location = new System.Drawing.Point(62, 20);
            this.cboxDBSource.Name = "cboxDBSource";
            this.cboxDBSource.Size = new System.Drawing.Size(303, 20);
            this.cboxDBSource.TabIndex = 0;
            this.cboxDBSource.SelectedIndexChanged += new System.EventHandler(this.cboxDBSource_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(541, 459);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormMain";
            this.Text = "代码生成器";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cboxSSPI;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnConnTest;
        private System.Windows.Forms.TextBox txtLoginAccount;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDBHelper;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Button btnCreateSQL;
        private System.Windows.Forms.Button btnCreateLayers;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox cboxModel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cboxBLL;
        private System.Windows.Forms.CheckBox cboxDAL;
        private System.Windows.Forms.Button btnSelectTables;
        private System.Windows.Forms.Button btnSelectSavePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.ComboBox cboxDBSource;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.TextBox txtDBHelper;

    }
}

