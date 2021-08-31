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
                }));
            }
            else
            {
                TodayDataGrid.Rows[0].Cells[0].Value = accountList[0];
                TodayDataGrid.Rows[0].Cells[1].Value = accountList[1];
                TodayDataGrid.Rows[0].Cells[2].Value = accountList[2];
            }
        }

        public void DisplayPosition(string shortCode, string krName, string balanceQty, string buyPrice, string curPrice, string change, string tradingPnL)
        {


            var dataGridCnt = PositionDataGrid.Rows.Count - 1;

            if (PositionDataGrid.InvokeRequired)
            {

                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {


                    for (int cnt = 0; cnt < dataGridCnt; cnt++)
                    {
                        if (PositionDataGrid["Position_StockCode", cnt].Value.ToString() == shortCode)
                        {
                            PositionDataGrid["Position_StockCode", cnt].Value = shortCode;
                            PositionDataGrid["Position_KrName", cnt].Value = krName;
                            PositionDataGrid["BalanceQty", cnt].Value = String.Format("{0:0,0}",double.Parse(balanceQty));
                            PositionDataGrid["BuyPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(buyPrice));
                            PositionDataGrid["CurPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(curPrice));
                            PositionDataGrid["Change", cnt].Value = String.Format("{0:0,0}", double.Parse(change));
                            PositionDataGrid["TradingPnL", cnt].Value = String.Format("{0:0,0}", double.Parse(tradingPnL));
                            break;
                        }     
                    }
                    PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                             String.Format("{0:0,0}", double.Parse(buyPrice)),
                                             String.Format("{0:0,0}", double.Parse(curPrice)),
                                             String.Format("{0:0,0}", double.Parse(change)),
                                             String.Format("{0:0,0}", double.Parse(tradingPnL)));

                }));
            }

            else
            {
                for (int cnt = 0; cnt < dataGridCnt; cnt++)
                {
                    if (PositionDataGrid["Position_StockCode", cnt].Value.ToString() == shortCode)
                    {
                        PositionDataGrid["Position_StockCode", cnt].Value = shortCode;
                        PositionDataGrid["Position_KrName", cnt].Value = krName;
                        PositionDataGrid["BalanceQty", cnt].Value = String.Format("{0:0,0}", double.Parse(balanceQty));
                        PositionDataGrid["BuyPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(buyPrice));
                        PositionDataGrid["CurPrice", cnt].Value = String.Format("{0:0,0}", double.Parse(curPrice));
                        PositionDataGrid["Change", cnt].Value = String.Format("{0:0,0}", double.Parse(change));
                        PositionDataGrid["TradingPnL", cnt].Value = String.Format("{0:0,0}", double.Parse(tradingPnL));
                        break;
                    }
                }
                PositionDataGrid.Rows.Add(shortCode, krName, String.Format("{0:0,0}", double.Parse(balanceQty)),
                                         String.Format("{0:0,0}", double.Parse(buyPrice)),
                                         String.Format("{0:0,0}", double.Parse(curPrice)),
                                         String.Format("{0:0,0}", double.Parse(change)),
                                         String.Format("{0:0,0}", double.Parse(tradingPnL)));
            }
        }
    }
}
