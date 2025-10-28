using NewsApp.Data;
using System;
using System.Collections.Generic;

namespace NewsApp.Controller
{
    public class CommentManager
    {
        public bool PostComment(Comment newComment)
        {
            // TODO: Lưu bình luận mới vào database
            Console.WriteLine($"Bình luận mới: {newComment.Content}");
            return true; // Giả sử thành công
        }

        public List<Comment> GetCommentsByArticleId(int articleId)
        {
            // TODO: Tải bình luận từ database
            // Dữ liệu giả lập:
            return new List<Comment>
            {
                new Comment { UserID = 1, Content = "Bình luận đầu tiên!", Timestamp = DateTime.Now },
                new Comment { UserID = 2, Content = "Bài viết hay quá.", Timestamp = DateTime.Now }
            };
        }
    }
}