namespace TaewooBot_v2
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MarketType = new System.Windows.Forms.ComboBox();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.API = new AxKHOpenAPILib.AxKHOpenAPI();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.CurrentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.TestBtn = new System.Windows.Forms.Button();
            this.TestText = new System.Windows.Forms.TextBox();
            this.DisplayBtn = new System.Windows.Forms.Button();
            this.DelBtn = new System.Windows.Forms.Button();
            this.GetDeposit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TotalPnLTextBox = new System.Windows.Forms.TextBox();
            this.DepositTextBox = new System.Windows.Forms.TextBox();
            this.AccountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.signalCnt = new System.Windows.Forms.TextBox();
            this.TestCheck = new System.Windows.Forms.CheckBox();
            this.GetDataTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.version = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OrderTextBox = new System.Windows.Forms.TextBox();
            this.MonitoringTextBox = new System.Windows.Forms.TextBox();
            this.process1 = new System.Diagnostics.Process();
            this.ParamsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.API)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // MarketType
            // 
            this.MarketType.BackColor = System.Drawing.SystemColors.Info;
            this.MarketType.FormattingEnabled = true;
            this.MarketType.Items.AddRange(new object[] {
            "Stock",
            "Coin"});
            this.MarketType.Location = new System.Drawing.Point(13, 18);
            this.MarketType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MarketType.Name = "MarketType";
            this.MarketType.Size = new System.Drawing.Size(171, 26);
            this.MarketType.TabIndex = 3;
            this.MarketType.SelectedIndexChanged += new System.EventHandler(this.MarketType_SelectedIndexChanged);
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(211, 14);
            this.Start_Btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(107, 34);
            this.Start_Btn.TabIndex = 4;
            this.Start_Btn.Text = "Start";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // API
            // 
            this.API.Enabled = true;
            this.API.Location = new System.Drawing.Point(280, 74);
            this.API.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.API.Name = "API";
            this.API.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("API.OcxState")));
            this.API.Size = new System.Drawing.Size(150, 143);
            this.API.TabIndex = 7;
            this.API.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.CurrentTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 477);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(582, 32);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "time";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(210, 25);
            this.Status.Text = "Welcome to TaewooBot";
            // 
            // CurrentTime
            // 
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(82, 25);
            this.CurrentTime.Text = "time line";
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(234, 1050);
            this.TestBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(107, 34);
            this.TestBtn.TabIndex = 10;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // TestText
            // 
            this.TestText.Location = new System.Drawing.Point(29, 1050);
            this.TestText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestText.Name = "TestText";
            this.TestText.Size = new System.Drawing.Size(195, 28);
            this.TestText.TabIndex = 11;
            // 
            // DisplayBtn
            // 
            this.DisplayBtn.Location = new System.Drawing.Point(401, 1050);
            this.DisplayBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DisplayBtn.Name = "DisplayBtn";
            this.DisplayBtn.Size = new System.Drawing.Size(107, 34);
            this.DisplayBtn.TabIndex = 12;
            this.DisplayBtn.Text = "Display";
            this.DisplayBtn.UseVisualStyleBackColor = true;
            this.DisplayBtn.Click += new System.EventHandler(this.DisplayBtn_Click);
            // 
            // DelBtn
            // 
            this.DelBtn.Location = new System.Drawing.Point(517, 1050);
            this.DelBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(107, 34);
            this.DelBtn.TabIndex = 13;
            this.DelBtn.Text = "Delete";
            this.DelBtn.UseVisualStyleBackColor = true;
            this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
            // 
            // GetDeposit
            // 
            this.GetDeposit.Location = new System.Drawing.Point(667, 1048);
            this.GetDeposit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GetDeposit.Name = "GetDeposit";
            this.GetDeposit.Size = new System.Drawing.Size(107, 34);
            this.GetDeposit.TabIndex = 14;
            this.GetDeposit.Text = "Deposit";
            this.GetDeposit.UseVisualStyleBackColor = true;
            this.GetDeposit.Click += new System.EventHandler(this.GetDeposit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TotalPnLTextBox);
            this.groupBox3.Controls.Add(this.DepositTextBox);
            this.groupBox3.Controls.Add(this.AccountTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.API);
            this.groupBox3.Location = new System.Drawing.Point(13, 86);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(551, 183);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "계좌현황";
            // 
            // TotalPnLTextBox
            // 
            this.TotalPnLTextBox.Location = new System.Drawing.Point(97, 124);
            this.TotalPnLTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TotalPnLTextBox.Name = "TotalPnLTextBox";
            this.TotalPnLTextBox.Size = new System.Drawing.Size(141, 28);
            this.TotalPnLTextBox.TabIndex = 5;
            // 
            // DepositTextBox
            // 
            this.DepositTextBox.Location = new System.Drawing.Point(97, 74);
            this.DepositTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DepositTextBox.Name = "DepositTextBox";
            this.DepositTextBox.Size = new System.Drawing.Size(141, 28);
            this.DepositTextBox.TabIndex = 4;
            // 
            // AccountTextBox
            // 
            this.AccountTextBox.Location = new System.Drawing.Point(97, 27);
            this.AccountTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AccountTextBox.Name = "AccountTextBox";
            this.AccountTextBox.Size = new System.Drawing.Size(141, 28);
            this.AccountTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "총 수익률";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "예수금";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "계좌번호";
            // 
            // signalCnt
            // 
            this.signalCnt.Location = new System.Drawing.Point(400, 68);
            this.signalCnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signalCnt.Name = "signalCnt";
            this.signalCnt.Size = new System.Drawing.Size(141, 28);
            this.signalCnt.TabIndex = 6;
            // 
            // TestCheck
            // 
            this.TestCheck.AutoSize = true;
            this.TestCheck.Location = new System.Drawing.Point(329, 20);
            this.TestCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TestCheck.Name = "TestCheck";
            this.TestCheck.Size = new System.Drawing.Size(69, 22);
            this.TestCheck.TabIndex = 17;
            this.TestCheck.Text = "Test";
            this.TestCheck.UseVisualStyleBackColor = true;
            this.TestCheck.CheckedChanged += new System.EventHandler(this.TestCheck_CheckedChanged);
            // 
            // GetDataTextBox
            // 
            this.GetDataTextBox.Location = new System.Drawing.Point(204, 30);
            this.GetDataTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GetDataTextBox.Name = "GetDataTextBox";
            this.GetDataTextBox.Size = new System.Drawing.Size(141, 28);
            this.GetDataTextBox.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.version);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.signalCnt);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.OrderTextBox);
            this.groupBox4.Controls.Add(this.MonitoringTextBox);
            this.groupBox4.Controls.Add(this.GetDataTextBox);
            this.groupBox4.Location = new System.Drawing.Point(13, 278);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(551, 200);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bot Status";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(429, 177);
            this.version.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(111, 18);
            this.version.TabIndex = 24;
            this.version.Text = "version view";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 112);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 18);
            this.label6.TabIndex = 23;
            this.label6.Text = "Order Thread";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "Monitoring Thread";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 34);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "GetData Thread";
            // 
            // OrderTextBox
            // 
            this.OrderTextBox.Location = new System.Drawing.Point(204, 108);
            this.OrderTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OrderTextBox.Name = "OrderTextBox";
            this.OrderTextBox.Size = new System.Drawing.Size(141, 28);
            this.OrderTextBox.TabIndex = 20;
            // 
            // MonitoringTextBox
            // 
            this.MonitoringTextBox.Location = new System.Drawing.Point(204, 68);
            this.MonitoringTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MonitoringTextBox.Name = "MonitoringTextBox";
            this.MonitoringTextBox.Size = new System.Drawing.Size(141, 28);
            this.MonitoringTextBox.TabIndex = 19;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // ParamsBtn
            // 
            this.ParamsBtn.Location = new System.Drawing.Point(407, 20);
            this.ParamsBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ParamsBtn.Name = "ParamsBtn";
            this.ParamsBtn.Size = new System.Drawing.Size(107, 34);
            this.ParamsBtn.TabIndex = 20;
            this.ParamsBtn.Text = "Params";
            this.ParamsBtn.UseVisualStyleBackColor = true;
            this.ParamsBtn.Click += new System.EventHandler(this.ParamsBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(582, 509);
            this.Controls.Add(this.ParamsBtn);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.TestCheck);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.GetDeposit);
            this.Controls.Add(this.DelBtn);
            this.Controls.Add(this.DisplayBtn);
            this.Controls.Add(this.TestText);
            this.Controls.Add(this.TestBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.MarketType);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TaewooBot";
            ((System.ComponentModel.ISupportInitialize)(this.API)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox MarketType;
        private System.Windows.Forms.Button Start_Btn;
        private AxKHOpenAPILib.AxKHOpenAPI API;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.ToolStripStatusLabel CurrentTime;
        private System.Windows.Forms.Button TestBtn;
        private System.Windows.Forms.TextBox TestText;
        private System.Windows.Forms.Button DisplayBtn;
        private System.Windows.Forms.Button DelBtn;
        private System.Windows.Forms.Button GetDeposit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TotalPnLTextBox;
        private System.Windows.Forms.TextBox DepositTextBox;
        private System.Windows.Forms.TextBox AccountTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox TestCheck;
        private System.Windows.Forms.TextBox GetDataTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OrderTextBox;
        private System.Windows.Forms.TextBox MonitoringTextBox;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Button ParamsBtn;
        private System.Windows.Forms.TextBox signalCnt;
        private System.Windows.Forms.Label version;
    }
}

