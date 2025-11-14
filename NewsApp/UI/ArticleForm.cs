using System;
using System.Drawing;
using System.Windows.Forms;
using NewsApp.Data;      
using NewsApp.Controller; 

namespace NewsApp.UI
{
    public partial class ArticleForm : Form
    {
        private int _articleId;
        private User _currentUser;

        // Hàm khởi tạo nhận ID bài báo và người dùng
        public ArticleForm(int articleId, User currentUser)
        {
            InitializeComponent();
            _articleId = articleId;
            _currentUser = currentUser;

            // Kết nối sự kiện
            this.Load += new System.EventHandler(this.ArticleForm_Load);
            this.btnPostComment.Click += new System.EventHandler(this.btnPostComment_Click);
        }

        // Khi form tải
        private void ArticleForm_Load(object? sender, EventArgs e)
        {
            LoadArticleDetails();
            LoadComments();
        }

        // Xử lý nhấn nút "Gửi" bình luận
        private void btnPostComment_Click(object? sender, EventArgs e)
        {
            string commentContent = textBoxtWriteComment.Text;

            if (string.IsNullOrWhiteSpace(commentContent))
            {
                MessageBox.Show("Vui lòng nhập nội dung bình luận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // (Giả sử class Comment có các thuộc tính này)
                Comment newComment = new Comment
                {
                    ArticleID = _articleId,
                    UserID = _currentUser.UserID, // (Giả sử User có UserID)
                    Content = commentContent,
                    Timestamp = DateTime.Now
                };

                CommentManager commentManager = new CommentManager();
                // (Giả sử có hàm PostComment)
                bool isSuccess = commentManager.PostComment(newComment);

                if (isSuccess)
                {
                    textBoxtWriteComment.Clear(); // Xóa text
                    LoadComments(); // Tải lại bình luận
                }
                else
                {
                    MessageBox.Show("Đăng bình luận thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng bình luận: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        // ----- HÀM TẢI DỮ LIỆU -----

        private void LoadArticleDetails()
        {
            try
            {
                // Dữ liệu giả lập
                Article article = new Article { Title = "Tiêu đề bài báo mẫu", Content = "Đây là nội dung chi tiết của bài báo...\r\nNội dung có thể rất dài." };

                if (article != null)
                {
                    lbTitle.Text = article.Title;
                    textBoxContent.Text = article.Content;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bài báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadComments()
        {
            flpComment.Controls.Clear(); // Xóa bình luận cũ

            try
            {
                // Dữ liệu giả lập
                var commentList = new[] {
                    new { AuthorName = "Người dùng A", Content = "Bình luận đầu tiên!" },
                    new { AuthorName = "Người dùng B", Content = "Bài viết hay quá." }
                };

                foreach (var comment in commentList)
                {

                    // Tạo control động cho mỗi bình luận
                    Label lblAuthor = new Label();
                    lblAuthor.Text = $"{comment.AuthorName}:";
                    lblAuthor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    lblAuthor.AutoSize = true;
                    lblAuthor.Margin = new Padding(5, 5, 5, 0);

                    Label lblContent = new Label();
                    lblContent.Text = comment.Content;
                    lblContent.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                    lblContent.AutoSize = true;
                    lblContent.Margin = new Padding(15, 2, 5, 10);
                    lblContent.MaximumSize = new Size(flpComment.Width - 40, 0);

                    flpComment.Controls.Add(lblAuthor);
                    flpComment.Controls.Add(lblContent);
                }
            }
            catch (Exception ex)
            {
                flpComment.Controls.Add(new Label { Text = "Lỗi tải bình luận: " + ex.Message, ForeColor = Color.Red, AutoSize = true });
            }
        }
    }
}