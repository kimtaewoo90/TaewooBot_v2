using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
 
    public partial class Main
    {
        private void TestBtn_Click(object sender, EventArgs e)
        {
            //ReqRealData(TestText.Text, "1111");
        }

        private void DisplayBtn_Click(object sender, EventArgs e)
        {
            string code = TestText.Text;

            universe.DisplayTargetStocks("Insert", code, GetKrName(code), botParams.StockPriceDict[TestText.Text], "0", "0");
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            string code = TestText.Text;
            universe.DeleteTargetStocks(code);
        }

        private void GetDeposit_Click(object sender, EventArgs e)
        {
            GetAccountInformation();
        }
    }
}
