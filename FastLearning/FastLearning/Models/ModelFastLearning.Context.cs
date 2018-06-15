﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FastLearning.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FastLearningDBEntities : DbContext
    {
        public FastLearningDBEntities()
            : base("name=FastLearningDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<ClaseUsuario> ClaseUsuario { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Descuento> Descuento { get; set; }
        public virtual DbSet<DescuentoUsuario> DescuentoUsuario { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Habilidad> Habilidad { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<SolicitudUsuario> SolicitudUsuario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tema> Tema { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    
        public virtual ObjectResult<spDescuentoUsuarioPorFecha_Result> spDescuentoUsuarioPorFecha(Nullable<System.DateTime> fecha_min, Nullable<System.DateTime> fecha_max)
        {
            var fecha_minParameter = fecha_min.HasValue ?
                new ObjectParameter("fecha_min", fecha_min) :
                new ObjectParameter("fecha_min", typeof(System.DateTime));
    
            var fecha_maxParameter = fecha_max.HasValue ?
                new ObjectParameter("fecha_max", fecha_max) :
                new ObjectParameter("fecha_max", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spDescuentoUsuarioPorFecha_Result>("spDescuentoUsuarioPorFecha", fecha_minParameter, fecha_maxParameter);
        }
    }
}
