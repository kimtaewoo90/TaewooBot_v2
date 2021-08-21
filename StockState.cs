using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    class StockState
    {
        Utils utils = new Utils();

        private string states_ShortCode { get; set; }
        private string states_KrName { get; set; }
        private string states_CurPrice { get; set; }
        private string states_highPrice { get; set; }
        private string states_Change { get; set; }
        private string states_ContractLots { get; set; }
        private List<int> states_TickList { get; set; }
        private List<int> states_TickOneMinList { get; set; }
        private double states_BeforeAvg { get; set; }
        private DateTime states_UpdateTime { get; set; }

        private bool Signals { get; set; } = false;
        private bool signal_1 { get; set; } = false;
        private bool signal_2 { get; set; } = false;
        private bool signal_3 { get; set; } = false;


        public StockState(string ShortCode, string KrName, string curPrice, string highPrice, string change, string contactLots, List<int> tickList, List<int> tickOneList, double beforeAvg, DateTime updateTime)
        {
            states_ShortCode = ShortCode;
            states_KrName = KrName;
            states_CurPrice = curPrice;
            states_highPrice = highPrice;
            states_Change = change;
            states_ContractLots = contactLots;
            states_UpdateTime = updateTime;
            states_TickList = tickList;
            states_TickOneMinList = tickOneList;
            states_BeforeAvg = beforeAvg;
            states_UpdateTime = updateTime;
        }

        // TODO : 시그널 확인 함수
        public bool MonitoringSignals()
        {
            if (double.Parse(states_CurPrice) >= double.Parse(states_highPrice))
                signal_1 = true;

            // 기준 잡기.
            if (states_TickList.Average() > states_BeforeAvg * 10)
                signal_2 = true;

            // 거래대금도 detail 하게 다시 잡기
            if (states_TickOneMinList.Sum() * double.Parse(states_CurPrice) > 1000000000)
                signal_3 = true;

            if (signal_1 && signal_2 && signal_3)
            {
                Signals = true;
            }
  
            return Signals;
        }
    }
}
