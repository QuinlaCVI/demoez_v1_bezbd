using System;
using System.Drawing;
using System.Windows.Forms;

namespace BurgerPlusApp.Controls
{
    public class PuzzleControl : UserControl
    {
        private PictureBox[] плитки = new PictureBox[4];
        private int[] картинкиНаМестах = new int[4];
        private int перваяВыбранная = -1;
        private bool победа = false;
        private Random rnd = new Random();
        private Image[] картинки;

        public event EventHandler ПазлСобран;

        public PuzzleControl()
        {
            this.Size = new Size(300, 300);
            this.BackColor = Color.LightGray;
            this.DoubleBuffered = true;

            // === ЗАГРУЖАЕМ КАРТИНКИ ИЗ РЕСУРСОВ ===
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
                картинки = СоздатьТестовыеКартинки();
            }

            // Изначально всё на своих местах
            for (int i = 0; i < 4; i++)
            {
                картинкиНаМестах[i] = i;
            }

            // ПЕРЕМЕШИВАЕМ
            for (int i = 0; i < 20; i++)
            {
                int a = rnd.Next(4);
                int b = rnd.Next(4);
                if (a != b)
                {
                    int temp = картинкиНаМестах[a];
                    картинкиНаМестах[a] = картинкиНаМестах[b];
                    картинкиНаМестах[b] = temp;
                }
            }

            // СОЗДАЁМ 4 ПЛИТКИ
            for (int i = 0; i < 4; i++)
            {
                плитки[i] = new PictureBox();
                плитки[i].Size = new Size(145, 145);
                плитки[i].SizeMode = PictureBoxSizeMode.StretchImage;
                плитки[i].BorderStyle = BorderStyle.FixedSingle;
                плитки[i].BackColor = Color.White;
                плитки[i].Cursor = Cursors.Hand;

                // Ставим картинку
                плитки[i].Image = картинки[картинкиНаМестах[i]];

                int индекс = i;
                плитки[i].Click += (s, e) => Клик(индекс);

                this.Controls.Add(плитки[i]);
            }

            РасставитьПлитки();
        }

        private Image[] СоздатьТестовыеКартинки()
        {
            Image[] тест = new Image[4];
            Color[] цвета = { Color.Red, Color.Blue, Color.Green, Color.Yellow };
            string[] буквы = { "1", "2", "3", "4" };

            for (int i = 0; i < 4; i++)
            {
                Bitmap bmp = new Bitmap(145, 145);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(цвета[i]);
                    g.DrawString(буквы[i], new Font("Arial", 48, FontStyle.Bold), Brushes.White, 50, 40);
                }
                тест[i] = bmp;
            }
            return тест;
        }

        private void РасставитьПлитки()
        {
            for (int i = 0; i < 4; i++)
            {
                int строка = i / 2;
                int столбец = i % 2;
                плитки[i].Location = new Point(столбец * 145 + 5, строка * 145 + 5);
                плитки[i].BringToFront();
            }
        }

        private void ОбновитьКартинки()
        {
            for (int i = 0; i < 4; i++)
            {
                плитки[i].Image = картинки[картинкиНаМестах[i]];
            }
        }

        private void Клик(int индекс)
        {
            if (победа) return;

            if (перваяВыбранная == -1)
            {
                перваяВыбранная = индекс;
                плитки[индекс].BorderStyle = BorderStyle.Fixed3D;
                return;
            }

            if (перваяВыбранная == индекс)
            {
                плитки[индекс].BorderStyle = BorderStyle.FixedSingle;
                перваяВыбранная = -1;
                return;
            }

            // МЕНЯЕМ МЕСТАМИ
            int temp = картинкиНаМестах[перваяВыбранная];
            картинкиНаМестах[перваяВыбранная] = картинкиНаМестах[индекс];
            картинкиНаМестах[индекс] = temp;

            плитки[перваяВыбранная].BorderStyle = BorderStyle.FixedSingle;
            перваяВыбранная = -1;

            ОбновитьКартинки();

            // ПРОВЕРЯЕМ ПОБЕДУ
            bool win = true;
            for (int i = 0; i < 4; i++)
            {
                if (картинкиНаМестах[i] != i)
                {
                    win = false;
                    break;
                }
            }

            if (win)
            {
                победа = true;
                MessageBox.Show("Пазл собран верно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ПазлСобран?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Сбросить()
        {
            for (int i = 0; i < 4; i++)
            {
                картинкиНаМестах[i] = i;
            }

            for (int i = 0; i < 20; i++)
            {
                int a = rnd.Next(4);
                int b = rnd.Next(4);
                if (a != b)
                {
                    int temp = картинкиНаМестах[a];
                    картинкиНаМестах[a] = картинкиНаМестах[b];
                    картинкиНаМестах[b] = temp;
                }
            }

            ОбновитьКартинки();
            победа = false;
            перваяВыбранная = -1;

            for (int i = 0; i < 4; i++)
            {
                плитки[i].BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }
}