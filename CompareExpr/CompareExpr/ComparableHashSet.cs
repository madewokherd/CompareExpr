using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareExpr
{
    class ComparableHashSet<T> : HashSet<T>
    {
        private static readonly IEqualityComparer<HashSet<T>> comparer = HashSet<T>.CreateSetComparer();

        public ComparableHashSet() : base ()
        {
        }

        public ComparableHashSet(IEnumerable<T> e) : base (e)
        {
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HashSet<T>))
                return false;
            return comparer.Equals(this, obj as HashSet<T>);
        }

        public override int GetHashCode()
        {
            return comparer.GetHashCode(this);
        }
    }
}
