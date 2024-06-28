using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLySieuThi.DAO;
using QuanLySieuThi.DTO;

namespace QuanLySieuThi.BUS
{
    public class CategoryBUS
    {
        private CategoryDAO categoryDAO;

        public CategoryBUS()
        {
            categoryDAO = new CategoryDAO(); // Khởi tạo đối tượng CategoryDAO để truy xuất dữ liệu từ bảng Categories
        }

        // Phương thức lấy danh sách các mục danh mục từ bảng Categories
        public List<Category> GetCategories()
        {
            return categoryDAO.GetCategories();
        }

        // Phương thức lấy một mục danh mục từ bảng Categories theo ID
        public Category GetCategoryById(int categoryId)
        {
            return categoryDAO.GetCategoryById(categoryId);
        }

        // Phương thức thêm một mục danh mục vào bảng Categories
        public void AddCategory(Category category)
        {
            categoryDAO.AddCategory(category);
        }

        // Phương thức sửa một mục danh mục trong bảng Categories
        public void UpdateCategory(Category category)
        {
            categoryDAO.UpdateCategory(category);
        }

        // Phương thức xoá một mục danh mục khỏi bảng Categories
        public bool DeleteCategory(int categoryId)
        {
            if (categoryDAO.DeleteCategory(categoryId))
                return true;
            else
                return false;
        }
        public bool UpdateInfo(Category category, string name, string description)
        {
            try
            {
                // Gọi phương thức UpdateInfo() trên đối tượng repository
                categoryDAO.UpdateInfo(category, name, description);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
