﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingStore1.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlineShoppingEntities : DbContext
    {
        public OnlineShoppingEntities()
            : base("name=OnlineShoppingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<T_Cart> T_Cart { get; set; }
        public virtual DbSet<T_CartStatus> T_CartStatus { get; set; }
        public virtual DbSet<T_Category> T_Category { get; set; }
        public virtual DbSet<T_MemberRole> T_MemberRole { get; set; }
        public virtual DbSet<T_Members> T_Members { get; set; }
        public virtual DbSet<T_Message> T_Message { get; set; }
        public virtual DbSet<T_Product> T_Product { get; set; }
        public virtual DbSet<T_Roles> T_Roles { get; set; }
        public virtual DbSet<T_ShippingDetails> T_ShippingDetails { get; set; }
        public virtual DbSet<T_SlideImage> T_SlideImage { get; set; }
    }
}
