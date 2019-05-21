using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfApp1.Persistance
{
    class CrudOperations<TEntity> where TEntity : class
    {
        private ObservableCollection<TEntity> _observableCollection;

        public CrudOperations(ObservableCollection<TEntity> observableCollection)
        {
            _observableCollection = observableCollection;
        }

        public static void AddRange<T>(ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }
    }
}
