using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    public class StockState
    {

        public string states_ShortCode { get; set; }
        public string states_KrName { get; set; }
        public string states_CurPrice { get; set; }
        public string states_highPrice { get; set; }
        public string states_lowPrice { get; set; }
        public string states_Change { get; set; }
        public string states_ContractLots { get; set; }
        public List<double> states_TickList { get; set; }
        public List<double> states_TickOneMinList { get; set; }
        public DateTime states_UpdateTime { get; set; }

        public bool Signals { get; set; } = false;
        public bool signal_1 { get; set; } = false;
        public bool signal_2 { get; set; } = false;
        public bool signal_3 { get; set; } = false;


        public StockState(string ShortCode,
                          string KrName,
                          string curPrice,
                          string highPrice,
                          string lowPrice,
                          string change, 
                          string contactLots, 
                          List<double> tickList, 
                          List<double> tickOneList, 
                          DateTime updateTime)
        {
            states_ShortCode = ShortCode;
            states_KrName = KrName;
            states_CurPrice = curPrice;
            states_highPrice = highPrice;
            states_lowPrice = lowPrice;
            states_Change = change;
            states_ContractLots = contactLots;
            states_UpdateTime = updateTime;
            states_TickList = tickList;
            states_TickOneMinList = tickOneList;
            states_UpdateTime = updateTime;
        }

        // TODO : 시그널 확인 함수
        public bool MonitoringSignals_Strategy1()
        {
            if (double.Parse(states_CurPrice) >= double.Parse(states_highPrice))
                signal_1 = true;

            // 기준 잡기.  tickList average => OneMinTickList average
            if (states_TickOneMinList.Average() > BotParams.BeforeAvg[states_ShortCode] * 2 && BotParams.BeforeAvg[states_ShortCode] != 0)
                signal_2 = true;

            // 거래대금도 detail 하게 다시 잡기
            if (states_TickOneMinList.Sum() * double.Parse(states_CurPrice) > 100000000)
                signal_3 = true;

            if (signal_1 && signal_2 && signal_3)
            {
                Signals = true;
            }
  
            return Signals;
        }
    }
}
