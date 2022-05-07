using System;
using System.Diagnostics.CodeAnalysis;

namespace Labyrinth
{
    public struct Location : IEquatable<Location>
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public Location(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public static bool operator ==(Location first, Location second) => first.Equals(second);
        public static bool operator !=(Location first, Location second) => !first.Equals(second);

        public override bool Equals([NotNullWhen(true)] object obj) => obj != null && obj is Location other && Equals(other);

        public bool Equals(Location other) => Column == other.Column && Row == other.Row;

        public override int GetHashCode()
        {
            return Column.GetHashCode() ^ Row.GetHashCode();
        }
    }
}
