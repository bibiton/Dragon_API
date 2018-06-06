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
    public class MemberRoleModels
    {
        /// <summary>角色編號</summary>
        public int MemberRole_ID { get; set; }
        /// <summary>角色名稱</summary>
        public string MemberRole_Name { get; set; }
        /// <summary>角色性別</summary>
        public bool MemberRole_Gender { get; set; }
        /// <summary>角色創建時間</summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>金幣數</summary>
        public int MemberRole_Golds { get; set; }
        /// <summary>經驗值</summary>
        public int MemberRole_Exp { get; set; }
        /// <summary>對戰點</summary>
        public int MemberRole_ArenaPoints { get; set; }
        /// <summary>對戰頭銜</summary>
        public int ArenaTitle_ID { get; set; }
        /// <summary>帳號編號</summary>
        public int Member_ID { get; set; }
        /// <summary>角色頭像</summary>
        public int MemberRoleImg_ID { get; set; }

    }
}
