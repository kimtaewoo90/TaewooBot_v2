﻿using System;
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
    public partial class Main
    {

        // Utils
        //  현재시간 불러오기
        public string get_cur_tm()
        {
            DateTime cur_time;
            string cur_tm;

            cur_time = DateTime.Now;
            cur_tm = cur_time.ToString("HH:mm:ss");

            return cur_tm;
        }

        // 시스템 로그 함수 구현
        public void write_sys_log(String text, int is_Clear)
        {

            
            DateTime cur_time;
            String cur_dt;
            String cur_tm;
            String cur_dtm;

            cur_dt = "";
            cur_tm = "";

            cur_time = DateTime.Now;
            cur_dt = cur_time.ToString("yyyy-") + cur_time.ToString("MM-") + cur_time.ToString("dd");
            cur_tm = get_cur_tm();

            cur_dtm = "[" + cur_dt + " " + cur_tm + "]";

            if (is_Clear == 1)
            {
                if (logs.LogBox.InvokeRequired)
                {
                    logs.LogBox.BeginInvoke(new Action(() => logs.LogBox.Clear()));
                }
                else
                {
                    this.logs.LogBox.Clear();
                }
            }

            else
            {
                if (this.logs.LogBox.InvokeRequired)
                {
                    logs.LogBox.BeginInvoke(new Action(() => logs.LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine)));
                }

                else
                {
                    this.logs.LogBox.AppendText("\n" + cur_dtm + text + Environment.NewLine);
                    // log 기록

                    File.AppendAllText(Path + LogFileName, cur_dtm + text + Environment.NewLine, Encoding.Default);
                }
            }
        }

        public void MakeLogFile()
        {

            FileInfo fileInfo = new FileInfo(Path + LogFileName);

            if (fileInfo.Exists)
            {}
            else
            {
                File.WriteAllText(Path + LogFileName, date + "의 로그기록을 시작합니다.\n", Encoding.Default);
                write_sys_log("로그파일 생성 완료", 0);
            }

        }

        public void MakeTickDataFile()
        {
            FileInfo fileInfo = new FileInfo(TickPath + TickLogFileName);

            if (fileInfo.Exists)
            { }
            else
            {
                File.WriteAllText(TickPath + TickLogFileName, date + "틱데이터의 기록을 시작합니다\n.", Encoding.Default);
                write_sys_log("틱데이터파일 생성 완료", 0);
            }
        }
        // 로그인 함수 구현
        private void Login()
        {
            int ret = 0;
            int ret2 = 0;

            String accno_cnt = null;
            string[] accno_arr = null;

            ret = API.CommConnect(); // 로그인 창 호출

            Status.Text = "로그인 중..."; // 화면 하단 상태란에 메시지 출력

            for (; ; )
            {
                ret2 = API.GetConnectState(); // 로그인 완료 여부를 가져옴
                if (ret2 == 1)
                {
                    // 로그인 성공
                    break;
                }
                else
                {
                    // 로그인 대기
                    delay(1000); // 1초 지연
                }
            } // end for

            Status.Text = "로그인 완료"; // 화면 하단 상태란에 메시지 출력

            UserID = "";
            UserID = API.GetLoginInfo("USER_ID").Trim(); // 사용자 아이디를 가져와서 클래스 변수(전역변수)에 저장
            //textBox1.Text = g_user_id; // 전역변수에 저장한 아이디를 텍스트박스에 출력

            accno_cnt = "";
            accno_cnt = API.GetLoginInfo("ACCOUNT_CNT").Trim(); // 사용자의 증권계좌 수를 가져옴

            // TODO : Error
            accno_arr = new string[int.Parse(accno_cnt)];

            AccountNumber = API.GetLoginInfo("ACCNO").Trim().Replace(";","");

            accno_arr = AccountNumber.Split(';');  // API에서 ';'를 구분자로 여러개의 계좌번호를 던져준다.
            write_sys_log("Account Number : " + AccountNumber, 0);
            write_sys_log("Welcome " + UserID, 0);


        }

        // 지연함수 구현
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public DateTime delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                try
                {
                    unsafe
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                catch (AccessViolationException ex)
                {
                    write_sys_log("delay() ex.Message : [" + ex.Message + "]\r\n", 0);
                }

                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        // 요청번호 부여 함수 구현
        private string get_scr_no()
        {
            if (ScrNo < 9999)
            {
                ScrNo++;
            }
            else ScrNo = 1000;

            return ScrNo.ToString();
        }

        // Params 초기화
        private void InitialParams()
        {
            IsThread = false;
            _SearchCondition = false;
            _GetTrData = false;
            _GetRTD = false;
            LossCut = 0.03;
            ScrNo = 1000;

            //GetDataTextBox.Text = "Not yet";
            //MonitoringTextBox.Text = "Not yet";
            //OrderTextBox.Text = "Not yet";
        }

        public void RemoveDict(string StockCode)
        {

            targetDict.Remove(StockCode);
            StockKrNameDict.Remove(StockCode);
            StockPriceDict.Remove(StockCode);
            TickSpeedDict.Remove(StockCode);
            StockPnLDict.Remove(StockCode);


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
                write_sys_log("호가단위 가져오는 중 다음 에러 발생 : [ " + ex.Message + "]\n", 0);
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
            targetDict.Add(codes, scr_no);


            while (true)
            {
                RqName = "";
                RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", codes);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(RqName, "OPT10001", 0, scr_no);

                if (res == 0)
                {
                    write_sys_log("Reqeust 'OPT10001' ReqRealData", 0);
                    break;
                }
                //delay(2000);
            }

            for (; ; )
            {
                if (repeatCnt == 5)
                {
                    write_sys_log("[ " + GetKrName(codes) + " ]의 시세 받기 실패.", 0);
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
                        delay(1000);
                        //write_sys_log(TargetCodes.ToString(), 0);
                        // 데이터 조회 성공
                        
                        if (TargetCodes.Contains(codes))
                        {
                            delay(200);
                            string msg = $"{codes}'s price is {StockPriceDict[codes]}" ;
                            write_sys_log(msg, 0);
                            write_sys_log("종목 [ " + GetKrName(codes) + " ] 데이터 조회 완료", 0);
                            ExitFunc = true;
                            break;
                        }
                        else
                        {
                            write_sys_log("[ " + GetKrName(codes) + " ] / 데이터 조회 요청중...", 0);
                            delay(200);
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
                    write_sys_log("거래량 조회중 다음 에러 발생 : [ " + ex.Message + " ]", 0);
                }

                delay(200);

            }  // end of 거래량 조회

        }

        public void DisplayTargetStocks(string Type, string StockCode, string StockName, string Price, string Change, string TickSpeed)
        {
            if (Type == "Insert")
            {
                int AddCnt = universe.TargetStocks.Rows.Count;
                // StockCode, StockKrName, Price, TickSpeed, PnL

                universe.TargetStocks.Rows.Add(StockCode, StockName, Price, TickSpeed, Change);

                
                write_sys_log(StockCode + "종목이 추가 되었습니다.", 0);
            }

            else if (Type == "Update")
            {
                int UpdateCnt = universe.TargetStocks.Rows.Count;
                for (int i = 0; i < UpdateCnt-1; i++)
                {
                    if (universe.TargetStocks["StockCode", i].Value.ToString() == StockCode)
                    {
                        universe.TargetStocks.Rows[i].Cells[2].Value = Price;
                        universe.TargetStocks.Rows[i].Cells[3].Value = Change;
                        universe.TargetStocks.Rows[i].Cells[4].Value = TickSpeed;

                    }
                }
            }
        }

        public void DisplayPosition(string StockCode, string StockName, string Price, string PnL, string status)
        {
            position.PositionDataGrid.Rows.Add(StockCode, StockName, Price, PnL, status);
        }


        public void DeleteTargetStocks(string StockCode)
        {
            try
            {
                int DelCnt = universe.TargetStocks.Rows.Count;
                for (int i = 0; i < DelCnt; i++)
                {
                    try
                    {
                        if (universe.TargetStocks["StockCode", i].Value.ToString() == StockCode && universe.TargetStocks != null)
                        {
                            // 해당 데이터 삭제
                            universe.TargetStocks.Rows.Remove(universe.TargetStocks.Rows[i]);

                            write_sys_log(StockCode + "종목이 삭제 되었습니다.", 0);
                            break;

                        }
                    }
                    catch(Exception err)
                    {
                        write_sys_log(err.ToString(), 0);
                    }

                }
            }

            catch (Exception err)
            {
                write_sys_log(err.ToString(), 0);
            }

        }

        public void GetTickSpeed(string StockCode, string ContractLots)
        {
            int TickSpeed = 0;

            try
            {
                if (TickSpeedDict[StockCode] != null)
                {
                    TickSpeed = Int32.Parse(TickSpeedDict[StockCode]) + Int32.Parse(ContractLots);
                }

                else
                {
                    TickSpeed = Int32.Parse(ContractLots);
                }

                TickSpeedDict[StockCode] = TickSpeed.ToString();
                write_sys_log("ContractLots of [ " + StockCode + " ] is " + ContractLots, 0);

            }

            catch (Exception err)
            {
                write_sys_log(err.ToString(), 0);
            }



        }

        public void GetAccountInformation()
        {
            string scr_no = get_scr_no();
            API.SetInputValue("계좌번호", AccountNumber);
            // 비밀번호는 숨기자 나중에
            API.SetInputValue("비밀번호", "");
            API.SetInputValue("상장폐지조회구분", "0");
            API.SetInputValue("비밀번호입력매체구분", "00");


            RqName = "계좌평가현황요청";
            API.CommRqData(RqName, "OPW00004", 0, scr_no);

        }

        // 전종목 주식데이터 요청
        public void RequestAllStocks()
        {
            string Market = "Kosdaq";

            if (Market == "Kosdaq")
            {
                string res = API.GetCodeListByMarket("10");
                Codes = res.Split(new char[] { ';' });

            }
            else if (Market == "Kospi")
            {
                string res = API.GetCodeListByMarket("0");
                Codes = res.Split(new char[] { ';' });
            }

            else
            {
                string kospi = API.GetCodeListByMarket("0");
                string kosdaq = API.GetCodeListByMarket("10");
                string res = kospi + kosdaq;
                Codes = res.Split(new char[] { ';' });
            }

            // 종목개수 제한...
            for (int i = 0; i < 50; i++)
            {
                StockCnt = i + 1;
                string scr_no = get_scr_no();
                // 주가 데이터 요청.
                RqName = "";
                RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", Codes[i]);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(RqName, "OPT10001", 0, scr_no);

                delay(300);
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

            foreach(KeyValuePair<string, string> pair in Accnt_StockPnL)
            {
                if (Double.Parse(pair.Value) > 3.0)
                {
                    GetAccountInformation();
                    // SendSellOrder(pair.Key);
                    string scr_no = get_scr_no();
                    // 1: 신규매수, 2: 신규매도, 3: 매수취소, 4: 매도취소, 5: 매수정정, 6:매도정정
                    API.SendOrder("주식매도요청", scr_no, AccountNumber, 2, pair.Key, 10, 0, "03", ""); 

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
            int StockCnt = TickSpeedDict.Count;

            foreach(KeyValuePair<string, string> pair in TickSpeedDict)
            {
                // TODO : 100이 아니라 Indicator 개발하기.
                if(Int32.Parse(pair.Value) > 100)
                {
                    GetAccountInformation();
                    string scr_no = get_scr_no();
                    
                    // 1: 신규매수, 2: 신규매도, 3: 매수취소, 4: 매도취소, 5: 매수정정, 6:매도정정
                    API.SendOrder("주식매수요청", scr_no, AccountNumber, 1, pair.Key, 10, 0, "03", "");

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


    }
}
