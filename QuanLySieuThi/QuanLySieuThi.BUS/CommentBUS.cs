using QuanLySieuThi.DAO;
using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLySieuThi.BUS
{
    public class CommentBUS
    {
        private readonly CommentDAO commentDAO;

        public CommentBUS()
        {
            commentDAO = new CommentDAO();
        }

        public List<Comment> GetAllComments()
        {
            return commentDAO.GetAllComments();
        }

        public List<Comment> GetCommentByProductId(int id)
        {
            return commentDAO.GetAllByProductID(id);
        }
        public Comment GetCommentById(int id)
        {
            return commentDAO.GetCommentById(id);
        }

        public void AddComment(Comment comment)
        {
            commentDAO.AddComment(comment);
        }

        public void UpdateComment(Comment comment)
        {
            commentDAO.UpdateComment(comment);
        }

        public void DeleteComment(int id)
        {
            commentDAO.DeleteComment(id);
        }
    }
}
