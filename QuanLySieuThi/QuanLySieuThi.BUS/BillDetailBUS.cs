using QuanLySieuThi.DAO;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.BUS
{
    public class BillDetailBUS
    {

        private readonly BillDetailDAO billDetailDAO;

        public BillDetailBUS()
        {
           billDetailDAO = new BillDetailDAO();
        }
        public List<BillDetail> GetAll()
        {
            return billDetailDAO.GetAll();
        }
    }
}
