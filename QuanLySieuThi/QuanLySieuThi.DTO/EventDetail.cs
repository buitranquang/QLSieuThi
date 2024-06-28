namespace QuanLySieuThi.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventDetail")]
    public partial class EventDetail
    {
        public int ID { get; set; }

        public int EventID { get; set; }

        public int ProductID { get; set; }

        public decimal? DiscountPrice { get; set; }

        public virtual Event Event { get; set; }

        public virtual Product Product { get; set; }
    }
}
