//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Charts.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dim_StatusMarital
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dim_StatusMarital()
        {
            this.TF_Absenteisme = new HashSet<TF_Absenteisme>();
        }
    
        public int id_marital { get; set; }
        public string MaritalDesc { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TF_Absenteisme> TF_Absenteisme { get; set; }
    }
}