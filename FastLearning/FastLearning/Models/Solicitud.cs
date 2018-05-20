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

    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            this.SolicitudUsuario = new HashSet<SolicitudUsuario>();
        }

        public int id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        public string descripcion { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public int estado { get; set; }

        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudUsuario> SolicitudUsuario { get; set; }
    }
}
