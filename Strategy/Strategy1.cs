using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaewooBot_v2.Strategy
{
    public class Strategy1
    {
        TelegramClass telegram = new TelegramClass();
        Utils utils = new Utils();
        Logs logs = new Logs();

        public Strategy1()
        {

        }

        public void CalculateTickSpeed(string shortCode, string contractLots, double curPrice, double startPrice )
        {
            // Add Total Tick List
            BotParams.TickList[shortCode].Add(double.Parse(contractLots));
            logs.write_sys_log($"{shortCode} updated TickList", 0);

            // Add 1 Min Tick List                
            BotParams.TickOneMinsList[shortCode].Add(double.Parse(contractLots));

            // 1분봉 내 저가
            BotParams.LowPriceOneMinute[shortCode] = double.Parse(BotParams.stockState[shortCode].states_lowPrice);

            // 현재가 < 시가 or 현재가 < 1분봉 저가
            if (startPrice > curPrice || BotParams.LowPriceOneMinute[shortCode] > curPrice)
                BotParams.LowPriceOneMinute[shortCode] = curPrice;

            // 1분 시간 비교를 위한 comparedTime 선언
            //BotParams.comparedTime = BotParams.stockState[shortCode].states_UpdateTime;

            // 1분 비교 (1분 시간 계산)
            if (DateTime.Parse(BotParams.CurTime) > BotParams.comparedTime.AddMinutes(1))
            {
                BotParams.comparedTime = DateTime.Parse(BotParams.CurTime);
                if (BotParams.TickOneMinsList.ContainsKey(shortCode))
                {
                    BotParams.TickOneMinsList.Remove(shortCode);
                    BotParams.TickOneMinsList[shortCode] = new List<double> { double.Parse(contractLots) };
                }
                BotParams.LowPriceOneMinute[shortCode] = curPrice;
                BotParams.BeforeAvg[shortCode] = BotParams.TickList[shortCode].Average();
            }
        }

        public void MonitoringSellSignals(string shortCode, string curPrice, string buyPrice, string change)
        {
            // Reset the Sell signals for ShortCode1.
            BotParams.SellSignals[shortCode] = false;

            // Monitoring Sell Signals
            if (DateTime.Parse(BotParams.BlotterStateDict[shortCode].OrderTime).AddSeconds(30) < DateTime.Parse(BotParams.CurTime) && double.Parse(curPrice) != 0.0)
            {
                // 30초 후 현재가 < 매수가 => 매도
                if (double.Parse(curPrice) < double.Parse(buyPrice))
                {
                    //BotParams.SellSignals[shortCode] = true;
                    //telegram.SendTelegramMsg($"[{shortCode}] cur_price < buy_price....Lostcut");
                    //return;
                }

                // 익절 1% 손절 -1%
                if (double.Parse(change) > 1.30 || double.Parse(change) < -0.7)
                {
                    BotParams.SellSignals[shortCode] = true;
                    //telegram.SendTelegramMsg($"[{shortCode}] Target change rates : {change}");
                    return;
                }

                //TODO : 매수 후 리셋 된 avgerage tick list > 현재 1분봉(30초) average * 2  => 매도
                if (BotParams.BeforeAvg[shortCode] > BotParams.TickOneMinsList[shortCode].Average() * 4)
                {
                    // BotParams.SellSignals[shortCode] = true;
                   // telegram.SendTelegramMsg($"[{shortCode}] BeforeAvg > OneMins Avg * 2");
                   // return;
                }
            }
        }

        public void ResetTickDataList(string shortCode)
        {
            BotParams.TickList[shortCode] = new List<double>() { 0.0 };
            BotParams.TickOneMinsList[shortCode] = new List<double>() { 0.0 };
            BotParams.BeforeAvg[shortCode] = BotParams.TickList[shortCode].Average();
        }
    }
}
