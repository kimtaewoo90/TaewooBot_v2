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

        public Blotter()
        {
            InitializeComponent();

        }


        // TODO : 신규주문(접수) 일 때 말고 정정 및 취소 일때 Action 추가
        public void DisplayBLT(List<string> bltData)
        {
            if (bltData[3] == "접수")
            {
                if (BltDataGrid.InvokeRequired)
                {
                    BltDataGrid.Invoke(new MethodInvoker(delegate ()
                    {                     
                        BltDataGrid.Rows.Add(bltData);
                    }));
                }
                else
                {
                    BltDataGrid.Rows.Add(bltData);
                }
            }
            else
            {

            }
        }
    }
}
