using System;
using System.Windows.Forms;
using NewsApp.Data;       
using NewsApp.Controller; 

namespace NewsApp.UI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            // Cài đặt
            tbPassword.PasswordChar = '*';
            tbConfirmPass.PasswordChar = '*';

            // Kết nối sự kiện
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.linkLabelLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogin_LinkClicked);
        }

        // Xử lý khi nhấn nút "Đăng kí"
        private void btnRegister_Click(object? sender, EventArgs e)
        {
            string fullName = tbFullName.Text;
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            string confirmPass = tbConfirmPass.Text;
            string email = tbEmail.Text;
            DateTime dateOfBirth = dtpickerBirth.Value;

            // 1. Kiểm tra
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường bắt buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Xử lý lưu
            try
            { 
                User newUser = new User
                {
                    FullName = fullName,
                    Username = username,
                    Password = password, 
                    Email = email,
                    DateOfBirth = dateOfBirth
                };

                UserManager userManager = new UserManager();
                bool isSuccess = userManager.RegisterUser(newUser);

                if (isSuccess)
                {
                    MessageBox.Show("Đăng kí thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form Đăng kí để quay lại form Đăng nhập
                }
                else
                {
                    MessageBox.Show("Đăng kí thất bại. Tên đăng nhập hoặc email có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng kí: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // Xử lý khi nhấn link "Đăng nhập"
        private void linkLabelLogin_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close(); // Đóng form này để quay lại form Đăng nhập
        }
    }
}