namespace Controller
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            button_show_checkedListBox_Category = new Button();
            button_Sort = new Button();
            button_Search = new Button();
            treeView_Category = new TreeView();
            listBox_Sort = new ListBox();
            flowLayoutPanel = new FlowLayoutPanel();
            button_ShowAdminLogin = new Button();
            SuspendLayout();
            // 
            // button_show_checkedListBox_Category
            // 
            button_show_checkedListBox_Category.BackColor = SystemColors.ActiveCaption;
            button_show_checkedListBox_Category.FlatStyle = FlatStyle.Popup;
            button_show_checkedListBox_Category.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button_show_checkedListBox_Category.Location = new Point(71, 12);
            button_show_checkedListBox_Category.Name = "button_show_checkedListBox_Category";
            button_show_checkedListBox_Category.Size = new Size(296, 49);
            button_show_checkedListBox_Category.TabIndex = 0;
            button_show_checkedListBox_Category.TabStop = false;
            button_show_checkedListBox_Category.Text = "Фильтры";
            button_show_checkedListBox_Category.UseVisualStyleBackColor = false;
            button_show_checkedListBox_Category.Click += button_show_checkedListBox_Category_Click;
            // 
            // button_Sort
            // 
            button_Sort.BackColor = SystemColors.ActiveCaption;
            button_Sort.FlatStyle = FlatStyle.Popup;
            button_Sort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button_Sort.Location = new Point(373, 12);
            button_Sort.Name = "button_Sort";
            button_Sort.Size = new Size(297, 49);
            button_Sort.TabIndex = 1;
            button_Sort.TabStop = false;
            button_Sort.Text = "Сортировать по";
            button_Sort.UseVisualStyleBackColor = false;
            button_Sort.Click += button_Sort_Click;
            // 
            // button_Search
            // 
            button_Search.BackColor = SystemColors.ActiveCaption;
            button_Search.FlatStyle = FlatStyle.Popup;
            button_Search.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button_Search.Location = new Point(676, 12);
            button_Search.Name = "button_Search";
            button_Search.Size = new Size(297, 49);
            button_Search.TabIndex = 2;
            button_Search.TabStop = false;
            button_Search.Text = "Показать";
            button_Search.UseVisualStyleBackColor = false;
            button_Search.Click += button_Search_Click;
            // 
            // treeView_Category
            // 
            treeView_Category.BackColor = SystemColors.InactiveCaption;
            treeView_Category.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            treeView_Category.Location = new Point(71, 67);
            treeView_Category.Name = "treeView_Category";
            treeView_Category.Size = new Size(297, 276);
            treeView_Category.TabIndex = 3;
            treeView_Category.Visible = false;
            treeView_Category.BeforeCheck += treeView_Category_BeforeCheck;
            treeView_Category.AfterCheck += treeView_Category_AfterCheck;
            treeView_Category.BeforeSelect += treeView_Category_BeforeSelect;
            // 
            // listBox_Sort
            // 
            listBox_Sort.BackColor = SystemColors.InactiveCaption;
            listBox_Sort.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            listBox_Sort.FormattingEnabled = true;
            listBox_Sort.ItemHeight = 31;
            listBox_Sort.Location = new Point(373, 67);
            listBox_Sort.Name = "listBox_Sort";
            listBox_Sort.Size = new Size(297, 35);
            listBox_Sort.TabIndex = 4;
            listBox_Sort.Visible = false;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.BackColor = Color.Transparent;
            flowLayoutPanel.Location = new Point(12, 488);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(1169, 125);
            flowLayoutPanel.TabIndex = 5;
            flowLayoutPanel.Visible = false;
            // 
            // button_ShowAdminLogin
            // 
            button_ShowAdminLogin.BackColor = Color.Transparent;
            button_ShowAdminLogin.BackgroundImage = Properties.Resources._1556978511_preview_cog;
            button_ShowAdminLogin.BackgroundImageLayout = ImageLayout.Zoom;
            button_ShowAdminLogin.FlatStyle = FlatStyle.Popup;
            button_ShowAdminLogin.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            button_ShowAdminLogin.Location = new Point(12, 12);
            button_ShowAdminLogin.Name = "button_ShowAdminLogin";
            button_ShowAdminLogin.Size = new Size(55, 49);
            button_ShowAdminLogin.TabIndex = 6;
            button_ShowAdminLogin.TabStop = false;
            button_ShowAdminLogin.UseVisualStyleBackColor = false;
            button_ShowAdminLogin.Click += button_ShowAdminLogin_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1193, 625);
            Controls.Add(button_ShowAdminLogin);
            Controls.Add(flowLayoutPanel);
            Controls.Add(listBox_Sort);
            Controls.Add(treeView_Category);
            Controls.Add(button_Search);
            Controls.Add(button_Sort);
            Controls.Add(button_show_checkedListBox_Category);
            DoubleBuffered = true;
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
        }

        #endregion

        private Button button_show_checkedListBox_Category;
        private Button button_Sort;
        private Button button_Search;
        private TreeView treeView_Category;
        private ListBox listBox_Sort;
        private FlowLayoutPanel flowLayoutPanel;
        private Button button_ShowAdminLogin;
    }
}