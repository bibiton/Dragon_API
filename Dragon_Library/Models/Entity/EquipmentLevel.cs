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
    
    public partial class EquipmentLevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipmentLevel()
        {
            this.EquipmentPrototypeLevelRare = new HashSet<EquipmentPrototypeLevelRare>();
        }
    
        public int EquipmentLevel_ID { get; set; }
        public int Level { get; set; }
        public Nullable<byte> AddAttr_Count { get; set; }
        public Nullable<short> AddAttrMax { get; set; }
        public Nullable<short> AddAttrMin { get; set; }
        public string LevelName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipmentPrototypeLevelRare> EquipmentPrototypeLevelRare { get; set; }
    }
}
