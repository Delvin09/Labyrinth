using System;
using System.Drawing;
using System.Linq;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {

            var lab = new Labyrinth();
            lab.Render();

            var path = GenericAlgorithms.DeptSearch_v1(lab.Start, lab.End, lab.PossibeMoves).Select(r => r.Current);
            lab.AcceptPath(path);
            lab.Render();
            lab.Clear();

            path = GenericAlgorithms.DeptSearch_v2(lab.Start, lab.End, lab.PossibeMoves);
            lab.AcceptPath(path);
            lab.Render();
            lab.Clear();

            path = GenericAlgorithms.WideSearch(lab.Start, lab.End, lab.PossibeMoves);
            lab.AcceptPath(path);
            lab.Render();
            lab.Clear();

            path = GenericAlgorithms.AStarSearch(lab.Start, lab.End, lab.PossibeMoves
                , possition => Math.Abs(possition.Row - lab.End.Row) + Math.Abs(possition.Column - lab.End.Column));
            lab.AcceptPath(path);
            lab.Render();
            lab.Clear();
        }
    }
}
