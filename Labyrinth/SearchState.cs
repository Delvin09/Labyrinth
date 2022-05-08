using System.Collections.Generic;

namespace Labyrinth
{
    public class SearchState<T>
    {
        public T Current { get; set; }

        public IEnumerable<T> Possible { get; set; }

        public SearchState<T> Parent { get; set; }

        public SearchState(T current, IEnumerable<T> possible, SearchState<T> parent = null)
        {
            Current = current;
            Possible = possible;
            Parent = parent;
        }
    }
}
