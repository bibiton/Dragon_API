using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// API回傳結果物件
    /// </summary>
    public class ResultModel<T>
    {
        /// <summary>API資料回應狀態</summary>
        public bool result_status { get; set; }
        /// <summary>API資料回應信息</summary>
        public string result_message { get; set; }
        /// <summary>API資料回應內容 (詳見右方模組資訊)</summary>
        public T result_content { get; set; }
    }
}
