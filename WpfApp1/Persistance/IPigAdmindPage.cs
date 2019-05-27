using System;

namespace WpfApp1.Persistance
{
    interface IPigAdmindPage<TEntity, TEventArgs> where TEntity : class
    {
        void InitializeCrudControls(TEntity entity);
        bool CustomFilter(Object obj);

        void GetItemsFromDatabase();

        void OnItemAdded(object sender, TEventArgs e);

        void OnItemDeleted(object sender, TEventArgs e);

        void OnItemEdited(object sender, TEventArgs e);
    }
}
