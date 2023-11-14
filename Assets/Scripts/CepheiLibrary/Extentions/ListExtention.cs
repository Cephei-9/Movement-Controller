using System;
using System.Collections.Generic;
using UnityEngine;

namespace CepheiForNPC
{
    public static class ListExtention
    {
        public static void SmartDelete<T>(this List<T> ls, int index)
        {
            int lastIndex = ls.Count - 1;
            T lastElement = ls[lastIndex];

            ls[lastIndex] = ls[index];
            ls[index] = lastElement;

            ls.RemoveAt(lastIndex);
        }

        public static void ForeachWithDelete<T>(this List<T> list, Func<T, bool> deleteFunc)
        {
            for (var index = 0; index < list.Count; index++)
            {
                T element = list[index];

                if (deleteFunc.Invoke(element))
                {
                    SmartDelete(list, index);
                    index--;
                }
            }
        }
    }
}