using System;

namespace Labyrinth
{
    public interface IRendererObject
    {
        void Render();
    }

    public struct Cell
    {
        public const char EMPTY = ' ';
        public const char FULL = 'X';
        public const char START = 'S';
        public const char END = 'E';

        public char Value { get; set; }

        public Cell()
        {
        }
    }

    public class Labyrinth : IRendererObject
    {
        private readonly int _columns, _rows;
        private readonly Cell[,] _grid;

        public Labyrinth(int columns = 10, int rows = 10)
        {
            _grid = new Cell[columns, rows];
        }

        public void Render()
        {
            Console.SetCursorPosition(0, 0);
            for(int column = 0; column < _columns; column++)
            {
                for(int row = 0; row < _rows; row++)
                {
                    Console.Write(_grid[column, row]);
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
        }
    }
}
