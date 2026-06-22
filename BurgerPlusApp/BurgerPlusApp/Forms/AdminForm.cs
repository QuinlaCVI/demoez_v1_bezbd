using BurgerPlusApp.Data;
using BurgerPlusApp.Models;
using System;
using System.Windows.Forms;

namespace BurgerPlusApp.Forms
{
    public partial class AdminForm : Form
    {
        private UserRepository _userRepo;

        public AdminForm(UserRepository userRepo)
        {
            InitializeComponent();
            _userRepo = userRepo;
            LoadUsers();

            // ПОДПИСЫВАЕМ КНОПКИ
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnUnblock.Click += BtnUnblock_Click;
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void LoadUsers()
        {
            var users = _userRepo.GetAll();
            dgvUsers.DataSource = null;
            dgvUsers.DataSource = users;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEditUserForm(_userRepo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = (User)dgvUsers.CurrentRow.DataBoundItem;
            var form = new AddEditUserForm(_userRepo, user);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void BtnUnblock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя для разблокировки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = (User)dgvUsers.CurrentRow.DataBoundItem;

            if (!user.IsBlocked)
            {
                MessageBox.Show("Пользователь не заблокирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Разблокировать пользователя '{user.Login}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _userRepo.Unblock(user.Id);
                LoadUsers();
                MessageBox.Show("Пользователь разблокирован", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}