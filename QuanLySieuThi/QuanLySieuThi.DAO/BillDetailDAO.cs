using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.DAO
{
    public class BillDetailDAO
    {
        private readonly QuanLySieuThiContext context;
        public BillDetailDAO()
        {
            context = new QuanLySieuThiContext();
        }

        public List<BillDetail> GetAll()
        {
            return context.BillDetails.ToList();
        }
    }
}
