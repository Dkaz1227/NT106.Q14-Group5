using System;

namespace NewsApp.Data
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryID { get; set; }
        public int AuthorID { get; set; }
        public DateTime PublishDate { get; set; }
    }
}