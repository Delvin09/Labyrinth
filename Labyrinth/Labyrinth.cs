using System;
using System.Collections.Generic;

namespace Labyrinth
{
    public class Labyrinth : IRendererObject
    {
        private readonly int _columns, _rows;
        private readonly Cell[,] _grid;
        private static readonly Random _random = new Random();

        public Location Start { get; }

        public Location End { get;  }

        public Labyrinth()
            : this(columns: 10, rows: 10, start: new Location(0, 0), end: new Location(9, 9), ratio: 0.14f)
        {
        }

        public Labyrinth(int columns, int rows, Location start, Location end, float ratio)
        {
            _columns = columns;
            _rows = rows;
            Start = start;
            End = end;
            _grid = new Cell[columns, rows];
            Generate(ratio);
        }

        private void Generate(float ratio)
        {
            for (int i = 0; i < _columns; i++)
                for (int j = 0; j < _rows; j++)
                {
                    if (_random.NextDouble().CompareTo(ratio) < 0)
                        _grid[i, j] = Cell.BLOCED;
                    else
                        _grid[i, j] = Cell.EMPTY;
                }

            _grid[Start.Column, Start.Row] = Cell.START;
            _grid[End.Column, End.Row] = Cell.END;
        }

        public void Render(Location? hightlight = null)
        {
            Console.SetCursorPosition(0, 0);
            for (int column = 0; column < _columns; column++)
            {
                for (int row = 0; row < _rows; row++)
                {
                    var defColor = Console.ForegroundColor;
                    if (hightlight.HasValue && column == hightlight.Value.Column && row == hightlight.Value.Row)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(_grid[column, row]);
                    Console.ForegroundColor = defColor;
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        public IEnumerable<Location> PossibeMoves(Location position)
        {
            var nextColumn = position.Column + 1;
            var prevColumn = position.Column - 1;
            var nextRow = position.Row + 1;
            var prevRow = position.Row - 1;

            if (nextColumn < _columns && nextColumn >= 0 && _grid[nextColumn, position.Row] != Cell.BLOCED)
                yield return new Location(nextColumn, position.Row);

            if (nextRow < _rows && nextRow >= 0 && _grid[position.Column, nextRow] != Cell.BLOCED)
                yield return new Location(position.Column, nextRow);

            if (prevColumn < _columns && prevColumn >= 0 && _grid[prevColumn, position.Row] != Cell.BLOCED)
                yield return new Location(prevColumn, position.Row);
            
            if (prevRow < _rows && prevRow >= 0 && _grid[position.Column, prevRow] != Cell.BLOCED)
                yield return new Location(position.Column, prevRow);
        }

        public void AcceptPath(IEnumerable<Location> searchResult)
        {
            foreach (var location in searchResult)
            {
                _grid[location.Column, location.Row] = Cell.PATHED;
            }
            _grid[Start.Column, Start.Row] = Cell.START;
            _grid[End.Column, End.Row] = Cell.END;
        }

        public void Clear()
        {

            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    if (_grid[i, j] == Cell.PATHED)
                        _grid[i, j] = Cell.EMPTY;
                }
            }
            _grid[Start.Column, Start.Row] = Cell.START;
            _grid[End.Column, End.Row] = Cell.END;
        }
    }
}
