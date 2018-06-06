﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DragonFighterEntities : DbContext
    {
        public DragonFighterEntities()
            : base("name=DragonFighterEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArenaTitle> ArenaTitle { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberLog> MemberLog { get; set; }
        public virtual DbSet<MemberRole> MemberRole { get; set; }
        public virtual DbSet<MemberRoleAttrs> MemberRoleAttrs { get; set; }
        public virtual DbSet<MemberRoleImg> MemberRoleImg { get; set; }
        public virtual DbSet<MemberRoleStatus> MemberRoleStatus { get; set; }
        public virtual DbSet<RoleAttrsDetail> RoleAttrsDetail { get; set; }
        public virtual DbSet<StoreType> StoreType { get; set; }
        public virtual DbSet<StoryPackage> StoryPackage { get; set; }
        public virtual DbSet<StoryStoreInfo> StoryStoreInfo { get; set; }
        public virtual DbSet<StoryTelling> StoryTelling { get; set; }
        public virtual DbSet<StoryTellingImg> StoryTellingImg { get; set; }
        public virtual DbSet<StroyMapping> StroyMapping { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectArea> SubjectArea { get; set; }
        public virtual DbSet<SubjectFile> SubjectFile { get; set; }
        public virtual DbSet<SubjectType> SubjectType { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<MemberRole_LvExp> MemberRole_LvExp { get; set; }
        public virtual DbSet<EquipmentBaseElement> EquipmentBaseElement { get; set; }
        public virtual DbSet<EquipmentLevel> EquipmentLevel { get; set; }
        public virtual DbSet<EquipmentMappingP2E> EquipmentMappingP2E { get; set; }
        public virtual DbSet<EquipmentPrototype> EquipmentPrototype { get; set; }
        public virtual DbSet<EquipmentPrototypeLevelRare> EquipmentPrototypeLevelRare { get; set; }
        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<EquipmentPrototypeLimit> EquipmentPrototypeLimit { get; set; }
    }
}
