using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labyrinth
{
    public class Labyrinth : IRendererObject
    {
        private readonly int _columns, _rows;
        private readonly Location _start;
        private readonly Location _end;
        private readonly Cell[,] _grid;
        private static readonly Random _random = new Random();

        public Labyrinth()
            : this(columns: 10, rows: 10, start: new Location(0, 0), end: new Location(9, 9), ratio: 0.14f)
        {
        }

        public Labyrinth(int columns, int rows, Location start, Location end, float ratio)
        {
            _columns = columns;
            _rows = rows;
            _start = start;
            _end = end;
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

            _grid[_start.Column, _start.Row] = Cell.START;
            _grid[_end.Column, _end.Row] = Cell.END;
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

        public class SearchState
        {
            public Location Current { get; set; }
            public List<Location> Possible { get; set; }

            public SearchState(Location current, List<Location> possible)
            {
                Current = current;
                Possible = possible;
            }
        }

        public IEnumerable<SearchState> DeepSearch()
        {
            HashSet<Location> explored = new HashSet<Location>();
            Stack<SearchState> states = new Stack<SearchState>();
            states.Push(new SearchState(_start, PossibeMoves(_start).ToList()));
            explored.Add(_start);

            while (states.Count > 0)
            {
                var current = states.Peek();
                if (current.Possible.Contains(_end))
                    break;

                var nextLocIndex = current.Possible.FindIndex(s => !explored.Contains(s));
                if (nextLocIndex >= 0)
                {
                    var move = current.Possible[nextLocIndex];
                    if (_grid[move.Column, move.Row] == Cell.END)
                        break; // find the way

                    _grid[move.Column, move.Row] = Cell.PATHED;
                    Render(move);

                    explored.Add(move);
                    states.Push(new SearchState(move, PossibeMoves(move).ToList()));
                }
                else
                {
                    var move = states.Pop();
                    _grid[move.Current.Column, move.Current.Row] = Cell.EMPTY;
                }
            }
            return states;
        }

        private IEnumerable<Location> PossibeMoves(Location position)
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
    }


    class Program
    {
        static void Main(string[] args)
        {
            var lab = new Labyrinth();
            lab.Render();
            var searchResult = lab.DeepSearch();
        }
    }
}
