//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dragon_Library.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseSentenceDetail
    {
        public long CourseSentenceDetail_ID { get; set; }
        public int Course_ID { get; set; }
        public int CourseSentence_ID { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual CourseSentence CourseSentence { get; set; }
    }
}
