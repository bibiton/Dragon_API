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
    
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            this.SubjectFile = new HashSet<SubjectFile>();
            this.Options = new HashSet<Options>();
        }
    
        public int Subject_ID { get; set; }
        public Nullable<int> SubjectArea_ID { get; set; }
        public short SubjectType_ID { get; set; }
        public string Subject_Name { get; set; }
        public Nullable<int> Excel_idx { get; set; }
        public string Template { get; set; }
        public Nullable<int> CourseWord_ID { get; set; }
        public Nullable<int> CourseSentence_ID { get; set; }
        public Nullable<int> CourseGrammar_ID { get; set; }
    
        public virtual SubjectArea SubjectArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectFile> SubjectFile { get; set; }
        public virtual SubjectType SubjectType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Options> Options { get; set; }
    }
}
