using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.DAO
{
    using QuanLySieuThi.DTO;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ImportBillDAO
    {
        private readonly QuanLySieuThiContext context;

        public ImportBillDAO()
        {
            context = new QuanLySieuThiContext();
        }

        public ImportBill GetImportBill(int id)
        {
            return context.ImportBills
                .Include(x => x.Supplier)
                .Include(x => x.ImportBillDetails)
                .SingleOrDefault(x => x.ID == id);
        }

        public List<ImportBill> GetByMonth(int year, int month)
        {
            return context.ImportBills
                .Include(x => x.Supplier)
                .Include(x => x.ImportBillDetails)
                .Where(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Year == year && x.CreatedDate.Value.Month == month)
                .ToList();
        }

        public List<ImportBill> GetByYear(int year)
        {
            return context.ImportBills
                .Include(x => x.Supplier)
                .Include(x => x.ImportBillDetails)
                .Where(x => x.CreatedDate.HasValue && x.CreatedDate.Value.Year == year)
                .ToList();
        }
        public int Delete(int evtID)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Event evt = context.Events.Find(evtID);
                    int res = 0;
                    List<EventDetail> edList = (List<EventDetail>)context.EventDetails.Where(ed => ed.EventID == evt.ID).ToList();
                    foreach (EventDetail ed in edList)
                    {
                        context.EventDetails.Remove(ed);
                    }

                    context.Events.Remove(evt);
                    res += context.SaveChanges();
                    transaction.Commit();
                    return res;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }
        public int Add(ImportBill importBill, List<ImportBillDetail> details)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Save import bill and details
                    context.ImportBills.Add(importBill);
                    context.SaveChanges();
                    ProductDAO productDAO = new ProductDAO();
                    // Assign ID to each BillDetail
                    foreach (var detail in details)
                    {
                        detail.ImportBillID = importBill.ID;
                        if (detail.Quantity != null)
                        {
                            Product product = productDAO.GetProductById(detail.Product.ID);
                            product.UnitInStock += (int)detail.Quantity;
                            productDAO.Update(product);
                            detail.ProductID = product.ID;
                            detail.Product = null;
                        }
                        detail.ImportBill = null;
                    }
                    // Add bill details to database
                    context.ImportBillDetails.AddRange(details);
                    context.SaveChanges();

                    transaction.Commit();
                    return importBill.ID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

    }

}
