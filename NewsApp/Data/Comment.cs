using System;

namespace NewsApp.Data
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}