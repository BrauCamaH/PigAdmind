using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class SalesRepository : Repository<DatabaseFirst.Sales>, ISalesRepository
    {
        public SalesRepository(DbContext cont) : base(cont)
        {
        }

        public IEnumerable GetSalesByUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public DatabaseFirst.Sales GetSaleById(int id)
        {
            return DbEntities.Sales.FirstOrDefault(s => s.id == id);
        }

        public void RemoveSaleById(int id)
        {
            var query = from s in DbEntities.PigGroups
                        where s.id == id
                        select s;
            if (query.First() != null)
            {
                DbEntities.PigGroups.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
            }
        }

        public Entities DbEntities => Context as Entities;
    }
}
