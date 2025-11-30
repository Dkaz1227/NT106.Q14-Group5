using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Data
{
    internal class Article
    {
        public string ArticleID { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public byte[] TopicImage { get; set; } = new byte[0];
        public string Content { get; set; } = string.Empty;
        public string AuthorID { get; set; } = string.Empty;
        public DateTime CreateDay { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Active";
        public Article() { }
        public void CreateTable()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Articles')
                BEGIN
                    CREATE TABLE Articles
                    (
                        ID INT IDENTITY(1, 1) PRIMARY KEY,
                        ArticleID AS ('A' + CAST(Id AS VARCHAR(8))) PERSISTED,
                        Title NVARCHAR(100) NOT NULL,
                        TopicImage VARBINARY(MAX) NULL,
                        Content NVARCHAR(MAX) NOT NULL,
                        AuthorID VARCHAR(9) NOT NULL,
                        CreateDay DATETIME NOT NULL DEFAULT(GETDATE()),
                        Status VARCHAR(10) NOT NULL DEFAULT('Active'),
                            CHECK (Status IN ('Active', 'Unactive', 'Deleted')),

                        CONSTRAINT FK_Articles_Users
                            FOREIGN KEY (AuthorID)
                            REFERENCES Users(AccountID)
                    )
                END";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddArticle(Article article)
        {
            User user = new User();
            if (user.GetType(article.AuthorID) != "Writer" && user.GetType(article.AuthorID) != "Admin")
            {
                throw new Exception("Not authorized");
            }
            if (user.GetStatus(article.AuthorID) != "Active")
            {
                throw new Exception("Fail to create article");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                INSERT INTO Articles(Title, TopicImage, Content, AuthorID)
                VALUES (@Title, @TopicImage, @Content, @AuthorID)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", article.Title);
                    cmd.Parameters.AddWithValue("@TopicImage", article.TopicImage);
                    cmd.Parameters.AddWithValue("@Content", article.Content);
                    cmd.Parameters.AddWithValue("@AuthorID", article.AuthorID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to create article" + ex.Message);
                    }
                }
            }
        }
        public Article GetArticle(string articleID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                SELECT ArticleID, Title, TopicImage, Content, AuthorID, CreateDay, Status, View
                FROM Articles
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", articleID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user = new User();
                            if (reader["Status"].ToString() != "Active" || user.GetStatus(reader["AuthorID"].ToString()) != "Active")
                            {
                                throw new Exception("Fail to load article");
                            }
                            return new Article
                            {
                                ArticleID = reader["ArticleID"].ToString(),
                                Title = reader["Title"].ToString(),
                                TopicImage = reader["TopicImage"] != DBNull.Value ? (byte[])reader["TopicImage"] : null,
                                Content = reader["Content"].ToString(),
                                AuthorID = reader["AuthorID"].ToString(),
                                CreateDay = Convert.ToDateTime(reader["CreateDay"]),
                                Status = reader["Status"].ToString(),
                            };
                        }
                        else
                        {
                            throw new Exception("Not found");
                        }
                    }
                }
            }
        }
        public void SetArticle(string articleID, string performerID)
        {
            Article article = GetArticle(articleID);
            if (article.AuthorID != performerID)
            {
                throw new Exception("Not authorized");
            }
            if(article.Status != "Active")
            {
                throw new Exception("Fail to load article");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Articles
                SET Title = @Title,
                    TopicImage = @TopicImage,
                    Content = @Content
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", article.Title);
                    cmd.Parameters.AddWithValue("@TopicImage", article.TopicImage);
                    cmd.Parameters.AddWithValue("@Content", article.Content);
                    cmd.Parameters.AddWithValue("@ArticleID", article.ArticleID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to change article" + ex.Message);
                    }
                }
            }
        }
        public void ShowArticle(string articleID, string performerID)
        {
            Article article = GetArticle(articleID);
            if (article.AuthorID != performerID)
            {
                throw new Exception("Not authorized");
            }
            if (article.Status == "Deleted")
            {
                throw new Exception("Fail to load article");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Articles
                SET Status = 'Active'
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", article.ArticleID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to show article" + ex.Message);
                    }
                }
            }
        }
        public void HideArticle(string articleID, string performerID)
        {
            Article article = GetArticle(articleID);
            if (article.AuthorID != performerID)
            {
                throw new Exception("Not authorized");
            }
            if (article.Status == "Deleted")
            {
                throw new Exception("Fail to load article");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Articles
                SET Status = 'Unactive'
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", article.ArticleID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to hide article" + ex.Message);
                    }
                }
            }
        }
        public void DeleteArticle(string articleID, string performerID)
        {
            User user = new User();
            Article article = GetArticle(articleID);
            if (article.AuthorID != performerID && !(user.GetType(performerID) == "Admin"))
            {
                throw new UnauthorizedAccessException("Not authorized");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Articles
                SET Status = 'Deleted'
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", article.ArticleID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to delete article" + ex.Message);
                    }
                }
            }
        }
    }
}
