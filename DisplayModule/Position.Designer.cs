namespace TaewooBot_v2
{
    partial class Position
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PositionDataGrid = new System.Windows.Forms.DataGridView();
            this.Position_StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position_KrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradingPnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayDataGrid = new System.Windows.Forms.DataGridView();
            this.TodayPnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositionPnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayPnLPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.profitTextBox = new System.Windows.Forms.TextBox();
            this.losscutTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodayDataGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PositionDataGrid
            // 
            this.PositionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PositionDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Position_StockCode,
            this.Position_KrName,
            this.BalanceQty,
            this.BuyPrice,
            this.CurPrice,
            this.Change,
            this.TradingPnL});
            this.PositionDataGrid.Location = new System.Drawing.Point(12, 87);
            this.PositionDataGrid.Name = "PositionDataGrid";
            this.PositionDataGrid.RowTemplate.Height = 23;
            this.PositionDataGrid.Size = new System.Drawing.Size(753, 291);
            this.PositionDataGrid.TabIndex = 1;
            // 
            // Position_StockCode
            // 
            this.Position_StockCode.HeaderText = "종목코드";
            this.Position_StockCode.Name = "Position_StockCode";
            // 
            // Position_KrName
            // 
            this.Position_KrName.HeaderText = "종목명";
            this.Position_KrName.Name = "Position_KrName";
            // 
            // BalanceQty
            // 
            this.BalanceQty.HeaderText = "BalanceQuantity";
            this.BalanceQty.Name = "BalanceQty";
            // 
            // BuyPrice
            // 
            this.BuyPrice.HeaderText = "BuyPrice";
            this.BuyPrice.Name = "BuyPrice";
            // 
            // CurPrice
            // 
            this.CurPrice.HeaderText = "현재가";
            this.CurPrice.Name = "CurPrice";
            // 
            // Change
            // 
            this.Change.HeaderText = "Change(%)";
            this.Change.Name = "Change";
            // 
            // TradingPnL
            // 
            this.TradingPnL.HeaderText = "TradingPnL";
            this.TradingPnL.Name = "TradingPnL";
            // 
            // TodayDataGrid
            // 
            this.TodayDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TodayDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TodayPnL,
            this.PositionPnL,
            this.TodayPnLPercent,
            this.TodayDeposit});
            this.TodayDataGrid.Location = new System.Drawing.Point(12, 12);
            this.TodayDataGrid.Name = "TodayDataGrid";
            this.TodayDataGrid.RowTemplate.Height = 23;
            this.TodayDataGrid.Size = new System.Drawing.Size(399, 59);
            this.TodayDataGrid.TabIndex = 2;
            // 
            // TodayPnL
            // 
            this.TodayPnL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TodayPnL.HeaderText = "당일실현손익";
            this.TodayPnL.Name = "TodayPnL";
            this.TodayPnL.Width = 72;
            // 
            // PositionPnL
            // 
            this.PositionPnL.HeaderText = "포지션손익";
            this.PositionPnL.Name = "PositionPnL";
            // 
            // TodayPnLPercent
            // 
            this.TodayPnLPercent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TodayPnLPercent.HeaderText = "당일실현손익률";
            this.TodayPnLPercent.Name = "TodayPnLPercent";
            this.TodayPnLPercent.Width = 83;
            // 
            // TodayDeposit
            // 
            this.TodayDeposit.HeaderText = "예수금";
            this.TodayDeposit.Name = "TodayDeposit";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.losscutTextBox);
            this.groupBox1.Controls.Add(this.profitTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(430, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 59);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "익절/손절";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "익절";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "손절";
            // 
            // profitTextBox
            // 
            this.profitTextBox.Location = new System.Drawing.Point(50, 20);
            this.profitTextBox.Name = "profitTextBox";
            this.profitTextBox.Size = new System.Drawing.Size(100, 21);
            this.profitTextBox.TabIndex = 2;
            // 
            // losscutTextBox
            // 
            this.losscutTextBox.Location = new System.Drawing.Point(202, 20);
            this.losscutTextBox.Name = "losscutTextBox";
            this.losscutTextBox.Size = new System.Drawing.Size(100, 21);
            this.losscutTextBox.TabIndex = 3;
            // 
            // Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 390);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TodayDataGrid);
            this.Controls.Add(this.PositionDataGrid);
            this.Name = "Position";
            this.Text = "Position";
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodayDataGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView PositionDataGrid;
        //private AxKHOpenAPILib.AxKHOpenAPI Position_API;
        private System.Windows.Forms.DataGridView TodayDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position_StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position_KrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Change;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradingPnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayPnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositionPnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayPnLPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayDeposit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox losscutTextBox;
        private System.Windows.Forms.TextBox profitTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}