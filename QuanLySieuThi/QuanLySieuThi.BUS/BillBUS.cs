using QuanLySieuThi.DAO;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.BUS
{
    public class BillBUS
    {
        private readonly BillDAO billDAO;

        public BillBUS()
        {
            billDAO = new BillDAO();
        }

        public int Create(Bill bill, List<BillDetail> billDetails)
        {
            try
            {
                return billDAO.Create(bill, billDetails);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Bill GetBillById(int id)
        {
            return billDAO.GetBillById(id);
        }

        public List<Bill> GetAll()
        {
            return billDAO.GetAll();
        }

        public List<Bill> GetBillsByCustomerId(int customerId)
        {
            return billDAO.GetBillsByCustomerId(customerId);
        }

        public int Update(Bill bill)
        {
            try
            {
                billDAO.Update(bill);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(Bill bill)
        {
            try
            {
                billDAO.Delete(bill);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
