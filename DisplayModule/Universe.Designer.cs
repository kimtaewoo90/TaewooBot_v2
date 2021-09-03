namespace TaewooBot_v2
{
    partial class Universe
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
            this.TargetStocks = new System.Windows.Forms.DataGridView();
            this.StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockKrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockChange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TickSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.universe_highPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Universe_TickAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Universe_BeforeAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Universe_TotalVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.TargetStocks)).BeginInit();
            this.SuspendLayout();
            // 
            // TargetStocks
            // 
            this.TargetStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TargetStocks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StockCode,
            this.StockKrName,
            this.Price,
            this.StockChange,
            this.TickSpeed,
            this.universe_highPrice,
            this.Universe_TickAvg,
            this.Universe_BeforeAvg,
            this.Universe_TotalVolume});
            this.TargetStocks.Location = new System.Drawing.Point(12, 12);
            this.TargetStocks.Name = "TargetStocks";
            this.TargetStocks.RowTemplate.Height = 23;
            this.TargetStocks.Size = new System.Drawing.Size(946, 359);
            this.TargetStocks.TabIndex = 0;
            // 
            // StockCode
            // 
            this.StockCode.HeaderText = "종목코드";
            this.StockCode.Name = "StockCode";
            // 
            // StockKrName
            // 
            this.StockKrName.HeaderText = "종목명";
            this.StockKrName.Name = "StockKrName";
            // 
            // Price
            // 
            this.Price.HeaderText = "현재가";
            this.Price.Name = "Price";
            // 
            // StockChange
            // 
            this.StockChange.HeaderText = "Change(%)";
            this.StockChange.Name = "StockChange";
            // 
            // TickSpeed
            // 
            this.TickSpeed.HeaderText = "틱속도";
            this.TickSpeed.Name = "TickSpeed";
            // 
            // universe_highPrice
            // 
            this.universe_highPrice.HeaderText = "고가";
            this.universe_highPrice.Name = "universe_highPrice";
            // 
            // Universe_TickAvg
            // 
            this.Universe_TickAvg.HeaderText = "TickAvg";
            this.Universe_TickAvg.Name = "Universe_TickAvg";
            // 
            // Universe_BeforeAvg
            // 
            this.Universe_BeforeAvg.HeaderText = "BeforeAvg";
            this.Universe_BeforeAvg.Name = "Universe_BeforeAvg";
            // 
            // Universe_TotalVolume
            // 
            this.Universe_TotalVolume.HeaderText = "거래대금";
            this.Universe_TotalVolume.Name = "Universe_TotalVolume";
            // 
            // Universe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 377);
            this.Controls.Add(this.TargetStocks);
            this.Name = "Universe";
            this.Text = "Universe";
            ((System.ComponentModel.ISupportInitialize)(this.TargetStocks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView TargetStocks;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockKrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn TickSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn universe_highPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Universe_TickAvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Universe_BeforeAvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Universe_TotalVolume;
    }
}