namespace dipl0611.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TTN")]
    public partial class TTN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTN()
        {
            operation = new HashSet<operation>();
        }

        public int id { get; set; }

        public DateTime date { get; set; }

        [Required]
        [StringLength(50)]
        public string nomer { get; set; }

        public int id_type { get; set; }

        public int id_kontr { get; set; }

        public virtual kontragents kontragents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<operation> operation { get; set; }

        public virtual type_TTN type_TTN { get; set; }
    }
}
