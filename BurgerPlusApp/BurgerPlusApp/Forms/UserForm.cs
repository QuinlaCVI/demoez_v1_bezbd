using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BurgerPlusApp.Models;

namespace BurgerPlusApp.Forms
{
    public partial class UserForm : Form
    {
        private User _user;

        public UserForm(User user)
        {
            InitializeComponent();
            _user = user;
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Панель пользователя - ООО Бургер плюс";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(400, 300);

            lblWelcome.Text = $"Добро пожаловать, {_user.Login}!";
            lblRole.Text = $"Ваша роль: {_user.Role}";
            lblInfo.Text = "Вы успешно авторизовались в системе.";

            btnLogout.Click += (s, e) =>
            {
                this.Close();
                Application.Restart();
            };
        }
    }
}