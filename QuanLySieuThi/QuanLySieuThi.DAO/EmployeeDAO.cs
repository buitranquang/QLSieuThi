using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
namespace QuanLySieuThi.DAO
{
    public class EmployeeDAO
    {
        private QuanLySieuThiContext db;

        public EmployeeDAO()
        {
            db = new QuanLySieuThiContext();
        }
        
        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        public Employee ViewDetail(int id)
        {
            return db.Employees.Find(id);
        }

        public List<Employee> GetAllEmployees()
        {
            return db.Employees.ToList();
        }
        
        public Employee GetEmployeeById(int id)
        {
            return db.Employees.Find(id);
        }
        public bool UpdateInfo(Employee employee, string name, string phone, string address, string role)
        {
            try
            {
                employee.Name = name;
                employee.Phone = phone;
                employee.Address = address;
                employee.Role = role;
                db.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Employee Authenticate(string username, string password)
        {
            return db.Employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
        }

        public int UpdateEmployee(Employee employee)
        {
            try
            {
                var existingEmployee = db.Employees.Find(employee.ID);

                if (existingEmployee != null)
                {
                    // Update existing employee entity properties
                    existingEmployee.Name = employee.Name;
                    existingEmployee.Address = employee.Address;
                    existingEmployee.Phone = employee.Phone;
                    existingEmployee.UserName = employee.UserName;
                    existingEmployee.Password = employee.Password;
                    existingEmployee.Role = employee.Role;

                    // Save changes to the database
                    return db.SaveChanges();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public int AddEmployee(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                return db.SaveChanges();
            }
            catch { return 0; }
        }
        public void Create(Employee employee)
        {
            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the validation errors
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the error messages together
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Throw a new exception with the full error message
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public int DeleteEmployee(int id)
        {
            var employee = db.Employees.Find(id);

            if (employee != null)
            {
                db.Employees.Remove(employee);
                return db.SaveChanges();
            }
            return 0;
        }

        public bool IsAuthorized(int id, string role)
        {
            var employee = db.Employees.Find(id);
            return employee.Role == role;
        }

        public Employee GetByUsername(string username)
        {
            return db.Employees.FirstOrDefault(e => e.UserName == username);
        }
    }

}
