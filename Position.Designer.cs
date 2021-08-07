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
            this.Order_StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Order_StockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradingPnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayDataGrid = new System.Windows.Forms.DataGridView();
            this.TodayPnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayPnLPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TodayDeposit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodayDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // PositionDataGrid
            // 
            this.PositionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PositionDataGrid.CurrentCell = null;
            this.PositionDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Order_StockCode,
            this.Order_StockName,
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
            // Order_StockCode
            // 
            this.Order_StockCode.HeaderText = "종목코드";
            this.Order_StockCode.Name = "Order_StockCode";
            // 
            // Order_StockName
            // 
            this.Order_StockName.HeaderText = "종목명";
            this.Order_StockName.Name = "Order_StockName";
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
            this.TodayPnLPercent,
            this.TodayDeposit});
            this.TodayDataGrid.Location = new System.Drawing.Point(12, 12);
            this.TodayDataGrid.Name = "TodayDataGrid";
            this.TodayDataGrid.RowTemplate.Height = 23;
            this.TodayDataGrid.Size = new System.Drawing.Size(305, 69);
            this.TodayDataGrid.TabIndex = 2;
            // 
            // TodayPnL
            // 
            this.TodayPnL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TodayPnL.HeaderText = "당일실현손익";
            this.TodayPnL.Name = "TodayPnL";
            this.TodayPnL.Width = 72;
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
            // Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 390);
            this.Controls.Add(this.TodayDataGrid);
            this.Controls.Add(this.PositionDataGrid);
            this.Name = "Position";
            this.Text = "Position";
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TodayDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order_StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Change;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradingPnL;
        public System.Windows.Forms.DataGridView PositionDataGrid;
        //private AxKHOpenAPILib.AxKHOpenAPI Position_API;
        private System.Windows.Forms.DataGridView TodayDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayPnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayPnLPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn TodayDeposit;
    }
}