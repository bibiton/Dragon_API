using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    public class ServerToken
    {
        /// <summary>登入用帳號</summary>
        public string server_account { get; set; }

        /// <summary>登入用密碼</summary>
        public string server_password { get; set; }
    }

    /// <summary>
    /// ServerTokenResult
    /// </summary>
    public class ServerTokenResult
    { /// <summary>API資料回應狀態</summary>
        public bool result_status { get; set; }
        /// <summary>API資料回應信息</summary>
        public string result_message { get; set; }
        /// <summary>API資料回應內容</summary>
        public string result_content { get; set; }
        
    }
}
