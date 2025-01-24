namespace Controller
{
    partial class AdminLoginForm
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
            textBox_Login = new TextBox();
            textBox_Password = new TextBox();
            labelLogin = new Label();
            labelPassword = new Label();
            button_Login = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // textBox_Login
            // 
            textBox_Login.Location = new Point(116, 45);
            textBox_Login.Name = "textBox_Login";
            textBox_Login.Size = new Size(225, 27);
            textBox_Login.TabIndex = 0;
            // 
            // textBox_Password
            // 
            textBox_Password.Location = new Point(116, 78);
            textBox_Password.Name = "textBox_Password";
            textBox_Password.Size = new Size(225, 27);
            textBox_Password.TabIndex = 1;
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Location = new Point(24, 52);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(52, 20);
            labelLogin.TabIndex = 2;
            labelLogin.Text = "Логин";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(24, 85);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(62, 20);
            labelPassword.TabIndex = 3;
            labelPassword.Text = "Пароль";
            // 
            // button_Login
            // 
            button_Login.Location = new Point(116, 111);
            button_Login.Name = "button_Login";
            button_Login.Size = new Size(125, 29);
            button_Login.TabIndex = 4;
            button_Login.Text = "Войти";
            button_Login.UseVisualStyleBackColor = true;
            button_Login.Click += button_Login_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new Point(347, 77);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(658, 104);
            listBox1.TabIndex = 5;
            // 
            // AdminLoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1123, 406);
            Controls.Add(listBox1);
            Controls.Add(button_Login);
            Controls.Add(labelPassword);
            Controls.Add(labelLogin);
            Controls.Add(textBox_Password);
            Controls.Add(textBox_Login);
            Name = "AdminLoginForm";
            Text = "AdminLoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_Login;
        private TextBox textBox_Password;
        private Label labelLogin;
        private Label labelPassword;
        private Button button_Login;
        private ListBox listBox1;
    }
}