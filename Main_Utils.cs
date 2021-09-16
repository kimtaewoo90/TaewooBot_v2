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
            API.SetInputValue("계좌번호", BotParams.AccountNumber);
            API.SetInputValue("비밀번호", "");
            API.SetInputValue("상장폐지조회구분", "0");
            API.SetInputValue("비밀번호입력매체구분", "00");


            BotParams.RqName = "계좌평가현황요청";
            API.CommRqData(BotParams.RqName, "OPW00004", 0, scr_no);
        }

        public void GetShortCodes(string market)
        {
            if (market == "Kosdaq")
            {
                string res = API.GetCodeListByMarket("10");
                BotParams.Codes = res.Split(new char[] { ';' });

            }
            else if (market == "Kospi")
            {
                string res = API.GetCodeListByMarket("0");
                BotParams.Codes = res.Split(new char[] { ';' });
            }

            else if (market == "MM")
            {
                string[] ShortCodeList = System.IO.File.ReadAllLines(BotParams.Path + "ShortCode.txt");
                BotParams.Codes = ShortCodeList;
            }

            else
            {
                string kospi = API.GetCodeListByMarket("0");
                string kosdaq = API.GetCodeListByMarket("10");
                string res = kospi + kosdaq;
                BotParams.Codes = res.Split(new char[] { ';' });
            }

            

        }

        // 전종목 주식데이터 요청
        public void RequestStocksData()
        {
            // 종목개수 제한...
            for (int i = 0; i < BotParams.Codes.Length; i++)
            {
                BotParams.StockCnt = i + 1;
                string scr_no = utils.get_scr_no();
                // 주가 데이터 요청.
                BotParams.RqName = "";
                BotParams.RqName = "주식기본정보";   // 해당 종목 데이터 요청 이름.
                API.SetInputValue("종목코드", BotParams.Codes[i]);

                BotParams.RequestRealDataScrNo.Add(scr_no);

                // 실시간 현재가 받아오기
                int res = API.CommRqData(BotParams.RqName, "OPT10001", 0, scr_no);

                utils.delay(300);
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
                logs.write_sys_log("호가단위 가져오는 중 다음 에러 발생 : [ " + ex.Message + "]\n", 0);
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
        
    }
}
