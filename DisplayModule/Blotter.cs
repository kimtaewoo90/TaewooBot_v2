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
    public partial class Blotter : Form
    {

        public TelegramClass telegram = new TelegramClass();
        public Utils utils = new Utils();

        public Blotter()
        {
            InitializeComponent();
        }


        // TODO : 신규주문(접수) 일 때 말고 정정 및 취소 일때 Action 추가
        public void DisplayBLT(List<string> bltData)
        {
            if (BltDataGrid.InvokeRequired)
            {
                BltDataGrid.Invoke(new MethodInvoker(delegate ()
                {

                    BltDataGrid.Rows.Add(bltData[0], bltData[1], bltData[2], bltData[3], bltData[4], bltData[5], bltData[6], bltData[7], bltData[8], bltData[9]);
                }));
            }
            else
            {
                BltDataGrid.Rows.Add(bltData[0], bltData[1], bltData[2], bltData[3], bltData[4], bltData[5], bltData[6], bltData[7], bltData[8], bltData[9]);
            }
        }
        
    }



}

