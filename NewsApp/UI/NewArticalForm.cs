using System;
using System.Windows.Forms;
using NewsApp.Data;       
using NewsApp.Controller; 

namespace NewsApp.UI
{
    public partial class NewArticalForm : Form
    {
        private User _currentUser;

        public NewArticalForm(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Kết nối sự kiện 
            this.guna2Button1.Click += new System.EventHandler(this.btnPost_Click);
        }

        private void Form2_Load(object? sender, EventArgs e)
        {
            LoadCategories();
        }

        // Xử lý nhấn nút "Đăng bài" (guna2Button1)
        private void btnPost_Click(object? sender, EventArgs e)
        {
            string title = textBoxTitle.Text;
            string categoryName = guna2ComboBox1.SelectedItem.ToString();
            string content = guna2TextBox1.Text; // Đây là TextBox nội dung

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tiêu đề, chuyên mục và nội dung.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int categoryId = 1; // Giả lập

                Article newArticle = new Article
                {
                    Title = title,
                    Content = content,
                    CategoryID = categoryId,
                    AuthorID = _currentUser.UserID, 
                    PublishDate = DateTime.Now
                };

                ArticleManager articleManager = new ArticleManager();
                bool isSuccess = articleManager.CreateArticle(newArticle);

                if (isSuccess)
                {
                    MessageBox.Show("Đăng bài thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form
                }
                else
                {
                    MessageBox.Show("Đăng bài thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng bài: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void LoadCategories()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Thế giới");
            guna2ComboBox1.Items.Add("Thể thao");
            guna2ComboBox1.Items.Add("Công nghệ");
            guna2ComboBox1.Items.Add("Giải trí");
            guna2ComboBox1.SelectedIndex = 0;
        }
    }
}