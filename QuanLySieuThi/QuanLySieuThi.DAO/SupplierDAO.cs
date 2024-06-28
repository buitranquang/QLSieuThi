using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi.DAO
{
    public class SupplierDAO
    {
        // Fields
        private QuanLySieuThiContext context;

        // Constructor
        public SupplierDAO()
        {
            this.context = new QuanLySieuThiContext();
        }
        public bool UpdateInfo(Supplier supplier, string name, string description)
        {
            try
            {
                supplier.Name = name;
                supplier.Description = description;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Create a new supplier
        public int Create(Supplier supplier)
        {
            try
            {
                context.Suppliers.Add(supplier);
                return context.SaveChanges();
            }
            catch { return 0; }
        }
        public List<Supplier> GetAllSuppliers()
        {
            return context.Suppliers.ToList();
        }
        // Read a supplier by ID
        public Supplier GetSupplierById(int supplierId)
        {
            return context.Suppliers.Find(supplierId);
        }

        // Read a supplier by name
        public Supplier GetSupplierByName(string Name)
        {
            return context.Suppliers.FirstOrDefault(s => s.Name == Name);
        }

        // Update an existing supplier
        public int Update(Supplier supplier)
        {
            context.Entry(supplier).State = EntityState.Modified;
            return context.SaveChanges();
        }

        // Delete an existing supplier by ID and return the number of rows affected
        public int Delete(int supplierId)
        {
            Supplier supplier = GetSupplierById(supplierId);
            context.Suppliers.Remove(supplier);
            return context.SaveChanges();
        }
    }
}
