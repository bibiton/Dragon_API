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
using System.Web.Mvc;
using Dragon_Library.API;
using Dragon_Library.Filter;
using Dragon_Library.Models.Entity;
using Dragon_Library.Repository;
using Helper.EncryptExtensions;
using Newtonsoft.Json.Linq;

namespace Dragon_API.Controllers
{
    /// <summary>
    /// 介紹圖文資料
    /// </summary>
    [CrossDomainFilter]
    [ValidServerTokenFilter]
    public class IntroduceController : ApiController
    {
        #region 資料庫連線物件
        GenericRepository<StroyMapping> StroyMapping_db = new GenericRepository<StroyMapping>();
        #endregion

        /// <summary>
        /// 取得介紹圖文資料
        /// </summary>
        /// <param name="StoryPackageID">劇情包編號(1:龍潭王國)</param>
        /// <param name="StoreName">場景名稱("BeginIntroduce":開頭介紹)</param> 
        /// <returns>
        /// </returns>
        [ResponseType(typeof(ResultModel<List<IntroduceModels>>))]
        public ResultModel<List<IntroduceModels>> GetIntroduce(int StoryPackageID,string StoreName)
        {
          
            ResultModel<List<IntroduceModels>> result = new ResultModel<List<IntroduceModels>>();
           
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
                    var rs = StroyMapping_db.GetList(e => e.StoryStoreInfo.StoryPackage_ID == StoryPackageID && e.StoryStoreInfo.Store_Name == StoreName).OrderBy(e => e.Sort).Select(
                        e => new IntroduceModels
                        {
                            StoryStoreInfo_ID = e.StoryStoreInfo.StoryStoreInfo_ID,
                            Store_Name = e.StoryStoreInfo.Store_Name,
                            StoreType = e.StoryStoreInfo.StoreType.StoreType_Name,
                            Content = e.StoryTelling == null ? String.Empty: e.StoryTelling.StoryTelling_Content,
                            Img= e.StoryTellingImg == null ? String.Empty : e.StoryTellingImg.StoryTellingImg_Url,
                            Sort= e.Sort,
                            Seconds = e.Seconds
                        }
                        );

                    result.result_content = rs.ToList();
                   
                }
                catch (Exception ex)
                {
                    result.result_status = false;
                    result.result_message = ex.Message;
                }
            }

            return result;
        }
       
    }
}