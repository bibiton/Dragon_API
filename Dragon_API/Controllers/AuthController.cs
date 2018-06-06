using Dragon_Library.Filter;
using Helper.EncryptExtensions;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Dragon_Library.API;
using System.Collections.Generic;

namespace Dragon_API.Controller
{
    [CrossDomainFilter]
    public class AuthController : ApiController
    {
        #region AppSettings
        private string ServerTokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
        #endregion

        #region DES加密Key
        private byte[] byteKey = { 1, 3, 5, 8, 2, 4, 6, 7 };
        private byte[] byteIV = { 1, 9, 0, 4, 3, 5, 2, 6 };
        #endregion

        /// <summary>
        /// 取得Server_Token
        /// </summary>
        /// <returns></returns>
        public ServerTokenResult Get()
        {
            ServerTokenResult result = new ServerTokenResult();
            IEnumerable<string> server_account;
            IEnumerable<string> server_password;

            if (Request.Headers.TryGetValues("server_account", out server_account) && Request.Headers.TryGetValues("server_password", out server_password))
            {
                try
                {
                    if (server_account.First() == "Dragon" && server_password.First() == "Dragon12345")
                    {
                        string st = "Auth,Auth," + DateTime.Now.AddHours(8).AddDays(1).ToString("yyyy/MM/dd HH:mm:ss") + ",App";
                        string token = st.DESEncode(ServerTokenDecryptString);

                        result.result_status = true;
                        result.result_message = "成功取得伺服端金鑰";
                        result.result_content = token;
                    }
                    else
                    {
                        result.result_status = false;
                        result.result_message = "帳號或密碼錯誤";
                        result.result_content = null;
                    }
                }
                catch (Exception ex)
                {
                    result.result_status = false;
                    result.result_message = "伺服器發生錯誤";
                    result.result_content = null;
                }
            }
            else
            {
                result.result_status = false;
                result.result_message = "請輸入帳號和密碼";
                result.result_content = null;
            }

            return result;
        }
    }
}
