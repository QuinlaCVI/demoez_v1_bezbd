using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BurgerPlusApp.Controls
{
    public class PuzzleControl : UserControl
    {
        private PictureBox[] _tiles;
        private int _gridSize = 3;
        private int _tileSize = 100;
        private int[] _tileOrder;
        private int _emptyIndex = 8;
        private bool _isSolved = false;

        // Событие, когда пазл собран
        public event System.EventHandler PuzzleSolved;

        public PuzzleControl()
        {
            InitializePuzzle();
        }

        private void InitializePuzzle()
        {
            this.Size = new Size(310, 310);
            this.BackColor = Color.LightGray;

            // Создаём массив из 9 плиток (3x3)
            _tiles = new PictureBox[_gridSize * _gridSize];
            _tileOrder = new int[_gridSize * _gridSize];

            // Инициализируем порядок (0-8)
            for (int i = 0; i < _tileOrder.Length; i++)
            {
                _tileOrder[i] = i;
            }

            // Перемешиваем
            Shuffle();

            // Создаём плитки
            for (int i = 0; i < _tiles.Length; i++)
            {
                _tiles[i] = new PictureBox();
                _tiles[i].Size = new Size(_tileSize, _tileSize);
                _tiles[i].BorderStyle = BorderStyle.FixedSingle;
                _tiles[i].BackColor = Color.White;
                _tiles[i].Click += Tile_Click;

                // Рисуем номер на плитке
                int row = i / _gridSize;
                int col = i % _gridSize;
                _tiles[i].Location = new Point(col * _tileSize + 5, row * _tileSize + 5);

                // Если это последняя плитка (пустая) — делаем её серой
                if (i == _emptyIndex)
                {
                    _tiles[i].BackColor = Color.LightGray;
                    _tiles[i].Tag = "empty";
                }
                else
                {
                    using (Graphics g = _tiles[i].CreateGraphics())
                    {
                        g.Clear(Color.FromArgb(200, 200, 255));
                        g.DrawString((i + 1).ToString(), new Font("Arial", 24), Brushes.Black, 30, 30);
                    }
                    _tiles[i].Tag = i + 1;
                }

                this.Controls.Add(_tiles[i]);
            }

            // Обновляем позиции согласно перемешанному порядку
            UpdateTiles();
        }

        private void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                // Находим соседей пустой клетки
                int emptyRow = _emptyIndex / _gridSize;
                int emptyCol = _emptyIndex % _gridSize;

                List<int> neighbors = new List<int>();

                if (emptyRow > 0) neighbors.Add(_emptyIndex - _gridSize);
                if (emptyRow < _gridSize - 1) neighbors.Add(_emptyIndex + _gridSize);
                if (emptyCol > 0) neighbors.Add(_emptyIndex - 1);
                if (emptyCol < _gridSize - 1) neighbors.Add(_emptyIndex + 1);

                int swapIndex = neighbors[rand.Next(neighbors.Count)];

                // Меняем местами
                int temp = _tileOrder[_emptyIndex];
                _tileOrder[_emptyIndex] = _tileOrder[swapIndex];
                _tileOrder[swapIndex] = temp;
                _emptyIndex = swapIndex;
            }
            _isSolved = false;
        }

        private void UpdateTiles()
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                int position = _tileOrder[i];
                int row = position / _gridSize;
                int col = position % _gridSize;
                _tiles[i].Location = new Point(col * _tileSize + 5, row * _tileSize + 5);

                // Если позиция пустая — скрываем плитку
                if (position == 8)
                {
                    _tiles[i].Visible = false;
                }
                else
                {
                    _tiles[i].Visible = true;
                }
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            if (_isSolved) return;

            PictureBox clicked = sender as PictureBox;
            int clickedIndex = -1;

            for (int i = 0; i < _tiles.Length; i++)
            {
                if (_tiles[i] == clicked)
                {
                    clickedIndex = i;
                    break;
                }
            }

            if (clickedIndex == -1) return;

            // Проверяем, можно ли переместить
            int clickedRow = clickedIndex / _gridSize;
            int clickedCol = clickedIndex % _gridSize;
            int emptyRow = _emptyIndex / _gridSize;
            int emptyCol = _emptyIndex % _gridSize;

            if (Math.Abs(clickedRow - emptyRow) + Math.Abs(clickedCol - emptyCol) == 1)
            {
                // Меняем местами
                int temp = _tileOrder[clickedIndex];
                _tileOrder[clickedIndex] = _tileOrder[_emptyIndex];
                _tileOrder[_emptyIndex] = temp;
                _emptyIndex = clickedIndex;

                UpdateTiles();

                // Проверяем, собран ли пазл
                CheckSolved();
            }
        }

        private void CheckSolved()
        {
            for (int i = 0; i < _tileOrder.Length; i++)
            {
                if (_tileOrder[i] != i)
                {
                    return;
                }
            }

            _isSolved = true;
            MessageBox.Show("Пазл собран верно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PuzzleSolved?.Invoke(this, EventArgs.Empty);
        }

        public void ResetPuzzle()
        {
            Shuffle();
            UpdateTiles();
            _isSolved = false;
        }
    }
}