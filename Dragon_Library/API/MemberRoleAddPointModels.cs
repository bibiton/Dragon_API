using Dragon_Library.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// 角色配點
    /// </summary>
    public class MemberRoleAddPointModels
    {
        /// <summary>角色編號</summary>
        public int MemberRole_ID { get; set; }
        /// <summary>體力</summary>
        public int Con { get; set; }
        /// <summary>力量</summary>
        public int Str { get; set; }
        /// <summary>強度</summary>
        public int Vit { get; set; }
        /// <summary>速度</summary>
        public int Spd { get; set; }
        /// <summary>魔法</summary>
        public int Int { get; set; }
      

    }
}
