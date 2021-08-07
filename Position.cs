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

        public void DisplayAccount(string todayPnL, string todayChange, string todayDeposit)
        {
            if (TodayDataGrid.InvokeRequired)
            {
                TodayDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    TodayDataGrid.Rows[0].Cells[0].Value = todayPnL;
                    TodayDataGrid.Rows[0].Cells[1].Value = todayChange;
                    TodayDataGrid.Rows[0].Cells[2].Value = todayDeposit;
                }));
            }
            else
            {
                TodayDataGrid.Rows[0].Cells[0].Value = todayPnL;
                TodayDataGrid.Rows[0].Cells[1].Value = todayChange;
                TodayDataGrid.Rows[0].Cells[2].Value = todayDeposit;
            }
        }

        public void DisplayPosition(string ShortCode, string KrName, string BalanceQty, string BuyPrice, string CurPrice, string Change, string TradingPnL)
        {


            if(PositionDataGrid.InvokeRequired)
            {
                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {

                    int UpdateCnt = PositionDataGrid.Rows.Count;

                    if (UpdateCnt > 0)
                    {
                        for (int i = 0; i < UpdateCnt - 1; i++)
                        {
                            if (PositionDataGrid["StockCode", i].Value.ToString() == ShortCode)
                            {
                                PositionDataGrid.Rows[i].Cells[0].Value = ShortCode;
                                PositionDataGrid.Rows[i].Cells[1].Value = KrName;
                                PositionDataGrid.Rows[i].Cells[2].Value = BalanceQty;
                                PositionDataGrid.Rows[i].Cells[3].Value = BuyPrice;
                                PositionDataGrid.Rows[i].Cells[4].Value = CurPrice;
                                PositionDataGrid.Rows[i].Cells[5].Value = Change + "%";
                                PositionDataGrid.Rows[i].Cells[6].Value = TradingPnL;
                                break;
                            }
                            else
                            {
                                PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
                            }
                        }
                    }
                    else
                    {
                        PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
                    }
                }));
            }
            else
            {
                int UpdateCnt = PositionDataGrid.Rows.Count;

                if (UpdateCnt > 0)
                {
                    for (int i = 0; i < UpdateCnt - 1; i++)
                    {
                        if (PositionDataGrid["StockCode", i].Value.ToString() == ShortCode)
                        {
                            PositionDataGrid.Rows[i].Cells[0].Value = ShortCode;
                            PositionDataGrid.Rows[i].Cells[1].Value = KrName;
                            PositionDataGrid.Rows[i].Cells[2].Value = BalanceQty;
                            PositionDataGrid.Rows[i].Cells[3].Value = BuyPrice;
                            PositionDataGrid.Rows[i].Cells[4].Value = CurPrice;
                            PositionDataGrid.Rows[i].Cells[5].Value = Change + "%";
                            PositionDataGrid.Rows[i].Cells[6].Value = TradingPnL;
                            break;
                        }
                        else
                        {
                            PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
                        }
                    }
                }
                else
                {
                    PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
                }
            }
        }
    }
}
