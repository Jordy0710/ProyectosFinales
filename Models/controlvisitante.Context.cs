﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class controlvisitanteEntities3 : DbContext
    {
        public controlvisitanteEntities3()
            : base("name=controlvisitanteEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Control_Visitante_Ocurrencias> Control_Visitante_Ocurrencias { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Revelante> Revelante { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vigilante> Vigilante { get; set; }
        public virtual DbSet<Visitante> Visitante { get; set; }
    }
}
