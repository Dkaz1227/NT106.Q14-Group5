using System;

namespace NewsApp.Data
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // (Đây là mật khẩu chưa mã hóa)
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}