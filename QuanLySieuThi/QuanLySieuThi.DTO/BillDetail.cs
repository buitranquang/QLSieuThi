namespace QuanLySieuThi.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillDetail")]
    public partial class BillDetail
    {
        public BillDetail()
        {

        }
        public BillDetail(int Quantity, decimal Price, int ProductID, Product product)
        {
            this.Quantity = Quantity;
            this.Price = Price;
            this.ProductID = ProductID;
            this.Product = product;
        }

        public BillDetail(int ID, int Quality, decimal Price, int BillID)
        {
            this.ID = ID;
            this.Quantity = Quality;
            this.Price = Price;
            this.BillID = BillID;
        }
        public BillDetail(int id, int quality)
        {
            this.ID = id;
            this.Quantity = quality;
        }
        public int ID { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int ProductID { get; set; }

        public int BillID { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Product Product { get; set; }
    }
}
