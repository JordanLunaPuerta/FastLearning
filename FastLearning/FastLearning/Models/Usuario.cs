//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.ClaseUsuario = new HashSet<ClaseUsuario>();
            this.DescuentoUsuario = new HashSet<DescuentoUsuario>();
            this.Habilidad = new HashSet<Habilidad>();
            this.Solicitud = new HashSet<Solicitud>();
            this.SolicitudUsuario = new HashSet<SolicitudUsuario>();
        }

        public int id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombres { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string foto { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese el formato correcto")]
        public string email { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string contrasena { get; set; }
        [Compare("contrasena")]
        public string confirmar_contrasena { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public int rol { get; set; }
        public Nullable<int> calificacion_alumno { get; set; }
        public Nullable<int> calificacion_profesor { get; set; }
        public int puntos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaseUsuario> ClaseUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DescuentoUsuario> DescuentoUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habilidad> Habilidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudUsuario> SolicitudUsuario { get; set; }
    }
}
