using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Data
{
    internal class DatabaseHelper
    {
        User user = new User();
        Article article = new Article();
        Comment comment = new Comment();
        public void InitializeDatabase()
        {
            user.CreateTable();
            article.CreateTable();
            comment.CreateTable();
        }
    }
}
