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
    internal class Comment
    {
        public string CommentorID { get; set; } = string.Empty;
        public string ArticleID { get; set; } = string.Empty;
        public string CommentorName {  get; set; } = String.Empty;
        public byte[] CommentorAvatar { get; set; } = new byte[0];
        public string Content { get; set; } = string.Empty;
        public Comment() { }
        public void CreateTable()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Comments')
                BEGIN
                    CREATE TABLE Comments
                    (
                        ID INT IDENTITY(1, 1) PRIMARY KEY,
                        CommentID AS ('C' + CAST(Id AS VARCHAR(8))) PERSISTED,
                        CommentorID NVARCHAR(9) NOT NULL,
                        ArticleID NVARCHAR(9) NOT NULL, 
                        Content NVARCHAR(MAX) NOT NULL,
                        CreateDay DATETIME NOT NULL DEFAULT(GETDATE()),
                        Status VARCHAR(10) NOT NULL DEFAULT('Active'),
                            CHECK (Status IN ('Active', 'Unactive')),

                        CONSTRAINT FK_Comments_Users
                            FOREIGN KEY (CommentorID)
                            REFERENCES Users(AccountID),

                        CONSTRAINT FK_Comments_Articles
                            FOREIGN KEY (ArticleID)
                            REFERENCES Articles(ArticleID),
                    )
                END";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddComment(Comment comment)
        {
            User user = new User();
            user = user.GetUser(comment.CommentorID);
            if (user.UserType == "Guest")
            {
                throw new Exception("Not authorized");
            }
            if (user.GetStatus(CommentorID) != "Active")
            {
                throw new Exception("Fail to load account");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                INSERT INTO Comments(CommentorID, ArticleID, Content)
                VALUES (@CommentorID, @ArticleID, @Content)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentorID", comment.CommentorID);
                    cmd.Parameters.AddWithValue("@ArticleID", comment.ArticleID);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to comment" + ex.Message);
                    }
                }
            }
        }
        public string GetCommentor(string commentID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                    SELECT CommentorID
                    FROM Comments
                    WHERE CommentID = @CommentID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", commentID);
                    object result = cmd.ExecuteScalar();
                    return result.ToString();
                }
            }
        }
        public List<Comment> GetComments(string articleID)
        {
            List<Comment> comments = new List<Comment>();
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                SELECT CommentorID, Content
                FROM Comments
                WHERE ArticleID = @ArticleID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ArticleID", articleID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user = user.GetUser(reader["CommentorID"].ToString());
                            Comment comment = new Comment
                            {
                                CommentorID = user.AccountID,
                                CommentorName = user.UserName,
                                CommentorAvatar = user.Avatar,
                                Content = reader["Content"].ToString(),
                            };
                            comments.Add(comment);
                        }
                    }
                }
                return comments;
            }
        }
        public void DeleteComment(string commentID, string performerID)
        {
            User user = new User();
            if (user.GetType(performerID) != "Admin" && GetCommentor(commentID) != performerID)
            {
                throw new Exception("Not authorized");
            }
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                DELETE FROM Comments
                WHERE CommentID = @CommentID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CommentID", commentID);
                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            throw new Exception("Comment not found");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to delete comment: " + ex.Message);
                    }
                }
            }
        }
    }
}
