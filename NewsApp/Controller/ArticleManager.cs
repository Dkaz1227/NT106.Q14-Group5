using NewsApp.Data;
using System;

namespace NewsApp.Controller
{
    public class ArticleManager
    {
        public Article GetArticleById(int articleId)
        {
            // TODO: Lấy bài báo từ database
            return new Article
            {
                ArticleID = articleId,
                Title = "Đây là tiêu đề bài báo từ DB",
                Content = "Đây là nội dung đầy đủ của bài báo..."
            };
        }

        public bool CreateArticle(Article newArticle)
        {
            // TODO: Lưu bài báo mới vào database
            Console.WriteLine($"Đã đăng bài: {newArticle.Title}");
            return true; // Giả sử thành công
        }
    }
}