using System;
using System.Windows.Forms;
using NewsApp.Data;       
using NewsApp.Controller; 

namespace NewsApp.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Kết nối các sự kiện Click 
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.linkLabelSignUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSignUp_LinkClicked);
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // Thiết lập cho ô mật khẩu để nó hiển thị dấu '*'
            tbPassword.PasswordChar = '*';
        }

        // Xử lý khi nhấn nút "Đăng nhập"
        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                UserManager userManager = new UserManager();
                // (Giả sử hàm Login trả về đối tượng User nếu thành công, và null nếu thất bại)
                User loggedInUser = userManager.Login(username, password);

                if (loggedInUser != null)
                {
                    // Đăng nhập thành công!
                    this.Hide(); // Ẩn form Login

                    // Mở MainForm và truyền thông tin người dùng vào
                    MainForm mainForm = new MainForm(loggedInUser);
                    mainForm.ShowDialog();

                    // Sau khi MainForm đóng (người dùng đăng xuất/thoát), đóng luôn Login
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // Xử lý khi nhấn link "Đăng kí"
        private void linkLabelSignUp_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); // Ẩn form Login

            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog(); // Mở form Đăng kí

            this.Show(); // Hiện lại form Login khi form Đăng kí đóng
        }
    }
}