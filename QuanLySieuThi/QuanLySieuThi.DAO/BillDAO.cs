using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.DAO
{
    public class BillDAO
    {
        private readonly QuanLySieuThiContext context;

        public BillDAO()
        {
            context = new QuanLySieuThiContext();
        }

        public int Create(Bill bill, List<BillDetail> billDetails)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Add bill to database
                    context.Bills.Add(bill);
                    context.SaveChanges();
                    ProductDAO productDAO = new ProductDAO();
                    // Assign ID to each BillDetail
                    foreach (var billDetail in billDetails)
                    {
                        billDetail.BillID = bill.ID;
                        if (billDetail.Quantity != null)
                        {
                            Product product = productDAO.GetProductById(billDetail.Product.ID);
                            product.UnitInStock -= (int)billDetail.Quantity;
                            productDAO.Update(product);
                        }
                        billDetail.Product = null;

                    }


                    // Add bill details to database
                    context.BillDetails.AddRange(billDetails);
                    context.SaveChanges();

                    transaction.Commit();
                    return bill.ID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Bill GetBillById(int id)
        {
            return context.Bills.FirstOrDefault(b => b.ID == id);
        }

        public List<Bill> GetAll()
        {
            return context.Bills.ToList();
        }

        public List<Bill> GetBillsByCustomerId(int customerId)
        {
            return context.Bills.Where(b => b.CustomerID == customerId).ToList();
        }

        public int Update(Bill bill)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Entry(bill).State = EntityState.Modified;
                    context.SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public int Delete(Bill bill)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Bills.Remove(bill);
                    context.SaveChanges();
                    transaction.Commit();
                    return 1;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

}
