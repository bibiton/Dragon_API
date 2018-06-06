using Dragon_Library.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// 介紹圖文資料
    /// </summary>
    public class IntroduceModels
    { 
        /// <summary>場景編號</summary>
        public int StoryStoreInfo_ID { get; set; }
        
        /// <summary>場景名稱</summary>
        public string Store_Name { get; set; }
        /// <summary>場景類別</summary>
        public string StoreType { get; set; }
        
        /// <summary>場景文字</summary>
        public string Content { get; set; }
        /// <summary>場景圖片</summary>
        public string Img { get; set; }
        /// <summary>排序</summary>
        public int Sort { get; set; }
        /// <summary>過場秒數</summary>
        public int Seconds { get; set; }
    }
}
