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

        public void DisplayPosition(List<string> positionList)
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
                            if (PositionDataGrid["StockCode", i].Value.ToString() == positionList[0])
                            {
                                PositionDataGrid.Rows[i].Cells[0].Value = positionList[0];
                                PositionDataGrid.Rows[i].Cells[1].Value = positionList[1];
                                PositionDataGrid.Rows[i].Cells[2].Value = positionList[2];
                                PositionDataGrid.Rows[i].Cells[3].Value = positionList[3];
                                PositionDataGrid.Rows[i].Cells[4].Value = positionList[4];
                                PositionDataGrid.Rows[i].Cells[5].Value = positionList[5] + "%";
                                PositionDataGrid.Rows[i].Cells[6].Value = positionList[6];
                                break;
                            }
                            else
                            {
                                PositionDataGrid.Rows.Add(positionList);
                            }
                        }
                    }
                    else
                    {
                        PositionDataGrid.Rows.Add(positionList);
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
                        if (PositionDataGrid["StockCode", i].Value.ToString() == positionList[0])
                        {
                            PositionDataGrid.Rows[i].Cells[0].Value = positionList[0];
                            PositionDataGrid.Rows[i].Cells[1].Value = positionList[1];
                            PositionDataGrid.Rows[i].Cells[2].Value = positionList[2];
                            PositionDataGrid.Rows[i].Cells[3].Value = positionList[3];
                            PositionDataGrid.Rows[i].Cells[4].Value = positionList[4];
                            PositionDataGrid.Rows[i].Cells[5].Value = positionList[5] + "%";
                            PositionDataGrid.Rows[i].Cells[6].Value = positionList[6];
                            break;
                        }
                        else
                        {
                            PositionDataGrid.Rows.Add(positionList);
                        }
                    }
                }
                else
                {
                    PositionDataGrid.Rows.Add(positionList);
                }
            }
        }
    }
}
