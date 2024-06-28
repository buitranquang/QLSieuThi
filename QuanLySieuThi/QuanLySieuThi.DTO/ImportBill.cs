namespace QuanLySieuThi.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportBill")]
    public partial class ImportBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImportBill()
        {
            ImportBillDetails = new HashSet<ImportBillDetail>();
        }

        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? SupplierID { get; set; }

        public int? EmployeeID { get; set; }

        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBillDetail> ImportBillDetails { get; set; }
    }
}
