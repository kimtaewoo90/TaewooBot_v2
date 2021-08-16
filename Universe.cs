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
    public partial class Universe : Form
    {
        Logs logs = new Logs();

        public Universe()
        {
            InitializeComponent();
        }


        public void DisplayTargetStocks(string Type, string StockCode, string StockName, string Price, string Change, string TickSpeed)
        {
            if (Type == "Insert")
            {
                int AddCnt = TargetStocks.Rows.Count;
                // StockCode, StockKrName, Price, TickSpeed, PnL

                TargetStocks.Rows.Add(StockCode, StockName, Price, TickSpeed, Change);


                logs.write_sys_log(StockCode + "종목이 추가 되었습니다.", 0);
            }

            else if (Type == "Update")
            {
                int UpdateCnt = TargetStocks.Rows.Count;
                for (int i = 0; i < UpdateCnt - 1; i++)
                {
                    if (TargetStocks["StockCode", i].Value.ToString() == StockCode)
                    {
                        TargetStocks.Rows[i].Cells[2].Value = Price;
                        TargetStocks.Rows[i].Cells[3].Value = Change;
                        TargetStocks.Rows[i].Cells[4].Value = TickSpeed;

                    }
                }
            }
        }

        public void DeleteTargetStocks(string StockCode)
        {
            try
            {
                int DelCnt = TargetStocks.Rows.Count;
                for (int i = 0; i < DelCnt; i++)
                {
                    try
                    {
                        if (TargetStocks["StockCode", i].Value.ToString() == StockCode && TargetStocks != null)
                        {
                            // 해당 데이터 삭제
                            TargetStocks.Rows.Remove(TargetStocks.Rows[i]);

                            logs.write_sys_log(StockCode + "종목이 삭제 되었습니다.", 0);
                            break;

                        }
                    }
                    catch (Exception err)
                    {
                        logs.write_sys_log(err.ToString(), 0);
                    }

                }
            }

            catch (Exception err)
            {
                logs.write_sys_log(err.ToString(), 0);
            }

        }
    }
}
