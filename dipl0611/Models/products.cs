namespace dipl0611.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public products()
        {
            operation = new HashSet<operation>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(70)]
        public string name { get; set; }

        public decimal price { get; set; }

        public int? lowBorderOrder { get; set; }

        public int id_kontr { get; set; }

        public virtual kontragents kontragents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<operation> operation { get; set; }
    }
}
