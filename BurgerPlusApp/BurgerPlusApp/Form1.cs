using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BurgerPlusApp
{
    public partial class Form1 : Form
    {
        // === ДАННЫЕ ПОЛЬЗОВАТЕЛЕЙ ===
        private Dictionary<string, string> пользователи = new Dictionary<string, string>
        {
            { "admin", "admin" },
            { "user1", "user1" }
        };

        private int попытки = 0;
        private bool пазлСобран = false;

        // === КОМПОНЕНТЫ ПАЗЛА ===
        private PictureBox[] плитки;
        private int[] порядок;
        private int пустая = 3;
        private bool собрано = false;
        private Image[] картинки;
        private Label lblStatus;

        public Form1()
        {
            // === НАСТРОЙКА ФОРМЫ ===
            this.Text = "Авторизация - ООО Бургер плюс";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(600, 450);

            // === ПОЛЯ ВВОДА ===
            Label lblLogin = new Label();
            lblLogin.Text = "Логин:";
            lblLogin.Location = new Point(30, 30);
            lblLogin.Size = new Size(60, 20);

            TextBox txtLogin = new TextBox();
            txtLogin.Location = new Point(30, 55);
            txtLogin.Size = new Size(200, 25);

            Label lblPass = new Label();
            lblPass.Text = "Пароль:";
            lblPass.Location = new Point(30, 95);
            lblPass.Size = new Size(60, 20);

            TextBox txtPass = new TextBox();
            txtPass.Location = new Point(30, 120);
            txtPass.Size = new Size(200, 25);
            txtPass.UseSystemPasswordChar = true;

            // === СТАТУС ПАЗЛА ===
            lblStatus = new Label();
            lblStatus.Text = "Соберите пазл";
            lblStatus.Location = new Point(260, 350);
            lblStatus.Size = new Size(200, 20);

            // === ПАНЕЛЬ ДЛЯ ПАЗЛА ===
            Panel panelPuzzle = new Panel();
            panelPuzzle.Location = new Point(260, 30);
            panelPuzzle.Size = new Size(300, 300);
            panelPuzzle.BorderStyle = BorderStyle.FixedSingle;

            // === ЗАГРУЖАЕМ КАРТИНКИ ===
            ЗагрузитьКартинки();

            // === СОЗДАЁМ ПАЗЛ ===
            плитки = new PictureBox[4];
            порядок = new int[4];

            // Правильный порядок: 0, 1, 2, 3 (3 — это пустая клетка)
            for (int i = 0; i < 4; i++) порядок[i] = i;

            // ПЕРЕМЕШИВАЕМ (теперь правильно!)
            Перемешать();

            // СОЗДАЁМ ПЛИТКИ
            for (int i = 0; i < 4; i++)
            {
                плитки[i] = new PictureBox();
                плитки[i].Size = new Size(145, 145);
                плитки[i].BorderStyle = BorderStyle.FixedSingle;
                плитки[i].SizeMode = PictureBoxSizeMode.StretchImage;
                плитки[i].Cursor = Cursors.Hand;

                // ПОДПИСКА НА КЛИК
                int индекс = i; // важно для замыкания
                плитки[i].Click += (s, e) => НажатиеНаПлитку(индекс);

                panelPuzzle.Controls.Add(плитки[i]);
            }

            // ОБНОВЛЯЕМ ПОЗИЦИИ ПЛИТОК
            ОбновитьПлитки();

            // === КНОПКА ВХОДА ===
            Button btnLogin = new Button();
            btnLogin.Text = "Войти";
            btnLogin.Location = new Point(30, 170);
            btnLogin.Size = new Size(200, 40);
            btnLogin.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!пазлСобран)
                {
                    MessageBox.Show("Соберите пазл!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (пользователи.ContainsKey(txtLogin.Text) && пользователи[txtLogin.Text] == txtPass.Text)
                {
                    MessageBox.Show("Вы успешно авторизовались!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Text = "Добро пожаловать, " + txtLogin.Text + "!";
                    btnLogin.Enabled = false;
                }
                else
                {
                    попытки++;
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (попытки >= 3)
                    {
                        MessageBox.Show("Вы заблокированы!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnLogin.Enabled = false;
                    }
                }
            };

            // === ДОБАВЛЯЕМ ВСЁ НА ФОРМУ ===
            this.Controls.Add(lblLogin);
            this.Controls.Add(txtLogin);
            this.Controls.Add(lblPass);
            this.Controls.Add(txtPass);
            this.Controls.Add(btnLogin);
            this.Controls.Add(panelPuzzle);
            this.Controls.Add(lblStatus);
        }

        // === ЗАГРУЗКА КАРТИНОК ===
        private void ЗагрузитьКартинки()
        {
            try
            {
                картинки = new Image[]
                {
                    Properties.Resources.p1,
                    Properties.Resources.p2,
                    Properties.Resources.p3,
                    Properties.Resources.p4
                };
            }
            catch
            {
                // Если картинок нет — создаём цветные квадраты
                картинки = new Image[4];
                Color[] цвета = { Color.Red, Color.Blue, Color.Green, Color.Yellow };
                string[] буквы = { "А", "Б", "В", "Г" };

                for (int i = 0; i < 4; i++)
                {
                    Bitmap bmp = new Bitmap(145, 145);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(цвета[i]);
                        g.DrawString(буквы[i], new Font("Arial", 48, FontStyle.Bold), Brushes.White, 50, 40);
                    }
                    картинки[i] = bmp;
                }
            }
        }

        // === ПЕРЕМЕШИВАНИЕ (ТЕПЕРЬ РАБОТАЕТ!) ===
        private void Перемешать()
        {
            Random rand = new Random();

            // Делаем 200 случайных ходов, чтобы хорошо перемешать
            for (int шаг = 0; шаг < 200; шаг++)
            {
                int строка = пустая / 2;
                int столбец = пустая % 2;
                List<int> соседи = new List<int>();

                if (строка > 0) соседи.Add(пустая - 2);
                if (строка < 1) соседи.Add(пустая + 2);
                if (столбец > 0) соседи.Add(пустая - 1);
                if (столбец < 1) соседи.Add(пустая + 1);

                int сосед = соседи[rand.Next(соседи.Count)];

                // Меняем местами
                int temp = порядок[пустая];
                порядок[пустая] = порядок[сосед];
                порядок[сосед] = temp;
                пустая = сосед;
            }

            // Проверяем, что пазл не собран случайно
            bool случайноСобран = true;
            for (int i = 0; i < 4; i++)
            {
                if (порядок[i] != i) { случайноСобран = false; break; }
            }

            // Если случайно собрался — перемешиваем ещё раз
            if (случайноСобран)
            {
                // Меняем местами две плитки, чтобы разобрать
                int temp = порядок[0];
                порядок[0] = порядок[1];
                порядок[1] = temp;
                пустая = 3;
            }

            собрано = false;
            пазлСобран = false;
        }

        // === ОБНОВЛЕНИЕ ПОЗИЦИЙ ПЛИТОК ===
        private void ОбновитьПлитки()
        {
            for (int i = 0; i < 4; i++)
            {
                int pos = порядок[i];
                int row = pos / 2;
                int col = pos % 2;
                плитки[i].Location = new Point(col * 145 + 5, row * 145 + 5);

                if (pos == 3) // пустая клетка
                {
                    плитки[i].Visible = false;
                    плитки[i].Image = null;
                }
                else
                {
                    плитки[i].Visible = true;
                    плитки[i].Image = картинки[pos];
                }
            }
        }

        // === ОБРАБОТЧИК КЛИКА ПО ПЛИТКЕ ===
        private void НажатиеНаПлитку(int индекс)
        {
            if (собрано || пазлСобран) return;

            int стрНаж = индекс / 2;
            int стлНаж = индекс % 2;
            int стрПуст = пустая / 2;
            int стлПуст = пустая % 2;

            // Проверяем, рядом ли с пустой клеткой
            if (Math.Abs(стрНаж - стрПуст) + Math.Abs(стлНаж - стлПуст) == 1)
            {
                // Меняем местами
                int temp = порядок[индекс];
                порядок[индекс] = порядок[пустая];
                порядок[пустая] = temp;
                пустая = индекс;

                ОбновитьПлитки();

                // Проверяем победу
                bool win = true;
                for (int j = 0; j < 4; j++)
                {
                    if (порядок[j] != j) { win = false; break; }
                }

                if (win)
                {
                    собрано = true;
                    пазлСобран = true;
                    lblStatus.Text = "✓ Пазл собран!";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Пазл собран верно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}