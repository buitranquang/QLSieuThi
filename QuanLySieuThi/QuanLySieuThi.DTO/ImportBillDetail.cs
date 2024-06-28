namespace QuanLySieuThi.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportBillDetail")]
    public partial class ImportBillDetail
    {
        public int ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int ImportBillID { get; set; }

        public int ProductID { get; set; }

        public virtual ImportBill ImportBill { get; set; }

        public virtual Product Product { get; set; }
    }
}
