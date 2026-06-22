namespace BurgerPlusApp.Forms
{
    partial class AdminForm
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
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnUnblock = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            // dgvUsers
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(700, 400);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.AutoGenerateColumns = false;
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.MultiSelect = false;

            // КОЛОНКИ ТАБЛИЦЫ
            this.dgvUsers.Columns.Add("Id", "ID");
            this.dgvUsers.Columns.Add("Login", "Логин");
            this.dgvUsers.Columns.Add("Role", "Роль");
            this.dgvUsers.Columns.Add("IsBlocked", "Заблокирован");

            // ПРИВЯЗКА К ПОЛЯМ КЛАССА USER
            this.dgvUsers.Columns["Id"].DataPropertyName = "Id";
            this.dgvUsers.Columns["Login"].DataPropertyName = "Login";
            this.dgvUsers.Columns["Role"].DataPropertyName = "Role";
            this.dgvUsers.Columns["IsBlocked"].DataPropertyName = "IsBlocked";

            // ШИРИНА КОЛОНОК
            this.dgvUsers.Columns["Id"].Width = 50;
            this.dgvUsers.Columns["Login"].Width = 150;
            this.dgvUsers.Columns["Role"].Width = 100;
            this.dgvUsers.Columns["IsBlocked"].Width = 100;

            // flowLayoutPanel1 (панель с кнопками внизу)
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnUnblock);
            this.flowLayoutPanel1.Controls.Add(this.btnRefresh);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 400);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 50);
            this.flowLayoutPanel1.TabIndex = 1;

            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;

            // btnEdit
            this.btnEdit.Location = new System.Drawing.Point(129, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;

            // btnUnblock
            this.btnUnblock.Location = new System.Drawing.Point(255, 3);
            this.btnUnblock.Name = "btnUnblock";
            this.btnUnblock.Size = new System.Drawing.Size(140, 40);
            this.btnUnblock.TabIndex = 2;
            this.btnUnblock.Text = "Снять блокировку";
            this.btnUnblock.UseVisualStyleBackColor = true;

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(401, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;

            // AdminForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Панель администратора - ООО Бургер плюс";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnUnblock;
        private System.Windows.Forms.Button btnRefresh;
    }
}