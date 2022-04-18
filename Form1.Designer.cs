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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_lasthb = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_heartbeat_1 = new System.Windows.Forms.Label();
            this.button_startbeat = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.remoteTipLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.remoteKeyInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remoteHostInput = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.data_wechat_View = new System.Windows.Forms.DataGridView();
            this.label_wechat_tip = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.button3 = new System.Windows.Forms.Button();
            this.wechat_add_input = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_wechat_View)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(493, 265);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(485, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "状态";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label_lasthb);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label_heartbeat_1);
            this.tabPage2.Controls.Add(this.button_startbeat);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.remoteKeyInput);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.remoteHostInput);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(485, 235);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "远端配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label_lasthb
            // 
            this.label_lasthb.AutoSize = true;
            this.label_lasthb.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label_lasthb.Location = new System.Drawing.Point(123, 122);
            this.label_lasthb.Name = "label_lasthb";
            this.label_lasthb.Size = new System.Drawing.Size(0, 17);
            this.label_lasthb.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "上一次上报状态:";
            // 
            // label_heartbeat_1
            // 
            this.label_heartbeat_1.AutoSize = true;
            this.label_heartbeat_1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label_heartbeat_1.Location = new System.Drawing.Point(123, 95);
            this.label_heartbeat_1.Name = "label_heartbeat_1";
            this.label_heartbeat_1.Size = new System.Drawing.Size(92, 17);
            this.label_heartbeat_1.TabIndex = 9;
            this.label_heartbeat_1.Text = "心跳上报未运行";
            // 
            // button_startbeat
            // 
            this.button_startbeat.Location = new System.Drawing.Point(19, 88);
            this.button_startbeat.Name = "button_startbeat";
            this.button_startbeat.Size = new System.Drawing.Size(101, 30);
            this.button_startbeat.TabIndex = 8;
            this.button_startbeat.Text = "启动心跳上报";
            this.button_startbeat.UseVisualStyleBackColor = true;
            this.button_startbeat.Click += new System.EventHandler(this.button_startbeat_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.remoteTipLabel);
            this.groupBox1.Location = new System.Drawing.Point(339, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 83);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // remoteTipLabel
            // 
            this.remoteTipLabel.Location = new System.Drawing.Point(3, 15);
            this.remoteTipLabel.Name = "remoteTipLabel";
            this.remoteTipLabel.Size = new System.Drawing.Size(136, 60);
            this.remoteTipLabel.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(268, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "心跳测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "通信密钥:";
            // 
            // remoteKeyInput
            // 
            this.remoteKeyInput.Location = new System.Drawing.Point(78, 56);
            this.remoteKeyInput.Name = "remoteKeyInput";
            this.remoteKeyInput.PasswordChar = '*';
            this.remoteKeyInput.Size = new System.Drawing.Size(181, 24);
            this.remoteKeyInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "远端地址:";
            // 
            // remoteHostInput
            // 
            this.remoteHostInput.Location = new System.Drawing.Point(78, 15);
            this.remoteHostInput.Name = "remoteHostInput";
            this.remoteHostInput.Size = new System.Drawing.Size(181, 24);
            this.remoteHostInput.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.data_wechat_View);
            this.tabPage3.Controls.Add(this.label_wechat_tip);
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.linkLabel2);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.wechat_add_input);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(485, 235);
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
            this.data_wechat_View.Location = new System.Drawing.Point(4, 70);
            this.data_wechat_View.Name = "data_wechat_View";
            this.data_wechat_View.ReadOnly = true;
            this.data_wechat_View.RowTemplate.Height = 26;
            this.data_wechat_View.Size = new System.Drawing.Size(474, 146);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(401, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(66, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "启动微信";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(340, 216);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(144, 17);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "下载微信 3.4.5.27 安装包";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(401, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(66, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "选择";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // wechat_add_input
            // 
            this.wechat_add_input.Location = new System.Drawing.Point(102, 8);
            this.wechat_add_input.Name = "wechat_add_input";
            this.wechat_add_input.Size = new System.Drawing.Size(288, 24);
            this.wechat_add_input.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "微信安装目录:";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(485, 235);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "支付宝监听";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(493, 262);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "支付监听回调";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_wechat_View)).EndInit();
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
        private Label label_lasthb;
        private Label label3;
        private TextBox wechat_add_input;
        private Label label4;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button3;
        private LinkLabel linkLabel2;
        private Button button4;
        private Label label_wechat_tip;
        private DataGridView data_wechat_View;
    }
}