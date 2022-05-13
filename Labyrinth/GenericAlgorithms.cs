using System;
using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    public static class GenericAlgorithms
    {
        public static IEnumerable<SearchState<T>> DeptSearch_v1<T>(T begin, T end, Func<T, IEnumerable<T>> getNext)
        {
            var explored = new HashSet<T>();
            var states = new Stack<SearchState<T>>();
            states.Push(new SearchState<T>(begin, getNext(begin)));
            explored.Add(begin);

            while (states.Count > 0)
            {
                var current = states.Peek();
                if (current.Possible.Contains(end))
                    break;

                var nextLocIndex = current.Possible.FindIndex(s => !explored.Contains(s));
                if (nextLocIndex >= 0)
                {
                    var move = current.Possible.ElementAt(nextLocIndex);
                    explored.Add(move);
                    states.Push(new SearchState<T>(move, getNext(move)));
                }
                else
                {
                    states.Pop();
                }
            }
            return states;
        }

        public static IEnumerable<T> DeptSearch_v2<T>(T begin, T end, Func<T, IEnumerable<T>> getNext)
        {
            HashSet<T> explored = new HashSet<T>();
            Stack<SearchState<T>> states = new Stack<SearchState<T>>();
            states.Push(new SearchState<T>(begin, getNext(begin)));
            explored.Add(begin);

            SearchState<T> current = null;
            while (states.Count > 0)
            {
                current = states.Pop();
                if (current.Possible.Contains(end))
                    break;

                foreach (var move in current.Possible.Where(s => !explored.Contains(s)))
                {
                    explored.Add(move);
                    states.Push(new SearchState<T>(move, getNext(move), current));
                }
            }
            return current.GetPath();
        }

        public static IEnumerable<T> WideSearch<T>(T begin, T end, Func<T, IEnumerable<T>> getNext)
        {
            HashSet<T> explored = new HashSet<T>();
            Queue<SearchState<T>> states = new Queue<SearchState<T>>();
            states.Enqueue(new SearchState<T>(begin, getNext(begin).ToList()));
            explored.Add(begin);

            SearchState<T> current = null;
            while (states.Count > 0)
            {
                current = states.Dequeue();
                if (current.Possible.Contains(end))
                    break;

                foreach (var move in current.Possible.Where(l => !explored.Contains(l)))
                {
                    explored.Add(move);
                    states.Enqueue(new SearchState<T>(move, getNext(move), current));
                }
            }
            return current.GetPath();
        }

        public static IEnumerable<T> AStarSearch<T>(T begin, T end, Func<T, IEnumerable<T>> getNext, Func<T, int> calcHeruistic)
        {
            HashSet<T> explored = new HashSet<T>();
            PriorityQueue<SearchState<T>, int> states = new PriorityQueue<SearchState<T>, int>();
            states.Enqueue(new SearchState<T>(begin, getNext(begin)), calcHeruistic(begin));
            explored.Add(begin);

            SearchState<T> current = null;
            while (states.Count > 0)
            {
                current = states.Dequeue();
                if (current.Possible.Contains(end))
                    break;

                foreach (var move in current.Possible.Where(l => !explored.Contains(l)))
                {
                    explored.Add(move);
                    states.Enqueue(new SearchState<T>(move, getNext(move), current), calcHeruistic(move));
                }
            }
            return current.GetPath();
        }

        private static IEnumerable<T> GetPath<T>(this SearchState<T> state)
        {
            while (state != null)
            {
                yield return state.Current;
                state = state.Parent;
            }
        }
    }
}
