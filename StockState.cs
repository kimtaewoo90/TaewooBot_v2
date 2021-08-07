using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    class StockState
    {
        Logs logs = new Logs();
        Params Params = new Params();
        Universe universe = new Universe();
        Position position = new Position();
        Blotter blt = new Blotter();
        Utils utils = new Utils();
        BotParams botParams = new BotParams();

        private string states_ShortCode { get; set; }
        private string states_KrName { get; set; }
        private string states_CurPrice { get; set; }
        private string states_highPrice { get; set; }
        private string states_Change { get; set; }
        private string states_ContractLots { get; set; }
        private DateTime states_UpdateTime { get; set; }

        private List<int> contractLotsList { get; set; }

        public StockState(string ShortCode, string KrName, string curPrice, string highPrice, string change, string contactLots, DateTime updateTime)
        {
            states_ShortCode = ShortCode;
            states_KrName = KrName;
            states_CurPrice = curPrice;
            states_highPrice = highPrice;
            states_Change = change;
            states_ContractLots = contactLots;
            states_UpdateTime = updateTime;
        }

        // TODO : 시그널 확인 함수
        public bool MonitoringSignals()
        {
            bool avgSpeed = false;

            return avgSpeed;
        }

        // TODO : 매수주문
        public void SendBuyOrder()
        {
            botParams.RqName = "주식주문";
            var scr_no = utils.get_scr_no();
            var ShortCode = states_ShortCode;
            var curPrice = states_CurPrice;
            int ordQty = Int32.Parse(Math.Truncate(1000000.0 / double.Parse(curPrice)).ToString());
            var ordPrice = 0;
            var hogaGb = "03";

            // TODO : 파라미터 다시 확인
            blt.SendBuyOrder(botParams.RqName, scr_no, ShortCode, curPrice, ordQty, ordPrice, hogaGb);
        }
    }
}
