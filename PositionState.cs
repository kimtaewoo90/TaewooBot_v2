using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{

    class PositionState
    {
        // ShortCode1, KrName1, BalanceQty, BuyPrice, CurPrice, Change, TradingPnL.ToString()

        //Position position = new Position();
        Utils utils = new Utils();
        BlotterClass blt = new BlotterClass();

        string position_ShortCode { get; set; }
        string position_KrName { get; set; }
        string position_BalanceQty { get; set; }
        string position_BuyPrice { get; set; }
        string position_CurPrice { get; set; }
        string position_Change { get; set; }
        string position_TradingPnL { get; set; }

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



        public void SendSellOrder()
        {
            BotParams.RqName = "주식주문";
            var scr_no = utils.get_scr_no();
            var ShortCode = position_ShortCode;
            var curPrice = double.Parse(position_CurPrice) ;
            int ordQty = int.Parse(position_BalanceQty);
            var ordPrice = 0;
            var hogaGb = "03";

            blt.SendSellOrder(scr_no, ShortCode, curPrice, ordQty, ordPrice, hogaGb);

            // 매도 되면 PositionDataGrid 에서 삭제하는게 좋을까 BalanceQty를 0으로 유지하는게 좋을까

        }
    }
}
