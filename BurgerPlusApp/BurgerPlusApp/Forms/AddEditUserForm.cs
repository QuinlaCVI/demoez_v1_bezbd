using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BurgerPlusApp.Data;
using BurgerPlusApp.Models;

namespace BurgerPlusApp.Forms
{
    public partial class AddEditUserForm : Form
    {
        private UserRepository _userRepo;
        private User _editUser;
        private bool _isEditMode;

        public AddEditUserForm(UserRepository userRepo, User editUser = null)
        {
            InitializeComponent();
            _userRepo = userRepo;
            _editUser = editUser;
            _isEditMode = editUser != null;
            SetupForm();

            if (_isEditMode)
            {
                LoadUserData();
            }
        }

        private void SetupForm()
        {
            this.Text = _isEditMode ? "Редактирование пользователя" : "Добавление пользователя";
            this.StartPosition = FormStartPosition.CenterParent;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            cmbRole.Items.Clear();
            cmbRole.Items.Add("admin");
            cmbRole.Items.Add("user");

            if (!_isEditMode)
            {
                chkBlocked.Visible = false;
            }
        }

        private void LoadUserData()
        {
            txtLogin.Text = _editUser.Login;
            cmbRole.SelectedItem = _editUser.Role;
            chkBlocked.Checked = _editUser.IsBlocked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Проверка логина
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Логин обязателен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка на существующий логин (только при добавлении)
            if (!_isEditMode && _userRepo.LoginExists(txtLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка пароля (только при добавлении)
            if (!_isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Пароль обязателен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка роли
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = new User();

            if (_isEditMode)
            {
                user.Id = _editUser.Id;
                user.Login = txtLogin.Text;
                user.Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? _editUser.Password : txtPassword.Text;
                user.Role = cmbRole.SelectedItem.ToString();
                user.IsBlocked = chkBlocked.Checked;
                _userRepo.Update(user);
            }
            else
            {
                user.Login = txtLogin.Text;
                user.Password = txtPassword.Text;
                user.Role = cmbRole.SelectedItem.ToString();
                user.IsBlocked = false;
                _userRepo.Add(user);
            }

            MessageBox.Show("Пользователь сохранён", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}