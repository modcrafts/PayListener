namespace PayListener
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.time = new System.Windows.Forms.ColumnHeader();
            this.status = new System.Windows.Forms.ColumnHeader();
            this.detail = new System.Windows.Forms.ColumnHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remoteHostInput = new System.Windows.Forms.TextBox();
            this.remoteKeyInput = new System.Windows.Forms.TextBox();
            this.label_heartbeat_1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_startbeat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.remoteTipLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.data_wechat_View = new System.Windows.Forms.DataGridView();
            this.label_wechat_tip = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wechat_add_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.data_alipay_View = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_startAlipay = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button_alipayFreshCookie = new System.Windows.Forms.Button();
            this.alipayIntervaltext = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_wechat_View)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_alipay_View)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(493, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(485, 232);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "状态";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(485, 232);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "远端配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.status,
            this.detail});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 119);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(479, 110);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "时间";
            // 
            // status
            // 
            this.status.Text = "状态";
            // 
            // detail
            // 
            this.detail.Text = "详细信息";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.remoteHostInput);
            this.panel2.Controls.Add(this.remoteKeyInput);
            this.panel2.Controls.Add(this.label_heartbeat_1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button_startbeat);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 116);
            this.panel2.TabIndex = 12;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(55, 21);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "https";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "远端地址:";
            // 
            // remoteHostInput
            // 
            this.remoteHostInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteHostInput.Location = new System.Drawing.Point(65, 7);
            this.remoteHostInput.Name = "remoteHostInput";
            this.remoteHostInput.Size = new System.Drawing.Size(186, 24);
            this.remoteHostInput.TabIndex = 0;
            // 
            // remoteKeyInput
            // 
            this.remoteKeyInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteKeyInput.Location = new System.Drawing.Point(65, 48);
            this.remoteKeyInput.Name = "remoteKeyInput";
            this.remoteKeyInput.PasswordChar = '*';
            this.remoteKeyInput.Size = new System.Drawing.Size(186, 24);
            this.remoteKeyInput.TabIndex = 2;
            // 
            // label_heartbeat_1
            // 
            this.label_heartbeat_1.AutoSize = true;
            this.label_heartbeat_1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label_heartbeat_1.Location = new System.Drawing.Point(110, 87);
            this.label_heartbeat_1.Name = "label_heartbeat_1";
            this.label_heartbeat_1.Size = new System.Drawing.Size(92, 17);
            this.label_heartbeat_1.TabIndex = 9;
            this.label_heartbeat_1.Text = "心跳上报未运行";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "通信密钥:";
            // 
            // button_startbeat
            // 
            this.button_startbeat.Location = new System.Drawing.Point(6, 80);
            this.button_startbeat.Name = "button_startbeat";
            this.button_startbeat.Size = new System.Drawing.Size(101, 30);
            this.button_startbeat.TabIndex = 8;
            this.button_startbeat.Text = "启动心跳上报";
            this.button_startbeat.UseVisualStyleBackColor = true;
            this.button_startbeat.Click += new System.EventHandler(this.button_startbeat_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(257, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "心跳测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.remoteTipLabel);
            this.groupBox1.Location = new System.Drawing.Point(331, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 83);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // remoteTipLabel
            // 
            this.remoteTipLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteTipLabel.Location = new System.Drawing.Point(3, 15);
            this.remoteTipLabel.Name = "remoteTipLabel";
            this.remoteTipLabel.Size = new System.Drawing.Size(136, 60);
            this.remoteTipLabel.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(257, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.data_wechat_View);
            this.tabPage3.Controls.Add(this.label_wechat_tip);
            this.tabPage3.Controls.Add(this.linkLabel2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(485, 232);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "微信监听";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // data_wechat_View
            // 
            this.data_wechat_View.AllowUserToAddRows = false;
            this.data_wechat_View.AllowUserToDeleteRows = false;
            this.data_wechat_View.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_wechat_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_wechat_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_wechat_View.Location = new System.Drawing.Point(3, 35);
            this.data_wechat_View.Name = "data_wechat_View";
            this.data_wechat_View.ReadOnly = true;
            this.data_wechat_View.RowTemplate.Height = 26;
            this.data_wechat_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data_wechat_View.Size = new System.Drawing.Size(479, 177);
            this.data_wechat_View.TabIndex = 9;
            // 
            // label_wechat_tip
            // 
            this.label_wechat_tip.AutoSize = true;
            this.label_wechat_tip.Location = new System.Drawing.Point(6, 216);
            this.label_wechat_tip.Name = "label_wechat_tip";
            this.label_wechat_tip.Size = new System.Drawing.Size(0, 17);
            this.label_wechat_tip.TabIndex = 8;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkLabel2.Location = new System.Drawing.Point(3, 212);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(479, 17);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "下载微信 3.4.5.27 安装包";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wechat_add_input);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 32);
            this.panel1.TabIndex = 10;
            // 
            // wechat_add_input
            // 
            this.wechat_add_input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wechat_add_input.Location = new System.Drawing.Point(94, 3);
            this.wechat_add_input.Name = "wechat_add_input";
            this.wechat_add_input.Size = new System.Drawing.Size(234, 24);
            this.wechat_add_input.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "微信安装目录:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(334, 3);
            this.button3.Margin = new System.Windows.Forms.Padding(10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 24);
            this.button3.TabIndex = 2;
            this.button3.Text = "选择";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(408, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 24);
            this.button4.TabIndex = 7;
            this.button4.Text = "启动微信";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.data_alipay_View);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(485, 232);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "支付宝监听";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // data_alipay_View
            // 
            this.data_alipay_View.AllowUserToAddRows = false;
            this.data_alipay_View.AllowUserToDeleteRows = false;
            this.data_alipay_View.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_alipay_View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_alipay_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_alipay_View.Location = new System.Drawing.Point(3, 31);
            this.data_alipay_View.Name = "data_alipay_View";
            this.data_alipay_View.ReadOnly = true;
            this.data_alipay_View.RowTemplate.Height = 26;
            this.data_alipay_View.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data_alipay_View.Size = new System.Drawing.Size(479, 198);
            this.data_alipay_View.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_startAlipay);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.button_alipayFreshCookie);
            this.panel3.Controls.Add(this.alipayIntervaltext);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(479, 28);
            this.panel3.TabIndex = 4;
            // 
            // button_startAlipay
            // 
            this.button_startAlipay.Location = new System.Drawing.Point(5, 1);
            this.button_startAlipay.Name = "button_startAlipay";
            this.button_startAlipay.Size = new System.Drawing.Size(75, 24);
            this.button_startAlipay.TabIndex = 0;
            this.button_startAlipay.Text = "启动";
            this.button_startAlipay.UseVisualStyleBackColor = true;
            this.button_startAlipay.Click += new System.EventHandler(this.button5_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(84, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "刷新间隔 (秒)";
            // 
            // button_alipayFreshCookie
            // 
            this.button_alipayFreshCookie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_alipayFreshCookie.Enabled = false;
            this.button_alipayFreshCookie.Location = new System.Drawing.Point(384, 1);
            this.button_alipayFreshCookie.Name = "button_alipayFreshCookie";
            this.button_alipayFreshCookie.Size = new System.Drawing.Size(92, 24);
            this.button_alipayFreshCookie.TabIndex = 1;
            this.button_alipayFreshCookie.Text = "刷新 Cookie";
            this.button_alipayFreshCookie.UseVisualStyleBackColor = true;
            this.button_alipayFreshCookie.Click += new System.EventHandler(this.button_alipayFreshCookie_Click);
            // 
            // alipayIntervaltext
            // 
            this.alipayIntervaltext.Location = new System.Drawing.Point(170, 2);
            this.alipayIntervaltext.Name = "alipayIntervaltext";
            this.alipayIntervaltext.Size = new System.Drawing.Size(44, 24);
            this.alipayIntervaltext.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(485, 232);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "关于";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 51);
            this.label3.TabIndex = 0;
            this.label3.Text = "本软件免费使用\r\n版本: Beta 0.1.1.0\r\nQQ群: 312558935";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 262);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(509, 301);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "支付监听回调";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_wechat_View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_alipay_View)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox remoteHostInput;
        private TabPage tabPage4;
        private Label label2;
        private TextBox remoteKeyInput;
        private Label label1;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private Label remoteTipLabel;
        private Label label_heartbeat_1;
        private Button button_startbeat;
        private TextBox wechat_add_input;
        private Label label4;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button3;
        private LinkLabel linkLabel2;
        private Button button4;
        private Label label_wechat_tip;
        private DataGridView data_wechat_View;
        private Button button_startAlipay;
        private Button button_alipayFreshCookie;
        private TextBox alipayIntervaltext;
        private Label label5;
        private Panel panel1;
        private Panel panel2;
        private ColumnHeader time;
        private ColumnHeader status;
        private ColumnHeader detail;
        public ListView listView1;
        private Panel panel3;
        private DataGridView data_alipay_View;
        private TabPage tabPage5;
        private Label label3;
        private CheckBox checkBox1;
    }
}