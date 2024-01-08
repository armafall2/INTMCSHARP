using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public class Percolation
    {
        private readonly bool[,] _open;
        private readonly bool[,] _full;
        private readonly int _size;
        private bool _percolate;

        public Percolation(int size)
        {
            try
            {
                if (size <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(size), size, "Taille de la grille négative ou nulle.");
                }

                _open = new bool[size, size];
                _full = new bool[size, size];
                _size = size;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }


        public bool IsOpen(int i, int j)
        {
            if (ControleInterval(i, j) == true)
            {
                return _open[i, j];
            }
            return false;

        }


        private bool IsFull(int i, int j)
        {
            if (ControleInterval(i, j) == true)
            {
                return _full[i, j];
            }
            return false;
        }

        public bool Percolate()
        {
            bool[,] tempFull = new bool[_size, _size];

            for (int j = 0; j < _size; j++)
            {
                if (IsOpen(0, j))
                {
                    PropagateWater(0, j, tempFull);
                }
            }

            for (int j = 0; j < _size; j++)
            {
                if (tempFull[_size - 1, j])
                {
                    _percolate = true;
                    return _percolate;
                }
            }

            return _percolate;
        }

        private void PropagateWater(int i, int j, bool[,] tempFull)
        {
            if (i < 0 || i >= _size || j < 0 || j >= _size || !_open[i, j] || tempFull[i, j])
            {
                return;
            }
            tempFull[i, j] = true;

            PropagateWater(i - 1, j, tempFull); // haut
            PropagateWater(i + 1, j, tempFull); // bas
            PropagateWater(i, j - 1, tempFull); // gauche
            PropagateWater(i, j + 1, tempFull); // droite
        }



        private List<KeyValuePair<int, int>> CloseNeighbors(int row, int col)
        {
            List<KeyValuePair<int, int>> neighbors = new List<KeyValuePair<int, int>>();
            int[] rowOffsets = { -1, 1, 0, 0 };
            int[] colOffsets = { 0, 0, -1, 1 };

            for (int k = 0; k < rowOffsets.Length; k++)
            {
                int newRow = row + rowOffsets[k];
                int newCol = col + colOffsets[k];
                if (IsValidCoordinate(newRow, newCol))
                {
                    neighbors.Add(new KeyValuePair<int, int>(newRow, newCol));
                }
            }

            return neighbors;
        }

        private bool IsValidCoordinate(int row, int col)
        {
            return row >= 0 && row < _size && col >= 0 && col < _size;
        }


        public void Open(int row, int col)
        {
            try
            {
                if (IsOpen(row, col))
                {
                    throw new InvalidOperationException($"Case déjà ouverte");
                }
                _open[row, col] = true;
                foreach (var neighbor in CloseNeighbors(row, col))
                {
                    int neighborRow = neighbor.Key;
                    int neighborCol = neighbor.Value;

                    if (IsFull(neighborRow, neighborCol))
                    {
                        _open[neighborRow, neighborCol] = true;
                    }
                }
                if (IsFull(row, col))
                {
                    foreach (var neighbor in CloseNeighbors(row, col))
                    {
                        int neighborRow = neighbor.Key;
                        int neighborCol = neighbor.Value;
                        _full[neighborRow, neighborCol] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message} {row} {col}");
            }
        }


        public void PrintGrid()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    char symbol = IsOpen(i, j) ? 'O' : 'X';
                    Console.Write($"{symbol} ");
                }
                Console.WriteLine();
            }
        }
        private bool ControleInterval(int i, int j)
        {
            try
            {
                if (i < 0 || i >= _size || j < 0 || j >= _size)
                {
                   throw new ArgumentOutOfRangeException("Indices hors limites de la grille.");
                }

                return true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }
        }

    }
}

