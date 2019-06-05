using System.Data.Entity;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class WeaningRepository : Repository<Weaning>, IWeaningRepository
    {
        public WeaningRepository(DbContext cont) : base(cont)
        {
        }

    }
}
