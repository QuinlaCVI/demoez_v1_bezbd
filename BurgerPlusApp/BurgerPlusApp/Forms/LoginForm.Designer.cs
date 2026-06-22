namespace BurgerPlusApp.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panelPuzzle = new System.Windows.Forms.Panel();
            this.puzzleControl = new BurgerPlusApp.Controls.PuzzleControl();
            this.lblPuzzleStatus = new System.Windows.Forms.Label();
            this.panelPuzzle.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLogin.Location = new System.Drawing.Point(30, 30);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(50, 19);
            this.lblLogin.TabIndex = 0;
            this.lblLogin.Text = "Логин:";
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLogin.Location = new System.Drawing.Point(30, 55);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(220, 25);
            this.txtLogin.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPassword.Location = new System.Drawing.Point(30, 95);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 19);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(30, 120);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(220, 25);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLogin.Location = new System.Drawing.Point(30, 170);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(220, 40);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // panelPuzzle
            // 
            this.panelPuzzle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPuzzle.Controls.Add(this.puzzleControl);
            this.panelPuzzle.Location = new System.Drawing.Point(290, 30);
            this.panelPuzzle.Name = "panelPuzzle";
            this.panelPuzzle.Size = new System.Drawing.Size(300, 300);
            this.panelPuzzle.TabIndex = 5;
            // 
            // puzzleControl
            // 
            this.puzzleControl.BackColor = System.Drawing.Color.LightGray;
            this.puzzleControl.Location = new System.Drawing.Point(-1, -1);
            this.puzzleControl.Name = "puzzleControl";
            this.puzzleControl.Size = new System.Drawing.Size(296, 292);
            this.puzzleControl.TabIndex = 0;
            // 
            // lblPuzzleStatus
            // 
            this.lblPuzzleStatus.AutoSize = true;
            this.lblPuzzleStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPuzzleStatus.Location = new System.Drawing.Point(290, 340);
            this.lblPuzzleStatus.Name = "lblPuzzleStatus";
            this.lblPuzzleStatus.Size = new System.Drawing.Size(102, 19);
            this.lblPuzzleStatus.TabIndex = 6;
            this.lblPuzzleStatus.Text = "Соберите пазл";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.lblPuzzleStatus);
            this.Controls.Add(this.panelPuzzle);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblLogin);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация - ООО Бургер плюс";
            this.panelPuzzle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // ==========================================
        // ПОЛЯ (все компоненты формы)
        // ==========================================

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panelPuzzle;
        private System.Windows.Forms.Label lblPuzzleStatus;
        private BurgerPlusApp.Controls.PuzzleControl puzzleControl;
    }
}