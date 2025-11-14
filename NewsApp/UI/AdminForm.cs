using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsApp.UI
{
    public partial class AdminForm: Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        // ban đầu textbox chuyên mục bị tắt, nút sửa, xóa, thêm cũng bị tắt
        // ở tab chuyên mục, nếu chọn chuyên mục ở datagridview thì hiện thông tin chuyên mục lên textbox
        // đồng thời bật nút sửa(sửa tên chuyên mục), xóa
        // chọn xóa thì xóa chuyên mục khỏi database và load lại datagridview
        // chọn sửa thì sửa tên chuyên mục trong database và load lại datagridview
        // nếu chọn nút thêm mới thì xóa textbox và tắt nút sửa, xóa
        // gõ tên chuyên mục vào textbox và chọn nút thêm thì thêm chuyên mục vào database và load lại datagridview
    }
}
