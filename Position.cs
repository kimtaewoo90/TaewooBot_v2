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
                    TodayDataGrid.Rows.Add(todayPnL, todayChange, todayDeposit);
                }));
            }
            else
            {
                TodayDataGrid.Rows.Add(todayPnL, todayChange, todayDeposit);
            }
        }
    

        public void DisplayPosition(string ShortCode, string KrName, string BalanceQty, string BuyPrice, string CurPrice, string Change, string TradingPnL)
        {
            if(PositionDataGrid.InvokeRequired)
            {
                PositionDataGrid.Invoke(new MethodInvoker(delegate ()
                {
                    PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
                }));
            }
            else
            {
                PositionDataGrid.Rows.Add(ShortCode, KrName, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL);
            }
        }
    }
}
