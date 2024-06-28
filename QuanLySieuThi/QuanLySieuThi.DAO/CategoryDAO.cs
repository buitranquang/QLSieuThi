using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLySieuThi.DTO;

namespace QuanLySieuThi.DAO
{
    public class CategoryDAO
    {
        private QuanLySieuThiContext context;

        public CategoryDAO()
        {
            context = new QuanLySieuThiContext(); // Khởi tạo đối tượng DbContext để truy xuất cơ sở dữ liệu
        }

        // Phương thức lấy danh sách các mục danh mục từ bảng Categories
        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        // Phương thức lấy một mục danh mục từ bảng Categories theo ID
        public Category GetCategoryById(int categoryId)
        {
            return context.Categories.Find(categoryId);
        }

        // Phương thức thêm một mục danh mục vào bảng Categories
        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public bool UpdateInfo(Category category, string name, string description)
        {
            try
            {
                category.Name = name;
                category.Description = description;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Phương thức sửa một mục danh mục trong bảng Categories
        public void UpdateCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        // Phương thức xoá một mục danh mục khỏi bảng Categories
        public bool DeleteCategory(int categoryId)
        {
            try
            {
                Category category = context.Categories.Find(categoryId);
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
