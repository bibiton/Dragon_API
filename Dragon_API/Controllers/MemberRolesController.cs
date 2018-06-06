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
 /// 角色資料
 /// </summary>
     [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class MemberRolesController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<MemberRole> MemberRole_db = new GenericRepository<MemberRole>();
        GenericRepository<MemberRole_LvExp> MemberRole_LvExp_db = new GenericRepository<MemberRole_LvExp>();
        #endregion



        /// <summary>
        /// 取得帳號內角色
        /// </summary>
        ///  <param name="id">帳號編號</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<List<MemberRoleModels>>))]
        public ResultModel<List<MemberRoleModels>> GetMemberRole(int id)
        {
            #region 取得TOKEN內帳密
            IEnumerable<string> tokens;
            bool haveToken = Request.Headers.TryGetValues("Server_Token", out tokens);
            string token = tokens.First();
            string ServerTokenDecryptString = ConfigurationManager.AppSettings["ServerTokenDecryptString"];
            string tokenac = token.DESDecode(ServerTokenDecryptString).Split(',')[0];

            #endregion

            ResultModel<List<MemberRoleModels>> result = new ResultModel<List<MemberRoleModels>>();

            if (!ModelState.IsValid)
            {
                result.result_status = false;
                result.result_message = "资讯有误";
            }
            else
            {
                try
                {

                    result.result_status = true;
                    result.result_message = "";
                    List<MemberRole_LvExp> explist = MemberRole_LvExp_db.GetAll();
                    var rs = MemberRole_db.GetList(e => e.Member_ID==id && e.Member.Member_Account== tokenac).Select(e=> new MemberRoleModels {
                        CreateTime=e.CreateTime,
                        ArenaTitle_ID=e.ArenaTitle_ID,
                        MemberRoleImg_ID=e.MemberRoleImg_ID,
                        MemberRole_ArenaPoints=e.MemberRole_ArenaPoints,
                        MemberRole_Exp=e.MemberRole_Exp,
                        MemberRole_Gender=e.MemberRole_Gender,
                        MemberRole_Golds=e.MemberRole_Golds,
                        MemberRole_ID=e.MemberRole_ID,
                        MemberRole_Name=e.MemberRole_Name,
                        Member_ID=e.Member_ID,
                        MemberRole_Lv = explist.Last(f => f.Exp <= e.MemberRole_Exp).Lv
                }) ;
                   
                    StatusCalc.CalcAll(MemberRole_db.GetList(e => e.Member_ID == id && e.Member.Member_Account == tokenac).First());
                    result.result_content = rs.ToList() ;

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
        /// 新增帳號內角色
        /// </summary>
        ///  <param name="memberRole">角色創立資料</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<MemberRoleModels>))]
        public ResultModel<MemberRoleModels> PostMemberRole(MemberRoleModels memberRole)
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

            ResultModel<MemberRoleModels> result = new ResultModel<MemberRoleModels>();


            try
            {
                MemberRole memberRoleModels = new MemberRole();
                memberRoleModels.Member_ID = MemberInfo.Member_ID;
                memberRoleModels.CreateTime = DateTime.Now;
                memberRoleModels.ArenaTitle_ID = memberRole.ArenaTitle_ID;
                memberRoleModels.MemberRoleImg_ID = memberRole.MemberRoleImg_ID;
                memberRoleModels.MemberRole_ArenaPoints = memberRole.MemberRole_ArenaPoints;
                memberRoleModels.MemberRole_Exp = memberRole.MemberRole_Exp;
                memberRoleModels.MemberRole_Gender = memberRole.MemberRole_Gender;
                memberRoleModels.MemberRole_Golds = memberRole.MemberRole_Golds;
                memberRoleModels.MemberRole_ID = memberRole.MemberRole_ID;
                memberRoleModels.MemberRole_Name = memberRole.MemberRole_Name;

                MemberRole_db.Create(memberRoleModels);
                StatusCalc.CalcAll(MemberRole_db.GetList(e => e.Member_ID == MemberInfo.Member_ID && e.Member.Member_Account == tokenac).First());
                result.result_status = true;
                result.result_message = "創立角色成功";
                memberRole.MemberRole_Lv = MemberRole_LvExp_db.GetList(f => f.Exp <= memberRole.MemberRole_Exp).Last().Lv ;
                result.result_content = memberRole;

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