using System;
using System.Data;
using System.Windows.Forms;
using NewsApp.Data;       
using NewsApp.Controller; 

namespace NewsApp.UI
{
    public partial class MainForm : Form
    {
        private User _currentUser; // Biến lưu thông tin người dùng

        // Hàm khởi tạo được cập nhật để nhận User từ LoginForm
        public MainForm(User loggedInUser)
        {
            InitializeComponent();
            _currentUser = loggedInUser; // Lưu người dùng

            // Kết nối các sự kiện
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.guna2Button1.Click += new System.EventHandler(this.btnPostArticle_Click); // Nút "Đăng bài"
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            this.dgvHeadlines.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeadlines_CellDoubleClick);
        }

        // Khi Form tải
        private void MainForm_Load(object? sender, EventArgs e)
        {
            this.Text = $"Home - Chào mừng, {_currentUser.FullName}"; // (Giả sử User có thuộc tính FullName)
            LoadCategories();
            LoadArticles();
        }

        // Xử lý nhấn nút "Tìm"
        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text;
            string category = cbCategories.SelectedItem.ToString();

            // TODO: Tải lại danh sách bài viết với bộ lọc
            MessageBox.Show($"Tìm kiếm: '{searchTerm}' trong danh mục '{category}'");
        }

        // Xử lý nhấn nút "Đăng bài" (guna2Button1)
        private void btnPostArticle_Click(object? sender, EventArgs e)
        {
            // Mở form đăng bài mới, truyền thông tin người dùng
            NewArticalForm postForm = new NewArticalForm(_currentUser);
            postForm.ShowDialog();

            LoadArticles(); // Tải lại danh sách bài báo sau khi đăng
        }

        // Xử lý nhấn nút "Profile"
        private void btnProfile_Click(object? sender, EventArgs e)
        {
            MessageBox.Show($"Mở Profile của {_currentUser.Username}");
        }

        // Xử lý nhấp đúp vào một bài báo
        private void dgvHeadlines_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không nhấp vào tiêu đề
            {
                try
                {
                    // (Giả sử cột đầu tiên (ẩn) của bạn tên là "ArticleID")
                    int articleId = Convert.ToInt32(dgvHeadlines.Rows[e.RowIndex].Cells["ArticleID"].Value);

                    // Mở form chi tiết bài báo
                    ArticleForm articleForm = new ArticleForm(articleId, _currentUser);
                    articleForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở bài báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ----- HÀM TẢI DỮ LIỆU -----

        private void LoadCategories()
        {
            cbCategories.Items.Clear();
            cbCategories.Items.Add("Tất cả");
            cbCategories.Items.Add("Thế giới");
            cbCategories.Items.Add("Thể thao");
            cbCategories.Items.Add("Công nghệ");
            cbCategories.Items.Add("Giải trí");
            cbCategories.SelectedIndex = 0;
        }

        private void LoadArticles(string category = "Tất cả", string searchTerm = "")
        {
            // Cấu hình DataGridView 
            if (dgvHeadlines.Columns.Count == 0)
            {
                dgvHeadlines.AutoGenerateColumns = false;
                dgvHeadlines.Columns.Add("ArticleID", "ID");
                dgvHeadlines.Columns.Add("Title", "Tiêu đề");
                dgvHeadlines.Columns.Add("AuthorName", "Tác giả");
                dgvHeadlines.Columns.Add("CategoryName", "Danh mục");

                dgvHeadlines.Columns["ArticleID"].Visible = false;
                dgvHeadlines.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvHeadlines.Rows.Clear();

            // Dữ liệu giả lập
            dgvHeadlines.Rows.Add(1, "Tin tức nóng hổi về AI!", "Admin", "Công nghệ");
            dgvHeadlines.Rows.Add(2, "Kết quả trận đấu tối qua", "Phóng viên", "Thể thao");
        }
    }
}