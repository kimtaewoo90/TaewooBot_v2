using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{

    public class PositionState
    {
        // ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString()

        Utils utils = new Utils();
     

        public string position_ShortCode { get; set; }
        public string position_KrName { get; set; }
        public string position_BalanceQty { get; set; }
        public string position_BuyPrice { get; set; }
        public string position_CurPrice { get; set; }
        public string position_Change { get; set; }
        public string position_TradingPnL { get; set; }

        public PositionState(string ShortCode, string KrName, string BalanceQty, string BuyPrice, string CurPrice, string Change, string TradingPnL)
        {
            position_ShortCode = ShortCode;
            position_KrName = KrName;
            position_BalanceQty = BalanceQty;
            position_BuyPrice = BuyPrice;
            position_CurPrice = CurPrice;
            position_Change = Change;
            position_TradingPnL = TradingPnL;
        }
    }
}
