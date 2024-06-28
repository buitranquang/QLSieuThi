using QuanLySieuThi.DAO;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.BUS
{
    public class ImportBillBUS
    {
        private ImportBillDAO importBillDAO;

        public ImportBillBUS()
        {
            importBillDAO = new ImportBillDAO();
        }
        public int Delete(int evtID)
        {
            return importBillDAO.Delete(evtID);
        }
        public int Add(ImportBill importBill, List<ImportBillDetail> importBillDetails)
        {
            return importBillDAO.Add(importBill, importBillDetails);
        }
    }
}
