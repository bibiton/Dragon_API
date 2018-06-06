using Dragon_Library.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// 角色頭像
    /// </summary>
    public class MemberRoleImgModels
    { 
        /// <summary>頭像編號</summary>
        public int MemberRoleImg_ID { get; set; }
        /// <summary>頭像圖檔路徑</summary>
        public string MemberRoleImg_URL { get; set; }
       
    }
}
