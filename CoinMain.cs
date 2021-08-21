using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;



namespace TaewooBot_v2
{
    class CoinMain
    {
        Logs logs = new Logs();

        static string ticker_url = "https://api.upbit.com/v1/ticker";
        static string account_url = "https://api.upbit.com/v1/accounts";
        static string UUID = Guid.NewGuid().ToString();
        static string AccessKey = "yTg9B5SL2BYgQxTed5LVbuVs7gXPa2czy7xDDx5m"; //발급받은 AccessKey를 넣어줍니다.
        static string SecretKey = "dxzr3r5KcJTxMkVCrFefWzhluFWPxdS1MDtAzKk5"; //발급받은 SecretKey를 넣어줍니다.
        public List<JObject> AccountInquiry() //나의 계좌 조회 (전체 계좌 조회)
        {

            var payload = new JwtPayload
            {
                { "access_key", AccessKey },
                { "nonce", Guid.NewGuid().ToString() },
                { "query_hash_alg", "SHA512" }
            };

            byte[] keyBytes = Encoding.Default.GetBytes(SecretKey);
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyBytes);
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, "HS256");
            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(secToken);
            var autorize_token = "Bearer " + jwtToken;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(account_url);
            request.Method = "GET";
            request.Headers.Add(string.Format("Authorization:{0}", autorize_token));
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResult = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            var Result = JArray.Parse(strResult).ToObject<List<JObject>>();

            for (int i = 0; i < Result.Count; i++)
            {
                // {{  "currency", "balance", "locked",  "avg_buy_price",  "avg_buy_price_modified",  "unit_currency"}}
                logs.write_sys_log($"currency : {Result[i].GetValue("currency").ToString().Trim()} / balance : {Result[i].GetValue("balance").ToString().Trim()}", 0);
            }

            return Result;

        }


    }
}
