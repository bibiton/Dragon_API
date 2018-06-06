using Dragon_Library.Filter;
using Helper.EncryptExtensions;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Dragon_Library.API;
using System.Collections.Generic;
using System.Web.Http.Description;
using Dragon_Library.Repository;
using Dragon_Library.Models.Entity;

namespace Dragon_API.Controller
{
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class ExpController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<MemberRole_LvExp> MemberRole_LvExp_db = new GenericRepository<MemberRole_LvExp>();
        #endregion
        /// <summary>
        /// 取得ExpList
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<List<ExpModels>>))]
        public ResultModel<List<ExpModels>> GetExp()
        {
            ResultModel<List<ExpModels>> result = new ResultModel<List<ExpModels>>();
                try
                {
                    result.result_status = true;
                    result.result_message = "";
                    result.result_content = MemberRole_LvExp_db.GetAll().Select(e=>new ExpModels {
                        Lv=e.Lv,
                        Exp=e.Exp??0
                    }).ToList();

                }
                catch (Exception ex)
                {
                    result.result_status = false;
                    result.result_message = ex.Message;
                }
            

            return result;
        }
        /// <summary>
        /// 取得單一等級Exp
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<ExpModels>))]
        public ResultModel<ExpModels> GetExp(int id)
        {
            ResultModel<ExpModels> result = new ResultModel<ExpModels>();
            try
            {
                result.result_status = true;
                result.result_message = "";
                ExpModels expModels = new ExpModels();
                var s = MemberRole_LvExp_db.Get(e => e.Lv == id);
                expModels.Lv = s.Lv;
                expModels.Exp = s.Exp??0;
                result.result_content = expModels;

            }
            catch (Exception ex)
            {
                result.result_status = false;
                result.result_message = ex.Message;
            }


            return result;
        }
    }
}
