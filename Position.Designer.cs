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
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // PositionDataGrid
            // 
            this.PositionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PositionDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Order_StockCode,
            this.Order_StockName,
            this.BalanceQty,
            this.BuyPrice,
            this.CurPrice,
            this.Change,
            this.TradingPnL});
            this.PositionDataGrid.Location = new System.Drawing.Point(10, 12);
            this.PositionDataGrid.Name = "PositionDataGrid";
            this.PositionDataGrid.RowTemplate.Height = 23;
            this.PositionDataGrid.Size = new System.Drawing.Size(753, 213);
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
            // Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 237);
            this.Controls.Add(this.PositionDataGrid);
            this.Name = "Position";
            this.Text = "Position";
            ((System.ComponentModel.ISupportInitialize)(this.PositionDataGrid)).EndInit();
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
        private AxKHOpenAPILib.AxKHOpenAPI Position_API;

    }
}