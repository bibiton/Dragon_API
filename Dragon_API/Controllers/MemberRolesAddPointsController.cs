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
using System.Web.Http.Description;
using Dragon_Library.API;
using Dragon_Library.Filter;
using Dragon_Library.Models.Entity;
using Dragon_Library.Repository;
using Helper.EncryptExtensions;
using Helper.StatusCalc;

namespace Dragon_API.Controllers
{/// <summary>
 /// 角色加點
 /// </summary>
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class MemberRolesAddPointsController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<MemberRole> MemberRole_db = new GenericRepository<MemberRole>();
        #endregion





        /// <summary>
        /// 角色加點
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<MemberRoleStatus>))]
        public ResultModel<MemberRoleStatus> PostMemberRolesAddPoints(MemberRoleAddPointModels memberRoleAddPoint)
        {
            #region 取得TOKEN內帳密
            IEnumerable<string> tokens;
            bool haveToken = Request.Headers.TryGetValues("Server_Token", out tokens);
            string token = tokens.First();
            string ServerTokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
            string tokenac = token.DESDecode(ServerTokenDecryptString).Split(',')[0];
            GenericRepository<Member> Member_db = new GenericRepository<Member>();
            Member MemberInfo = Member_db.Get(e => e.Member_Account == tokenac);
            #endregion

            ResultModel<MemberRoleStatus> result = new ResultModel<MemberRoleStatus>();


            try
            {
                MemberRole memberRoleModels = MemberRole_db.Get(e => e.MemberRole_ID == memberRoleAddPoint.MemberRole_ID && e.Member_ID == MemberInfo.Member_ID);
                memberRoleModels.Con = memberRoleAddPoint.Con;
                memberRoleModels.Int = memberRoleAddPoint.Int;
                memberRoleModels.Str = memberRoleAddPoint.Str;
                memberRoleModels.Vit = memberRoleAddPoint.Vit;
                memberRoleModels.Spd = memberRoleAddPoint.Spd;

                MemberRole_db = new GenericRepository<MemberRole>();
                MemberRole_db.Update(memberRoleModels);

                result.result_status = true;
                result.result_message = "創立角色成功";

                result.result_content = StatusCalc.CalcAll(MemberRole_db.GetList(e => e.Member_ID == MemberInfo.Member_ID && e.Member.Member_Account == tokenac).First());

            }
            catch (Exception ex)
            {
                result.result_status = false;
                result.result_message = ex.Message;
            }


            return result;
        }

        private bool MemberRoleExists(int id)
        {
            return MemberRole_db.Count(e => e.MemberRole_ID == id) > 0;
        }
    }
}