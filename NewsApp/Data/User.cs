using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Data
{
    internal class User
    {
        public string AccountID { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountPassword { get; set; } = string.Empty;
        public DateTime AccountCreateDay { get; set; } = DateTime.Now;
        public string UserName { get; set; } = string.Empty;
        public byte[] Avatar { get; set; } = new byte[0];
        public string UserType {  get; set; } = "Reader";
        public User() { }
        public void CreateTable()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                BEGIN
                    CREATE TABLE Users
                    (
                        ID INT IDENTITY(1, 1) PRIMARY KEY,
                        AccountID AS ('U' + CAST(ID AS VARCHAR(8))) PERSISTED,
                        AccountName VARCHAR(100) PRIMARY KEY NOT NULL,
                        AccountPassword VARCHAR(100) NOT NULL,
                        AccountCreateDay DATETIME NOT NULL DEFAULT(GETDATE()),
                        AccountStatus VARCHAR(10) NOT NULL DEFAULT('Active'),
                            CHECK (AccountStatus IN ('Active', 'Unactive')),
                        UserName NVARCHAR(100) NOT NULL,
                        Avatar VARBINARY(MAX) NULL,
                        UserType VARCHAR(10) NOT NULL DEFAULT('Reader'),
                            CHECK (UserType IN ('Guest', 'Reader', 'Writer', 'Admin')),
                        UserStatus NVARCHAR(1000) NULL
                    )
                END";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddNewUser(User user)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string checkSql = "SELECT COUNT(*) FROM Users WHERE AccountName = @AccountName";
                using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Name", user.AccountName);
                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        throw new Exception("Account name already exists");
                    }
                }
                string insertSql = @"
                    INSERT INTO Users(AccountName, AccountPassword, UserName)
                    VALUES (@Name, @Password, @UserName)";
                using (SqlCommand cmd = new SqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", user.AccountName);
                    cmd.Parameters.AddWithValue("@Password", user.AccountPassword);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to create account: " + ex.Message);
                    }
                }
            }
        }
        public User GetUser(string accountID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                SELECT AccountID, AccountName, AccountPassword, AccountCreateDay, UserName, Avatar, UserType
                FROM Users
                WHERE AccountID = @AccountID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                AccountID = reader["AccountID"].ToString(),
                                AccountName = reader["AccountName"].ToString(),
                                AccountPassword = reader["AccountPassword"].ToString(),
                                AccountCreateDay = Convert.ToDateTime(reader["AccountCreateDay"]),
                                UserName = reader["UserName"].ToString(),
                                Avatar = reader["Avatar"] != DBNull.Value ? (byte[])reader["Avatar"] : null,
                                UserType = reader["UserType"].ToString()
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
        public void DeleteUser(string accountID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Users
                SET AccountStatus = 'Unactive'
                WHERE AccountID = @AccountID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to delete account" + ex.Message);
                    }
                }
            }
        }
        public void SetUserName(string accountID, string name)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Users
                SET UserName = @UserName
                WHERE AccountID = @AccountID AND AccountStatus = 'Active'";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", name);
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to change username" + ex.Message);
                    }
                }
            }
        }
        public void SetAvatar(string accountID, byte[] avatar)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Users
                SET Avatar = @Avatar
                WHERE AccountID = @AccountID AND AccountStatus = 'Active'";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Avatar", avatar);
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to change avatar" + ex.Message);
                    }
                }
            }
        }
        public void SetUserStatus(string accountID, string status)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                UPDATE Users
                SET UserStatus = @Status
                WHERE AccountID = @AccountID AND AccountStatus = 'Active'";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Fail to change status" + ex.Message);
                    }
                }
            }
        }
        public string GetStatus(string accountID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                SELECT AccountStatus
                FROM Users
                WHERE AccountID = @AccountID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    object result = cmd.ExecuteScalar();
                    return result.ToString();
                }
            }
        }
        public bool CheckLogin(string accountName, string password)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                SELECT AccountPassword
                FROM Users
                WHERE AccountName = @AccountName";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountName", accountName);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string dbPassword = result.ToString();
                        return dbPassword == password;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public string GetType(string ID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                    SELECT UserType
                    FROM Users
                    WHERE AccountID = @AccountID
                ";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", ID);
                    object result = cmd.ExecuteScalar();
                    return result.ToString();
                }
            }
        }
        public byte[] GetAvatar(string ID)
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            string sql = @"
                    SELECT Avatar
                    FROM Users
                    WHERE AccountID = @AccountID
                ";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@AccountID", ID);
                    object result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value) return null;
                    return (byte[])result;
                }
            }
        }
    }
}