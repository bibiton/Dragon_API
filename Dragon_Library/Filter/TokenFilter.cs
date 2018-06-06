using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;
using Helper.EncryptExtensions;
using System.Configuration;
using Dragon_Library.API;

namespace Dragon_Library.Filter
{
    /// <summary>
    /// 驗證Server Token
    /// </summary>
    public class ValidServerTokenFilterAttribute : ActionFilterAttribute
    {
        string ServerTokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ServerTokenResult result = new ServerTokenResult();

            //從Header 取得Server Token內容
            IEnumerable<string> tokens;
            bool haveToken = filterContext.Request.Headers.TryGetValues("Server_Token", out tokens);

            if (haveToken)
            {
                string token = tokens.First();
               
                DateTime Server_Token = token.Length > 0 ? DateTime.Parse(token.DESDecode(ServerTokenDecryptString).Split(',')[2]) : DateTime.Now.AddMinutes(-1);
                //DateTime Server_Token = token.Length > 0 ? DateTime.Parse(EncryptExtensions.RSADecrypt(token, "Melody")) : DateTime.Now.AddMinutes(-1);

                //驗證Server Token是否有效,若已失效則導向ValidToken/TokenInvalid 處理
                if (Server_Token < DateTime.Now)
                {
                    result.result_status = false;
                    result.result_content = null;
                    result.result_message = "API Key 已失效";
                    filterContext.Response = filterContext.Request.CreateResponse(result);
                }
            }
            else
            {
                result.result_status = false;
                result.result_content = null;
                result.result_message = "驗證失敗";
                filterContext.Response = filterContext.Request.CreateResponse(result);
            }
        }
    }

    ///// <summary>
    ///// 驗證Access Token
    ///// </summary>
    //public class ValidAccessTokenFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(HttpActionContext filterContext)
    //    {
    //        base.OnActionExecuting(filterContext);

    //        //從Header 取得Server Token內容
    //        string access_token = ((string[])filterContext.Request.Headers.GetValues("Access_Token"))[0];
    //        DateTime Access_Token = DateTime.Now.AddMinutes(-1);
    //        if (access_token.Length > 0)
    //        {
    //            access_token = EncryptExtensions.RSADecrypt(access_token, "Newimag");
    //            string[] access_tokenArray = access_token.Split(',');
    //            Access_Token = access_tokenArray[0].Length > 0 ? DateTime.Parse(access_tokenArray[0]) : DateTime.Now.AddMinutes(-1);

    //            //string MemberNo = access_tokenArray[1];
    //            //filterContext.ActionArguments["MemberNo"] = MemberNo;
    //        }

    //        //驗證Server Token是否有效,若已失效則導向ValidToken/TokenInvalid 處理
    //        if (Access_Token < DateTime.Now)
    //        {
    //            ServerTokenResult result = new ServerTokenResult();
    //            result.result_status = false;
    //            result.result_content = null;
    //            result.result_message = "A008-會員Token驗證失敗";
    //            filterContext.Response = filterContext.Request.CreateResponse(result);
    //        }
    //    }
    //}
}