namespace NewsApp.UI
{
    partial class ArticleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArticleForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lbTitle = new Label();
            splitContainer1 = new SplitContainer();
            textBoxContent = new Guna.UI2.WinForms.Guna2TextBox();
            flpComment = new FlowLayoutPanel();
            panelAddComment = new Guna.UI2.WinForms.Guna2Panel();
            textBoxtWriteComment = new Guna.UI2.WinForms.Guna2TextBox();
            btnPostComment = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelAddComment.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Dock = DockStyle.Top;
            lbTitle.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.ForeColor = Color.White;
            lbTitle.Location = new Point(0, 0);
            lbTitle.Name = "lbTitle";
            lbTitle.Padding = new Padding(10);
            lbTitle.Size = new Size(162, 70);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "Tựa đề";
            lbTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.White;
            splitContainer1.Location = new Point(28, 12);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(textBoxContent);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flpComment);
            splitContainer1.Panel2.Controls.Add(panelAddComment);
            splitContainer1.Size = new Size(1216, 572);
            splitContainer1.SplitterDistance = 381;
            splitContainer1.TabIndex = 1;
            // 
            // textBoxContent
            // 
            textBoxContent.BorderThickness = 0;
            textBoxContent.CustomizableEdges = customizableEdges1;
            textBoxContent.DefaultText = "";
            textBoxContent.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBoxContent.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBoxContent.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBoxContent.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBoxContent.Dock = DockStyle.Fill;
            textBoxContent.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBoxContent.Font = new Font("Segoe UI", 9F);
            textBoxContent.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBoxContent.Location = new Point(0, 0);
            textBoxContent.Margin = new Padding(6, 6, 6, 6);
            textBoxContent.Multiline = true;
            textBoxContent.Name = "textBoxContent";
            textBoxContent.PlaceholderText = "";
            textBoxContent.ReadOnly = true;
            textBoxContent.ScrollBars = ScrollBars.Vertical;
            textBoxContent.SelectedText = "";
            textBoxContent.ShadowDecoration.CustomizableEdges = customizableEdges2;
            textBoxContent.Size = new Size(1216, 381);
            textBoxContent.TabIndex = 0;
            // 
            // flpComment
            // 
            flpComment.AutoScroll = true;
            flpComment.Dock = DockStyle.Fill;
            flpComment.FlowDirection = FlowDirection.TopDown;
            flpComment.Location = new Point(0, 0);
            flpComment.Name = "flpComment";
            flpComment.Size = new Size(1216, 147);
            flpComment.TabIndex = 1;
            flpComment.WrapContents = false;
            // 
            // panelAddComment
            // 
            panelAddComment.Controls.Add(textBoxtWriteComment);
            panelAddComment.Controls.Add(btnPostComment);
            panelAddComment.CustomizableEdges = customizableEdges7;
            panelAddComment.Dock = DockStyle.Bottom;
            panelAddComment.Location = new Point(0, 147);
            panelAddComment.Name = "panelAddComment";
            panelAddComment.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelAddComment.Size = new Size(1216, 40);
            panelAddComment.TabIndex = 0;
            // 
            // textBoxtWriteComment
            // 
            textBoxtWriteComment.CustomizableEdges = customizableEdges3;
            textBoxtWriteComment.DefaultText = "";
            textBoxtWriteComment.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBoxtWriteComment.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBoxtWriteComment.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBoxtWriteComment.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBoxtWriteComment.Dock = DockStyle.Fill;
            textBoxtWriteComment.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBoxtWriteComment.Font = new Font("Segoe UI", 9F);
            textBoxtWriteComment.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBoxtWriteComment.Location = new Point(0, 0);
            textBoxtWriteComment.Margin = new Padding(6, 6, 6, 6);
            textBoxtWriteComment.Name = "textBoxtWriteComment";
            textBoxtWriteComment.PlaceholderText = "Viết bình luận ...";
            textBoxtWriteComment.SelectedText = "";
            textBoxtWriteComment.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textBoxtWriteComment.Size = new Size(1084, 40);
            textBoxtWriteComment.TabIndex = 1;
            // 
            // btnPostComment
            // 
            btnPostComment.CustomizableEdges = customizableEdges5;
            btnPostComment.DisabledState.BorderColor = Color.DarkGray;
            btnPostComment.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPostComment.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPostComment.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPostComment.Dock = DockStyle.Right;
            btnPostComment.Font = new Font("Segoe UI", 9F);
            btnPostComment.ForeColor = Color.White;
            btnPostComment.Image = (Image)resources.GetObject("btnPostComment.Image");
            btnPostComment.Location = new Point(1084, 0);
            btnPostComment.Name = "btnPostComment";
            btnPostComment.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnPostComment.Size = new Size(132, 40);
            btnPostComment.TabIndex = 0;
            btnPostComment.Text = "Gửi";
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = SystemColors.MenuBar;
            guna2Panel1.BorderRadius = 8;
            guna2Panel1.Controls.Add(splitContainer1);
            guna2Panel1.CustomizableEdges = customizableEdges9;
            guna2Panel1.Location = new Point(12, 96);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel1.Size = new Size(1276, 626);
            guna2Panel1.TabIndex = 2;
            // 
            // ArticleForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1300, 720);
            Controls.Add(lbTitle);
            Controls.Add(guna2Panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5);
            Name = "ArticleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Article";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelAddComment.ResumeLayout(false);
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbTitle;
        private SplitContainer splitContainer1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox textBoxContent;
        private Guna.UI2.WinForms.Guna2Panel panelAddComment;
        private Guna.UI2.WinForms.Guna2Button btnPostComment;
        private FlowLayoutPanel flpComment;
        private Guna.UI2.WinForms.Guna2TextBox textBoxtWriteComment;
    }
}