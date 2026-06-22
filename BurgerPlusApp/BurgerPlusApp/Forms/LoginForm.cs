using BurgerPlusApp.Data;
using BurgerPlusApp.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BurgerPlusApp.Forms
{
    public partial class LoginForm : Form
    {
        private UserRepository _userRepo = new UserRepository();
        private int _failedAttempts = 0;
        private bool _puzzleSolved = false;

        public LoginForm()
        {
            InitializeComponent();

            // ПОДПИСЫВАЕМСЯ НА СОБЫТИЕ ПАЗЛА
            puzzleControl.ПазлСобран += PuzzleControl_PuzzleSolved;

            SetupForm();
        }

        private void SetupForm()
        {
            btnLogin.Click += BtnLogin_Click;
            this.Text = "Авторизация - ООО Бургер плюс";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // Проверка заполнения полей
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Поля 'Логин' и 'Пароль' обязательны для заполнения",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Проверка пазла
            if (!_puzzleSolved)
            {
                MessageBox.Show(
                    "Соберите пазл для продолжения",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Поиск пользователя
            User user = _userRepo.GetByLogin(txtLogin.Text);

            if (user == null)
            {
                _failedAttempts++;
                MessageBox.Show(
                    "Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                if (_failedAttempts >= 3)
                {
                    MessageBox.Show(
                        "Вы заблокированы. Обратитесь к администратору",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    btnLogin.Enabled = false;
                }
                return;
            }

            // Проверка блокировки
            if (user.IsBlocked)
            {
                MessageBox.Show(
                    "Вы заблокированы. Обратитесь к администратору",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Проверка пароля
            if (user.Password != txtPassword.Text)
            {
                _failedAttempts++;
                MessageBox.Show(
                    "Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                if (_failedAttempts >= 3)
                {
                    _userRepo.Block(user.Id);
                    MessageBox.Show(
                        "Вы заблокированы. Обратитесь к администратору",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    btnLogin.Enabled = false;
                }
                return;
            }

            // Успешная авторизация
            MessageBox.Show(
                "Вы успешно авторизовались",
                "Успех",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Переход к нужной панели
            if (user.Role == "admin")
            {
                AdminForm adminForm = new AdminForm(_userRepo);
                adminForm.Show();
            }
            else
            {
                UserForm userForm = new UserForm(user);
                userForm.Show();
            }

            this.Hide();
        }

        // ОБРАБОТЧИК СОБЫТИЯ ПАЗЛА
        private void PuzzleControl_PuzzleSolved(object sender, EventArgs e)
        {
            _puzzleSolved = true;
            lblPuzzleStatus.Text = "✓ Пазл собран верно!";
            lblPuzzleStatus.ForeColor = Color.Green;
        }
    }
}