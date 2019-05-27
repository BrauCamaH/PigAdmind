using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class SicksRepository : Repository<Sicks>, ISicksRepository
    {
        public SicksRepository(Entities cont) : base(cont)
        {
        }




        public IEnumerable<Sicks> GetSicksByFemale(string code)
        {
            return DbEntities.Sicks.Where(s => s.fem_code == code).ToList();
        }

        public void RemoveById()
        {
            throw new NotImplementedException();
        }

        public Entities DbEntities => Context as Entities;
    }
}
