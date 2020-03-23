namespace dipl0611.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class orderList
    {
        public int prodid { get; set; }

        public string name { get; set; }

        public int приход { get; set; }

        public int продажи { get; set; }

        public int остатки { get; set; }

        public int? lowBorderOrder { get; set; }

        //public int id_ttn { get; set; }

        //public int id_product { get; set; }

        //public int count { get; set; }

        //public virtual products products { get; set; }

        //public virtual TTN TTN { get; set; }

        //public IEnumerable<products> products7 { get; set; }


    }
}
