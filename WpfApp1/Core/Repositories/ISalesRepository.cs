using System.Collections;

namespace WpfApp1.Core.Repositories
{
    interface ISalesRepository : IRepository<DatabaseFirst.Sales>
    {
        IEnumerable GetSalesByUser(int id);
        DatabaseFirst.Sales GetSaleById(int id);

        void RemoveSaleById(int id);
    }
}
