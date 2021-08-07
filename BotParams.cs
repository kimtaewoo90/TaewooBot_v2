using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    class BotParams
    {

        // Initialize global params
        public string Market { get; set; }
        public string UserID { get; set; }
        public int ScrNo { get; set; }
        public bool IsThread { get; set; }
        public bool _SearchCondition { get; set; }
        public bool _GetTrData { get; set; }
        public bool _GetRTD { get; set; }
        public string RqName { get; set; }
        public string CurTime { get; set; }

        public bool Test { get; set; } = false;

        // About Account
        public string AccountNumber { get; set; } = null;
        public double Deposit { get; set; } = 0.0;
        public int AccountStockLots { get; set; }
        public int TotalPnL { get; set; }

        // Slow Params
        public double LossCut { get; set; }

        // Logs File
        public string date { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        public string Path { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/Log/";
        public string TickPath { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/Log/TickLog/";
        public string LogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_Log.txt";
        public string TickLogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_TickLog.txt";

        // Stocks
        public List<string> TargetCodes = new List<string>();

        public string[] Codes;
        public string Kospi { get; set; } = null;
        public string Kosdaq { get; set; } = null;
        public string AllMarket { get; set; } = null;

        // Thread 간 동기화
        public bool signal { get; set; } = false;
        public bool order { get; set; } = false;
        public string SignalStockCode { get; set; } = null;
        public string SignalKrName { get; set; } = null;
        public string SignalPrice { get; set; } = null;

   

        // Test
        public int StockCnt { get; set; } = 0;

        // 항목별 DIctionary 설정

        public Dictionary<string, string> targetDict = new Dictionary<string, string>();
        public Dictionary<string, string> StockKrNameDict = new Dictionary<string, string>();
        public Dictionary<string, string> StockPriceDict = new Dictionary<string, string>();
        public Dictionary<string, string> TickSpeedDict = new Dictionary<string, string>();
        public Dictionary<string, string> StockPnLDict = new Dictionary<string, string>();
        public Dictionary<string, string> StockHighPriceDict = new Dictionary<string, string>();

        public Dictionary<string, List<string>> TickAvgDict = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> StockDict = new Dictionary<string, List<string>>();


        // 계좌관련 Dictionary 설정
        public Dictionary<string, string> Accnt_StockName = new Dictionary<string, string>();
        public Dictionary<string, string> Accnt_StockLots = new Dictionary<string, string>();
        public Dictionary<string, string> Accnt_StockPnL = new Dictionary<string, string>();
        public Dictionary<string, string> Accnt_StockPnL_Won = new Dictionary<string, string>();





    }
}
