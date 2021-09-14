using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2
{
    public static class BotParams
    {
        public static string version { get; set; } = "2.0.001";
        
        // Initialize global params
        public static string Market { get; set; }
        public static string UserID { get; set; }
        public static int ScrNo { get; set; }
        public static bool IsThread { get; set; }
        public static bool CoinThread { get; set; }
        public static bool _SearchCondition { get; set; }
        public static bool _GetTrData { get; set; }
        public static bool _GetRTD { get; set; }
        public static string RqName { get; set; }
        public static string CurTime { get; set; }
        public static bool ArrangingPosition { get; set; } = false;

        public static bool Test { get; set; } = false;

        // About Account
        public static string AccountNumber { get; set; } = null;
        public static double Deposit { get; set; } = 0.0;
        public static int AccountStockLots { get; set; }
        public static int TotalPnL { get; set; }

        // Slow Params
        public static double LossCut { get; set; }
        public static DateTime comparedTime { get; set; }


        // Logs File
        public static string date { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        public static string Path { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/LogFile/";
        public static string TickPath { get; set; } = "C:/Users/tangb/source/repos/TaewooBot_v2/LogFile/TickLog/";
        public static string LogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_Log.txt";
        public static string TickLogFileName { get; set; } = DateTime.Now.ToString("yyyyMMdd") + "_TickLog.txt";

        // Stocks
        public static List<string> TargetCodes = new List<string>();
        public static List<string> RequestRealDataScrNo = new List<string>();

        // Market
        public static string[] Codes;
        public static string Kospi { get; set; } = null;
        public static string Kosdaq { get; set; } = null;
        public static string AllMarket { get; set; } = null;

        // Thread 간 동기화
        public static bool signal { get; set; } = false;
        public static bool order { get; set; } = false;
        public static string SignalStockCode { get; set; } = null;
        public static string SignalKrName { get; set; } = null;
        public static string SignalPrice { get; set; } = null;

        // Tick Speed Dictionary.
        public static Dictionary<string, List<double>> TickList = new Dictionary<string, List<double>>();
        public static Dictionary<string, List<double>> TickOneMinsList = new Dictionary<string, List<double>>();
        public static Dictionary<string, double> BeforeAvg = new Dictionary<string, double>();

        // Test
        public static int StockCnt { get; set; } = 0;

        // 항목별 DIctionary 설정

        public static Dictionary<string, string> targetDict = new Dictionary<string, string>();
        public static Dictionary<string, string> StockKrNameDict = new Dictionary<string, string>();
        public static Dictionary<string, string> StockPriceDict = new Dictionary<string, string>();
        public static Dictionary<string, string> TickSpeedDict = new Dictionary<string, string>();
        public static Dictionary<string, string> StockPnLDict = new Dictionary<string, string>();
        public static Dictionary<string, string> StockHighPriceDict = new Dictionary<string, string>();

        public static Dictionary<string, List<string>> TickAvgDict = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> StockDict = new Dictionary<string, List<string>>();

        public static Dictionary<string, StockState> stockState = new Dictionary<string, StockState>();


        // 계좌관련 Dictionary 설정
        public static Dictionary<string, PositionState> Accnt_Position = new Dictionary<string, PositionState>();

        public static Dictionary<string, string> Accnt_StockName = new Dictionary<string, string>();
        public static Dictionary<string, string> Accnt_StockLots = new Dictionary<string, string>();
        public static Dictionary<string, string> Accnt_StockPnL = new Dictionary<string, string>();
        public static Dictionary<string, string> Accnt_StockPnL_Won = new Dictionary<string, string>();

        // Blotter List
        public static List<string> BltList = new List<string>();
        public static List<string> AccountList = new List<string>();
        public static List<string> PositionList = new List<string>();
        public static List<string> OrderedStocks = new List<string>();
        public static List<string> OrderingStocks = new List<string>();
        
        public static Dictionary<List<string>, string> OrderNumberAndOrderType = new Dictionary<List<string>, string>();
        public static Dictionary<string, BlotterState> BlotterStateDict = new Dictionary<string, BlotterState>();
        //public static Dictionary<string, double> LowPriceOneMinute = new Dictionary<string, double>();

        // Order
        public static Dictionary<string, bool> SellSignals = new Dictionary<string, bool>();
        public static List<string> PendingOrders = new List<string>();

        // Position Dictionary
        public static Dictionary<string, PositionState> PositionDict = new Dictionary<string, PositionState>();




    }
}
