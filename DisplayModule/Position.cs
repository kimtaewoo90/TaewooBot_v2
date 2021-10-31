using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaewooBot_v2
{
    public partial class Position : Form
    {
        public Position()
        {
            InitializeComponent();
        }


        public void DisplayAccount(List<string> accountList)
        {
            if (TodayDataGrid.InvokeRequired)
            {
                TodayDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    TodayDataGrid.Rows[0].Cells[0].Value = accountList[0];
                    TodayDataGrid.Rows[0].Cells[1].Value = accountList[1];
                    TodayDataGrid.Rows[0].Cells[2].Value = accountList[2];
                    TodayDataGrid.Rows[0].Cells[3].Value = accountList[3];

                }));
            }
            else
            {
                TodayDataGrid.Rows[0].Cells[0].Value = accountList[0];
                TodayDataGrid.Rows[0].Cells[1].Value = accountList[1];
                TodayDataGrid.Rows[0].Cells[2].Value = accountList[2];
                TodayDataGrid.Rows[0].Cells[3].Value = accountList[3];

            }
        }

        public void DisplayPosition(string shortCode, string krName, string balanceQty, string buyPrice, string curPrice, string change, string tradingPnL)
        {


            var dataGridCnt = PositionDataGrid.Rows.Count - 1;

            var positionPnL = 0.0;

            if (PositionDataGrid.InvokeRequired)
            {

                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    for(int row = 0; row < dataGridCnt; row++)
                    {
                        positionPnL = positionPnL + Convert.ToDouble(PositionDataGrid.Rows[row].Cells[6].Value);
                    }

                    BotParams.positionPnL = positionPnL;

                    profitTextBox.Text = BotParams.profitTimes.ToString();
                    losscutTextBox.Text = BotParams.losscutTimes.ToString();

                    for (int cnt = 0; cnt < dataGridCnt; cnt++)
                    {
                        if (PositionDataGrid["Position_StockCode", cnt].Value.ToString() == shortCode)
                        {
                            PositionDataGrid["Position_StockCode", cnt].Value = shortCode;
                            PositionDataGrid["Position_KrName", cnt].Value = krName;
                            PositionDataGrid["BalanceQty", cnt].Value = String.Format("{0:0,0}",double.Parse(balanceQty));
                            PositionDataGrid["BuyPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(buyPrice));
                            PositionDataGrid["CurPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(curPrice));
                            PositionDataGrid["Change", cnt].Value = String.Format("{0:f2}%", double.Parse(change));
                            PositionDataGrid["TradingPnL", cnt].Value = String.Format("{0:0,0}", double.Parse(tradingPnL));

                            return;
                        }     
                    }
                    PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                             String.Format("{0:0,0}", double.Parse(buyPrice)),
                                             String.Format("{0:0,0}", double.Parse(curPrice)),
                                             String.Format("{0:f2}%", double.Parse(change)),
                                             String.Format("{0:0,0}", double.Parse(tradingPnL)));

                }));
            }

            else
            {
                for (int row = 0; row < dataGridCnt; row++)
                {
                    positionPnL = positionPnL + Convert.ToDouble(PositionDataGrid.Rows[row].Cells[6].Value);
                }

                BotParams.positionPnL = positionPnL;

                profitTextBox.Text = BotParams.profitTimes.ToString();
                losscutTextBox.Text = BotParams.losscutTimes.ToString();

                for (int cnt = 0; cnt < dataGridCnt; cnt++)
                {
                    if (PositionDataGrid["Position_StockCode", cnt].Value.ToString() == shortCode)
                    {
                        PositionDataGrid["Position_StockCode", cnt].Value = shortCode;
                        PositionDataGrid["Position_KrName", cnt].Value = krName;
                        PositionDataGrid["BalanceQty", cnt].Value = String.Format("{0:0,0}", double.Parse(balanceQty));
                        PositionDataGrid["BuyPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(buyPrice));
                        PositionDataGrid["CurPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(curPrice));
                        PositionDataGrid["Change", cnt].Value = String.Format("{0:f2}%", double.Parse(change));
                        PositionDataGrid["TradingPnL", cnt].Value = String.Format("{0:0,0}", double.Parse(tradingPnL));
                        return;
                    }
                }
                PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                         String.Format("{0:0,0}", double.Parse(buyPrice)),
                                         String.Format("{0:0,0}", double.Parse(curPrice)),
                                         String.Format("{0:f2}%", double.Parse(change)),
                                         String.Format("{0:0,0}", double.Parse(tradingPnL)));
            }
        }

        public void DisplayPositionOnce()
        {

            if (PositionDataGrid.InvokeRequired)
            {

                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    if (PositionDataGrid.Rows.Count > 0)
                    {
                        for (int i = 0; i < PositionDataGrid.Rows.Count; i++)
                        {
                            //PositionDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            if (!PositionDataGrid.Rows[i].IsNewRow)
                                PositionDataGrid.Rows.Remove(PositionDataGrid.Rows[i]);
                        }
                    }

                    foreach (KeyValuePair<string, PositionState> items in BotParams.Accnt_Position)
                    {
                        var shortCode = items.Key;
                        var krName = items.Value.position_KrName;
                        var balanceQty = items.Value.position_BalanceQty;
                        var buyPrice = items.Value.position_BuyPrice;
                        var curPrice = items.Value.position_CurPrice;
                        var change = items.Value.position_Change;
                        var tradingPnL = items.Value.position_TradingPnL;

                        PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                                                     String.Format("{0:0,0}", double.Parse(buyPrice)),
                                                                     String.Format("{0:0,0}", double.Parse(curPrice)),
                                                                     String.Format("{0:f2}%", double.Parse(change)),
                                                                     String.Format("{0:0,0}", double.Parse(tradingPnL)));
                    }

                    var positionPnL = 0.0;

                    for (int row = 0; row < BotParams.Accnt_Position.Count; row++)
                    {
                        positionPnL = positionPnL + Convert.ToDouble(PositionDataGrid.Rows[row].Cells[6].Value);
                    }
                    BotParams.positionPnL = positionPnL;
                }));
            }
            else
            {
                if (PositionDataGrid.Rows.Count > 0)
                { 
                    for (int i = 0; i < PositionDataGrid.Rows.Count; i++)
                    {
                        //PositionDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        if (!PositionDataGrid.Rows[i].IsNewRow)
                            PositionDataGrid.Rows.Remove(PositionDataGrid.Rows[i]);
                    }
                }

                foreach (KeyValuePair<string, PositionState> items in BotParams.Accnt_Position)
                {
                    var shortCode = items.Key;
                    var krName = items.Value.position_KrName;
                    var balanceQty = items.Value.position_BalanceQty;
                    var buyPrice = items.Value.position_BuyPrice;
                    var curPrice = items.Value.position_CurPrice;
                    var change = items.Value.position_Change;
                    var tradingPnL = items.Value.position_TradingPnL;

                    PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                                                 String.Format("{0:0,0}", double.Parse(buyPrice)),
                                                                 String.Format("{0:0,0}", double.Parse(curPrice)),
                                                                 String.Format("{0:f2}%", double.Parse(change)),
                                                                 String.Format("{0:0,0}", double.Parse(tradingPnL)));
                }

                var positionPnL = 0.0;

                for (int row = 0; row < BotParams.Accnt_Position.Count; row++)
                {
                    positionPnL = positionPnL + Convert.ToDouble(PositionDataGrid.Rows[row].Cells[6].Value);
                }
                BotParams.positionPnL = positionPnL;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
