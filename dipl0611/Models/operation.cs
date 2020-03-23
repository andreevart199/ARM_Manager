namespace dipl0611.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("operation")]
    public partial class operation
    {
        public int id { get; set; }

        public int id_ttn { get; set; }

        public int id_product { get; set; }

        public int count { get; set; }

        public virtual products products { get; set; }

        public virtual TTN TTN { get; set; }
    }
}
