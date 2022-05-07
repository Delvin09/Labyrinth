using System;
using System.Diagnostics.CodeAnalysis;

namespace Labyrinth
{
    public struct Cell : IEquatable<Cell>
    {
        public static readonly Cell EMPTY = new Cell(' ');
        public static readonly Cell BLOCED = new Cell('X');
        public static readonly Cell START = new Cell('S');
        public static readonly Cell END = new Cell('E');
        public static readonly Cell PATHED = new Cell('*');

        public char Value { get; set; } = ' ';

        private Cell(char val)
        {
            Value = val;
        }

        public static bool operator == (Cell first, Cell second) => first.Equals(second);

        public static bool operator != (Cell first, Cell second) => !first.Equals(second);

        public override bool Equals([NotNullWhen(true)] object obj)
            => obj != null && obj is Cell other && this.Equals(other);

        public bool Equals(Cell other) => Value == other.Value;

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();
    }
}
