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

namespace Dragon_API.Controllers
{/// <summary>
 /// 角色頭像資料
 /// </summary>
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class MemberRoleImgsController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<MemberRoleImg> MemberRoleImg_db = new GenericRepository<MemberRoleImg>();
        #endregion
        /// <summary>
        /// 取得所有頭像資料
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultModel<List<MemberRoleImgModels>>))]
        public ResultModel<List<MemberRoleImgModels>> GetMemberRoleImg()
        {
            
            ResultModel<List<MemberRoleImgModels>> result = new ResultModel<List<MemberRoleImgModels>>();

          
                try
                {

                    result.result_status = true;
                    result.result_message = "";
                    var rs = MemberRoleImg_db.GetAll().Select(e=>new MemberRoleImgModels {
                        MemberRoleImg_ID = e.MemberRoleImg_ID,
                        MemberRoleImg_URL = e.MemberRoleImg_URL
                    });

                    result.result_content = rs.ToList();

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