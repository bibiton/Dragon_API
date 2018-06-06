using Dragon_Library.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// 帳號資料
    /// </summary>
    public class MemberModels
    {
        public int Member_ID { get; set; }
        public Nullable<int> Student_ID { get; set; }
        public string Member_Account { get; set; }
        public string Member_Password { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<int> Course_ID { get; set; }
        public string Student_Source { get; set; }
        public string IToken { get; set; }


    }
}
