using Dragon_Library.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon_Library.API
{
    /// <summary>
    /// 經驗值對應
    /// </summary>
    public class ExpModels
    {
        /// <summary>
        /// 等級
        /// </summary>
        public int Lv { get; set; }
        /// <summary>
        /// 所需經驗
        /// </summary>
        public int Exp { get; set; }
    }
}
