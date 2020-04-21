namespace dipl0611.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class kontragents
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kontragents()
        {
            products = new HashSet<products>();
            TTN = new HashSet<TTN>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string telephone { get; set; }

        [StringLength(100)]
        public string adress { get; set; }

        [StringLength(20)]
        public string kontact_name { get; set; }

        [StringLength(15)]
        public string kontact_telephone { get; set; }

        [Required]
        [StringLength(15)]
        public string dogovor { get; set; }

        public int? type_kontr_id { get; set; }

        public virtual type_kontr type_kontr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<products> products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<TTN> TTN { get; set; }
    }
}
