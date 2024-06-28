using QuanLySieuThi.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLySieuThi.DAO
{
    public class CommentDAO
    {
        private readonly QuanLySieuThiContext context = new QuanLySieuThiContext();

        public CommentDAO()
        {
        }

        public List<Comment> GetAllComments()
        {
            return context.Comments.ToList();
        }

        public List<Comment> GetAllByProductID(int productID)
        {
            return context.Comments.Where(e => e.ProductID == productID).ToList();
        }

        public Comment GetCommentById(int id)
        {
            return context.Comments.FirstOrDefault(c => c.ID == id);
        }

        public void AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            Comment comment = context.Comments.FirstOrDefault(c => c.ID == id);
            context.Comments.Remove(comment);
            context.SaveChanges();
        }
    }
}
