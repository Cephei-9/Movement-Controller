using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cephei
{
    public static class LinkedListExtantion
    {
        public static void ByPass<T>(this LinkedList<T> list, Action<T> action)
        {
            LinkedListNode<T> node = list.First;
            while (node != null)
            {
                action.Invoke(node.Value);
                node = node.Next;
            }
        }

        public static LinkedList<T> AddRange<T>(this LinkedList<T> ts, IEnumerable<T> toAdd)
        {
            foreach (var item in toAdd)
            {
                ts.AddLast(item);
            }

            return ts;
        }
    }
}
