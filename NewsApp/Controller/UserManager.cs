using NewsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // <-- THÊM MỚI
using System.Text;                 // <-- THÊM MỚI

namespace NewsApp.Controller
{
    public class UserManager
    {
        // "Database" tạm thời
        private static List<User> _userDatabase = new List<User>();

        // ----- HÀM MÃ HÓA SHA-256 -----
        private static string ComputeSha256Hash(string rawData)
        {
            // Tạo một đối tượng SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển chuỗi input thành mảng byte và tính hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Chuyển mảng byte thành chuỗi hex (chữ thập lục phân)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // "x2" để format thành 2 ký tự hex
                }
                return builder.ToString();
            }
        }
        // ------------------------------------

        // Hàm Đăng kí (ĐÃ CẬP NHẬT)
        public bool RegisterUser(User newUser)
        {
            // 1. Kiểm tra username đã tồn tại chưa
            bool isUsernameTaken = _userDatabase.Any(u => u.Username == newUser.Username);

            if (isUsernameTaken)
            {
                return false; // Trả về false nếu trùng tên
            }

            // 2. MÃ HÓA mật khẩu trước khi lưu
            // newUser.Password đang là chữ thuần (plain text)
            // Chúng ta băm nó và lưu lại hash
            newUser.Password = ComputeSha256Hash(newUser.Password); // <-- CẬP NHẬT

            // 3. Thêm ID và lưu vào "database"
            newUser.UserID = _userDatabase.Count + 1;
            _userDatabase.Add(newUser);

            Console.WriteLine($"Đã đăng kí: {newUser.Username} (Hash: {newUser.Password})");
            return true;
        }

        // Hàm Đăng nhập (ĐÃ CẬP NHẬT)
        public User Login(string username, string password)
        {
            // 1. MÃ HÓA mật khẩu mà người dùng vừa nhập
            string hashedPassword = ComputeSha256Hash(password); // <-- CẬP NHẬT

            // 2. Tìm kiếm user có username VÀ HASH mật khẩu khớp
            User foundUser = _userDatabase.FirstOrDefault(u =>
                u.Username == username &&
                u.Password == hashedPassword // <-- CẬP NHẬT (so sánh hash với hash)
            );

            // 3. Trả về user nếu tìm thấy
            return foundUser;
        }

        public User GetUserById(int userId)
        {
            return _userDatabase.FirstOrDefault(u => u.UserID == userId);
        }
    }
}