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
            this.MarketType.Location = new System.Drawing.Point(9, 12);
            this.MarketType.Name = "MarketType";
            this.MarketType.Size = new System.Drawing.Size(121, 20);
            this.MarketType.TabIndex = 3;
            this.MarketType.SelectedIndexChanged += new System.EventHandler(this.MarketType_SelectedIndexChanged);
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(148, 9);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 4;
            this.Start_Btn.Text = "Start";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // API
            // 
            this.API.Enabled = true;
            this.API.Location = new System.Drawing.Point(289, 85);
            this.API.Name = "API";
            this.API.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("API.OcxState")));
            this.API.Size = new System.Drawing.Size(97, 48);
            this.API.TabIndex = 7;
            this.API.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status,
            this.CurrentTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 326);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(406, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "time";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(135, 17);
            this.Status.Text = "Welcome to TaewooBot";
            // 
            // CurrentTime
            // 
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(54, 17);
            this.CurrentTime.Text = "time line";
            // 
            // TestBtn
            // 
            this.TestBtn.Location = new System.Drawing.Point(164, 700);
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.Size = new System.Drawing.Size(75, 23);
            this.TestBtn.TabIndex = 10;
            this.TestBtn.Text = "Test";
            this.TestBtn.UseVisualStyleBackColor = true;
            this.TestBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // TestText
            // 
            this.TestText.Location = new System.Drawing.Point(20, 700);
            this.TestText.Name = "TestText";
            this.TestText.Size = new System.Drawing.Size(138, 21);
            this.TestText.TabIndex = 11;
            // 
            // DisplayBtn
            // 
            this.DisplayBtn.Location = new System.Drawing.Point(281, 700);
            this.DisplayBtn.Name = "DisplayBtn";
            this.DisplayBtn.Size = new System.Drawing.Size(75, 23);
            this.DisplayBtn.TabIndex = 12;
            this.DisplayBtn.Text = "Display";
            this.DisplayBtn.UseVisualStyleBackColor = true;
            this.DisplayBtn.Click += new System.EventHandler(this.DisplayBtn_Click);
            // 
            // DelBtn
            // 
            this.DelBtn.Location = new System.Drawing.Point(362, 700);
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(75, 23);
            this.DelBtn.TabIndex = 13;
            this.DelBtn.Text = "Delete";
            this.DelBtn.UseVisualStyleBackColor = true;
            this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
            // 
            // GetDeposit
            // 
            this.GetDeposit.Location = new System.Drawing.Point(467, 699);
            this.GetDeposit.Name = "GetDeposit";
            this.GetDeposit.Size = new System.Drawing.Size(75, 23);
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
            this.groupBox3.Location = new System.Drawing.Point(9, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(386, 122);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "계좌현황";
            // 
            // TotalPnLTextBox
            // 
            this.TotalPnLTextBox.Location = new System.Drawing.Point(68, 83);
            this.TotalPnLTextBox.Name = "TotalPnLTextBox";
            this.TotalPnLTextBox.Size = new System.Drawing.Size(100, 21);
            this.TotalPnLTextBox.TabIndex = 5;
            // 
            // DepositTextBox
            // 
            this.DepositTextBox.Location = new System.Drawing.Point(68, 49);
            this.DepositTextBox.Name = "DepositTextBox";
            this.DepositTextBox.Size = new System.Drawing.Size(100, 21);
            this.DepositTextBox.TabIndex = 4;
            // 
            // AccountTextBox
            // 
            this.AccountTextBox.Location = new System.Drawing.Point(68, 18);
            this.AccountTextBox.Name = "AccountTextBox";
            this.AccountTextBox.Size = new System.Drawing.Size(100, 21);
            this.AccountTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "총 수익률";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "예수금";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "계좌번호";
            // 
            // signalCnt
            // 
            this.signalCnt.Location = new System.Drawing.Point(280, 45);
            this.signalCnt.Name = "signalCnt";
            this.signalCnt.Size = new System.Drawing.Size(100, 21);
            this.signalCnt.TabIndex = 6;
            // 
            // TestCheck
            // 
            this.TestCheck.AutoSize = true;
            this.TestCheck.Location = new System.Drawing.Point(230, 13);
            this.TestCheck.Name = "TestCheck";
            this.TestCheck.Size = new System.Drawing.Size(49, 16);
            this.TestCheck.TabIndex = 17;
            this.TestCheck.Text = "Test";
            this.TestCheck.UseVisualStyleBackColor = true;
            this.TestCheck.CheckedChanged += new System.EventHandler(this.TestCheck_CheckedChanged);
            // 
            // GetDataTextBox
            // 
            this.GetDataTextBox.Location = new System.Drawing.Point(143, 20);
            this.GetDataTextBox.Name = "GetDataTextBox";
            this.GetDataTextBox.Size = new System.Drawing.Size(100, 21);
            this.GetDataTextBox.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.signalCnt);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.OrderTextBox);
            this.groupBox4.Controls.Add(this.MonitoringTextBox);
            this.groupBox4.Controls.Add(this.GetDataTextBox);
            this.groupBox4.Controls.Add(this.API);
            this.groupBox4.Location = new System.Drawing.Point(9, 185);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(386, 133);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bot Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "Order Thread";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "Monitoring Thread";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "GetData Thread";
            // 
            // OrderTextBox
            // 
            this.OrderTextBox.Location = new System.Drawing.Point(143, 72);
            this.OrderTextBox.Name = "OrderTextBox";
            this.OrderTextBox.Size = new System.Drawing.Size(100, 21);
            this.OrderTextBox.TabIndex = 20;
            // 
            // MonitoringTextBox
            // 
            this.MonitoringTextBox.Location = new System.Drawing.Point(143, 45);
            this.MonitoringTextBox.Name = "MonitoringTextBox";
            this.MonitoringTextBox.Size = new System.Drawing.Size(100, 21);
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
            this.ParamsBtn.Location = new System.Drawing.Point(285, 13);
            this.ParamsBtn.Name = "ParamsBtn";
            this.ParamsBtn.Size = new System.Drawing.Size(75, 23);
            this.ParamsBtn.TabIndex = 20;
            this.ParamsBtn.Text = "Params";
            this.ParamsBtn.UseVisualStyleBackColor = true;
            this.ParamsBtn.Click += new System.EventHandler(this.ParamsBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(406, 348);
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
            this.Location = new System.Drawing.Point(1460, 520);
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
    }
}

