﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApp1.Persistance
{
    class CrudOperations<TEntity> where TEntity : class
    {
        public CrudOperations()
        {

        }

        public static void AddRange<T>(ObservableCollection<T> coll, IEnumerable<T> items)
        {
            var enumerable = items.ToList();
            foreach (var item in enumerable)
            {
                if (coll.Count < enumerable.Count())
                {
                    coll.Add(item);
                }

            }
        }
    }
}
