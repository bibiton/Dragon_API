using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Mvc;
using Dragon_Library.API;
using Dragon_Library.Filter;
using Dragon_Library.Models.Entity;
using Dragon_Library.Repository;
using Helper.EncryptExtensions;

namespace Dragon_API.Controllers
{
    /// <summary>
     /// 會員帳號
     /// </summary>
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class MembersController : ApiController
    {
      

        #region 資料庫連線物件
        GenericRepository<Member> member_db = new GenericRepository<Member>();
        GenericRepository<MemberLog> memberlog_db = new GenericRepository<MemberLog>();
        #endregion
        /// <summary>
        /// 註冊會員資料
        /// </summary>
        /// <param name="IToken"></param>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<object>))]
        private ResultModel<object> IMember(string IToken)
        {
            string ITokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
            ResultModel<object> result = new ResultModel<object>();

            string TokenData = IToken.Length > 0 ? IToken.DESDecode(ITokenDecryptString) : "";
            Member m = new Member();
            m.Student_ID = int.Parse(TokenData.Split(',')[0]);
            m.Member_Account = TokenData.Split(',')[1];
            DateTime VDate = DateTime.Parse(TokenData.Split(',')[2]);
            m.Student_Source = TokenData.Split(',')[3];
            m.CreateTime = DateTime.Now;
            Member memberinfo = member_db.Get(e => e.Member_Account == m.Member_Account && e.Student_ID == m.Student_ID);
            if (!ModelState.IsValid)
            {
                result.result_status = false;
                result.result_message = "资讯有误";
            }
            else if (memberinfo != null)
            {
                if (VDate < DateTime.Now)
                {
                    result.result_status = false;
                    result.result_message = "驗證過期";
                }
                else
                {
                    memberinfo.MemberLog = memberinfo.MemberLog.OrderByDescending(e => e.ActionTime).Take(5).ToList();
                    result.result_content = memberinfo;
                    result.result_status = true;
                    result.result_message = "";
                   
                    MemberLog memberLog = new MemberLog();
                    memberLog.Member_ID = memberinfo.Member_ID;
                    memberLog.Action = "Login";
                    memberLog.ActionTime = DateTime.Now;
                    memberlog_db.Create(memberLog);
                }

            }
            else
            {
                try
                {
                    member_db.Create(m);
                    result.result_status = true;
                    result.result_message = "注册成功";
                    Member ac = member_db.Get(e => e.Member_Account == m.Member_Account && e.Student_ID == m.Student_ID);
                    result.result_content = ac;
                    MemberLog memberLog = new MemberLog();
                    memberLog.Member_ID = ac.Member_ID;
                    memberLog.Action = "Create and Login";
                    memberLog.ActionTime = DateTime.Now;
                    memberlog_db.Create(memberLog);
                }
                catch (Exception ex)    
                {
                    result.result_status = false;
                    result.result_message = ex.Message;
                }
            }

            return result;
        }
        /// <summary>
        /// 註冊/取得會員資料
        /// </summary>
        /// <param name="member"></param>
        /// <returns>
        /// </returns>
        [ResponseType(typeof(ResultModel<object>))]
        public ResultModel<object> PostMember(Member member)
        {
            #region 取得TOKEN內帳密
            IEnumerable<string> tokens;
            bool haveToken = Request.Headers.TryGetValues("Server_Token", out tokens);
            string token = tokens.First();
            string ServerTokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
            string source = token.DESDecode(ServerTokenDecryptString).Split(',')[3];

            #endregion
            member.CreateTime = DateTime.Now;
            if (source!="App")
            {
                return IMember(token);
            }
            ResultModel<object> result = new ResultModel<object>();
           
            if (!ModelState.IsValid)
            {
                result.result_status = false;
                result.result_message = "注册资讯有误";
            }
            else if (member_db.Get(e => e.Member_Account == member.Member_Account) != null)
            {
                result.result_status = false;
                result.result_message = "帳號已被註冊";
                if (member_db.Get(e => e.Member_Password == member.Member_Password && e.Member_Account == member.Member_Account) == null)
                {
                    result.result_status = false;
                    result.result_message = "密碼錯誤";
                }
                else
                {
                    result.result_status = true;
                    result.result_message = "";
                    
                    Member ac = member_db.Get(e => e.Member_Password == member.Member_Password && e.Member_Account == member.Member_Account);
                    ac.MemberLog = ac.MemberLog.OrderByDescending(e => e.ActionTime).Take(5).ToList();
                   
                    string st = ac.Member_Account +","+ac.Member_Password+"," + DateTime.Now.AddHours(8).AddDays(1).ToString("yyyy/MM/dd HH:mm:ss") + ",App";
                    ac.IToken = st.DESEncode(ServerTokenDecryptString);
                    result.result_content = ac;
                    MemberLog memberLog = new MemberLog();
                    memberLog.Member_ID = ac.Member_ID;
                    memberLog.Action = "Login";
                    memberLog.ActionTime = DateTime.Now;
                    
                    memberlog_db.Create(memberLog);
                }
            }
            else
            {
                try
                {
                    member.Student_Source = "App";
                    member_db.Create(member);
                    result.result_status = true;
                    result.result_message = "注册成功";
                    Member ac = member_db.Get(e => e.Member_Password == member.Member_Password && e.Member_Account == member.Member_Account);
                    string st = ac.Member_Account + "," + ac.Member_Password + "," + DateTime.Now.AddHours(8).AddDays(1).ToString("yyyy/MM/dd HH:mm:ss") + ",App";
                    ac.IToken = st.DESEncode(ServerTokenDecryptString);
                    result.result_content = ac;
                    MemberLog memberLog = new MemberLog();
                    memberLog.Member_ID = ac.Member_ID;
                    memberLog.Action = "Create and Login";
                    memberLog.ActionTime = DateTime.Now;
                    memberlog_db.Create(memberLog);
                }
                catch (Exception ex)
                {
                    result.result_status = false;
                    result.result_message = ex.Message;
                }
            }

            return result;
        }
        ///// <summary>
        ///// 取得會員資料
        ///// </summary>
        ///// <param name="Member_Account"></param>
        ///// <param name="Member_Password"></param>
        ///// <returns></returns>

        //public ResultModel GetMember(string Member_Account,string Member_Password)
        //{
        //   int id = 0;
        //       ResultModel result = new ResultModel();

        //    if (member_db.Get(e => e.Member_Account == Member_Account)==null)
        //    {
        //        result.result_status = false;
        //        result.result_message = "帳號不存在";
        //    }
        //    else if (member_db.Get(e => e.Member_Password == Member_Password && e.Member_Account == Member_Account) == null)
        //    {
        //        result.result_status = false;
        //        result.result_message = "密碼錯誤";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            result.result_content = member_db.Get(e => e.Member_Password == Member_Password && e.Member_Account == Member_Account);
        //            result.result_status = true;
        //            result.result_message = "";
        //        }
        //        catch (Exception ex)
        //        {
        //            result.result_status = false;
        //            result.result_message = ex.Message;
        //        }
        //    }

        //    return result;
        //}
        


        private bool MemberExists(int id)
        {
            return member_db.Get(e => e.Member_ID == id) != null;
        }
    }
}