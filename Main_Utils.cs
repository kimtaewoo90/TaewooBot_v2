using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace TaewooBot_v2
{
    partial class Main
    {


        public void GetAccountInformation()
        {
            string scr_no = utils.get_scr_no();
            API.SetInputValue("계좌번호", botParams.AccountNumber);
            API.SetInputValue("비밀번호", "");
            API.SetInputValue("상장폐지조회구분", "0");
            API.SetInputValue("비밀번호입력매체구분", "00");


            botParams.RqName = "계좌평가현황요청";
            API.CommRqData(botParams.RqName, "OPW00004", 0, scr_no);

        }

        // 전종목 주식데이터 요청
        public void RequestAllStocks()
        {
            string Market = "Kosdaq";

            if (Market == "Kosdaq")
            {
                string res = API.GetCodeListByMarket("10");
                botParams.Codes = res.Split(new char[] { ';' });

            }
            else if (Market == "Kospi")
            {
                string res = API.GetCodeListByMarket("0");
                botParams.Codes = res.Split(new char[] { ';' });
            }

            else
            {
                string kospi = API.GetCodeListByMarket("0");
                string kosdaq = API.GetCodeListByMarket("10");
                string res = kospi + kosdaq;
                botParams.Codes = res.Split(new char[] { ';' });
            }

            // 종목개수 제한...
            for (int i = 0; i < 50; i++)
            {
                botParams.StockCnt = i + 1;
                string scr_no = utils.get_scr_no();
                // 주가 데이터 요청.
                botParams.RqName = "";
                botParams.RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", botParams.Codes[i]);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(botParams.RqName, "OPT10001", 0, scr_no);

                utils.delay(300);
            }

        }

        public void MonitoringSellStocks()
        {
            /*
             Accnt_StockName
             Accnt_StockLots
             Accnt_StockPnL
             Accnt_StockPnL_Won
             */

            foreach (KeyValuePair<string, string> pair in botParams.Accnt_StockPnL)
            {
                if (Double.Parse(pair.Value) > 3.0)
                {
                    GetAccountInformation();
                    // SendSellOrder(pair.Key);
                    string scr_no = utils.get_scr_no();
                    // 1: 신규매수, 2: 신규매도, 3: 매수취소, 4: 매도취소, 5: 매수정정, 6:매도정정
                    API.SendOrder("주식매도요청", scr_no, botParams.AccountNumber, 2, pair.Key, 10, 0, "03", "");

                }
            }
        }

        public void MonitoringBuyStocks()
        {
            /*
            targetDict
            StockKrNameDict
            StockPriceDict
            TickSpeedDict
            */
            int StockCnt = botParams.TickSpeedDict.Count;

            foreach (KeyValuePair<string, string> pair in botParams.TickSpeedDict)
            {
                // TODO : 100이 아니라 Indicator 개발하기.
                if (Int32.Parse(pair.Value) > 100)
                {
                    GetAccountInformation();
                    string scr_no = utils.get_scr_no();

                    // 1: 신규매수, 2: 신규매도, 3: 매수취소, 4: 매도취소, 5: 매수정정, 6:매도정정
                    API.SendOrder("주식매수요청", scr_no, botParams.AccountNumber, 1, pair.Key, 10, 0, "03", "");

                    // SendBuyOrder(pair.Key);
                }
            }
        }

        // Thread간 winform 객체에 접근 
        public void setText_Control(Control ctr, string txtValue)
        {
            if (ctr.InvokeRequired)
            {
                Ctr_Involk CI = new Ctr_Involk(setText_Control);
                ctr.Invoke(CI, ctr, txtValue);
            }
            else
            {
                ctr.Text = txtValue;
            }
        }

        private void ParamsBtn_Click(object sender, EventArgs e)
        {
            Params.Show();
        }

        // 종목이름 요청
        public string GetKrName(string Code)
        {
            string KrName = null;
            KrName = API.GetMasterCodeName(Code);

            return KrName;
        }

        // 호가가격단위 가져오기 메서드
        public int get_hoga_unit_price(int price, string jongmok_cd, int hoga_unit_jump)
        {
            int market_type;
            int rest;

            market_type = 0;

            try
            {
                // 시장구분 가져오기
                market_type = int.Parse(API.GetMarketType(jongmok_cd).ToString());
            }

            catch (Exception ex)
            {
                utils.write_sys_log("호가단위 가져오는 중 다음 에러 발생 : [ " + ex.Message + "]\n", 0);
            }

            // 천원 미만
            if (price < 1000)
            {
                return price + (hoga_unit_jump * 1);
            }
            // 천원 이상 오천원 미만
            else if (1000 <= price && price < 5000)
            {
                rest = price % 5;
                if (rest == 0)
                {
                    return price + (hoga_unit_jump * 5);
                }
                else if (rest < 3)
                {
                    return (price - rest) + (hoga_unit_jump * 5);
                }
                else
                {
                    return (price + (5 - rest)) + (hoga_unit_jump * 5);
                }
            }
            // 오천원 이상 만원 미만
            else if (price >= 5000 && price < 10000)
            {
                rest = price % 10;
                if (rest == 0)
                {
                    return price + (hoga_unit_jump * 10);
                }
                else if (rest < 5)
                {
                    return (price - rest) + (hoga_unit_jump * 10);
                }
                else
                {
                    return (price + (10 - rest)) + (hoga_unit_jump * 10);
                }
            }
            // 만원 이상 오만원 미만
            else if (price >= 10000 && price < 50000)
            {
                rest = price % 50;
                if (rest == 0)
                {
                    return price + (hoga_unit_jump * 50);
                }
                else if (rest < 25)
                {
                    return (price - rest) + (hoga_unit_jump * 50);
                }
                else
                {
                    return (price + (50 - rest)) + (hoga_unit_jump * 50);
                }
            }
            // 오만원 이상 십만원 미만
            else if (price >= 50000 && price < 100000)
            {
                rest = price % 100;
                if (rest == 0)
                {
                    return price + (hoga_unit_jump * 100);
                }
                else if (rest < 50)
                {
                    return (price - rest) + (hoga_unit_jump * 100);
                }
                else
                {
                    return (price + (100 - rest)) + (hoga_unit_jump * 100);
                }
            }
            // 십만원 이상 오십만원 미만 (장 구분)
            else if (price >= 100000 && price < 500000)
            {
                if (market_type == 10)
                {
                    rest = price % 100;
                    if (rest == 0)
                    {
                        return price + (hoga_unit_jump * 100);
                    }
                    else if (rest < 50)
                    {
                        return (price - rest) + (hoga_unit_jump * 100);
                    }
                    else
                    {
                        return (price + (100 - rest)) + (hoga_unit_jump * 100);
                    }
                }
                else
                {
                    rest = price % 500;
                    if (rest == 0)
                    {
                        return price + (hoga_unit_jump * 500);
                    }
                    else if (rest < 250)
                    {
                        return (price - rest) + (hoga_unit_jump * 500);
                    }
                    else
                    {
                        return (price + (500 - rest)) + (hoga_unit_jump * 500);
                    }
                }
            }
            // 50만원 이상
            else if (price >= 500000)
            {
                if (market_type == 10)
                {
                    rest = price % 100;
                    if (rest == 0)
                    {
                        return price + (hoga_unit_jump * 100);
                    }
                    else if (rest < 50)
                    {
                        return (price - rest) + (hoga_unit_jump * 100);
                    }
                    else
                    {
                        return (price + (100 - rest)) + (hoga_unit_jump * 100);
                    }
                }
                else
                {
                    rest = price % 1000;
                    if (rest == 0)
                    {
                        return price + (hoga_unit_jump * 1000);
                    }
                    else if (rest < 500)
                    {
                        return (price - rest) + (hoga_unit_jump * 1000);
                    }
                    else
                    {
                        return (price + (1000 - rest)) + (hoga_unit_jump * 1000);
                    }
                }
            }

            return 0;
        }


        // TargetDict 의 종목 리스트에 대해 실시간 데이터 요청
        public void ReqRealData(string codes, string scr_no)
        {
            int repeatCnt = 0;
            int loopCnt = 0;
            bool ExitFunc = false;

            // Dictionary 에 조건검색 종목, 화면번호 저장
            // 중복 방지
            botParams.targetDict.Add(codes, scr_no);


            while (true)
            {
                botParams.RqName = "";
                botParams.RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", codes);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(botParams.RqName, "OPT10001", 0, scr_no);

                if (res == 0)
                {
                    utils.write_sys_log("Reqeust 'OPT10001' ReqRealData", 0);
                    break;
                }
                //delay(2000);
            }

            for (; ; )
            {
                if (repeatCnt == 5)
                {
                    utils.write_sys_log("[ " + GetKrName(codes) + " ]의 시세 받기 실패.", 0);
                    break;
                }

                if (ExitFunc is true)
                {
                    break;
                }

                try
                {
                    loopCnt = 0;
                    for (; ; )
                    {
                        utils.delay(1000);
                        //write_sys_log(TargetCodes.ToString(), 0);
                        // 데이터 조회 성공

                        if (botParams.TargetCodes.Contains(codes))
                        {
                            utils.delay(200);
                            string msg = $"{codes}'s price is {botParams.StockPriceDict[codes]}";
                            utils.write_sys_log(msg, 0);
                            utils.write_sys_log("종목 [ " + GetKrName(codes) + " ] 데이터 조회 완료", 0);
                            ExitFunc = true;
                            break;
                        }
                        else
                        {
                            utils.write_sys_log("[ " + GetKrName(codes) + " ] / 데이터 조회 요청중...", 0);
                            utils.delay(200);
                            loopCnt++;
                            if (loopCnt == 5)
                            {
                                repeatCnt++;
                                loopCnt = 0;
                                break;
                            }

                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    utils.write_sys_log("거래량 조회중 다음 에러 발생 : [ " + ex.Message + " ]", 0);
                }

                utils.delay(200);

            }  // end of 거래량 조회

        }
    }
}
