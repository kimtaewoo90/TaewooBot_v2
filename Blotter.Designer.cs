namespace TaewooBot_v2
{
    partial class Blotter
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
            this.BltDataGrid = new System.Windows.Forms.DataGridView();
            this.OrderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilledQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilledPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BltDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BltDataGrid
            // 
            this.BltDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BltDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderTime,
            this.StockCode,
            this.KrName,
            this.OrderType,
            this.Type,
            this.OrdQty,
            this.FilledQty,
            this.OrdPrice,
            this.FilledPrice});
            this.BltDataGrid.Location = new System.Drawing.Point(13, 13);
            this.BltDataGrid.Name = "BltDataGrid";
            this.BltDataGrid.RowTemplate.Height = 23;
            this.BltDataGrid.Size = new System.Drawing.Size(947, 370);
            this.BltDataGrid.TabIndex = 0;
            // 
            // OrderTime
            // 
            this.OrderTime.HeaderText = "OrderTime";
            this.OrderTime.Name = "OrderTime";
            // 
            // StockCode
            // 
            this.StockCode.HeaderText = "ShortCode";
            this.StockCode.Name = "StockCode";
            // 
            // KrName
            // 
            this.KrName.HeaderText = "KrName";
            this.KrName.Name = "KrName";
            // 
            // OrderType
            // 
            this.OrderType.HeaderText = "OrdType";
            this.OrderType.Name = "OrderType";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // OrdQty
            // 
            this.OrdQty.HeaderText = "OrdQty";
            this.OrdQty.Name = "OrdQty";
            // 
            // FilledQty
            // 
            this.FilledQty.HeaderText = "FilledQty";
            this.FilledQty.Name = "FilledQty";
            // 
            // OrdPrice
            // 
            this.OrdPrice.HeaderText = "OrdPrice";
            this.OrdPrice.Name = "OrdPrice";
            // 
            // FilledPrice
            // 
            this.FilledPrice.HeaderText = "FilledPrice";
            this.FilledPrice.Name = "FilledPrice";
            // 
            // Blotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 389);
            this.Controls.Add(this.BltDataGrid);
            this.Name = "Blotter";
            this.Text = "Blotter";
            ((System.ComponentModel.ISupportInitialize)(this.BltDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BltDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn KrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilledQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilledPrice;

        private System.Windows.Forms.DataGridViewTextBoxColumn OrderTime;
    }
}