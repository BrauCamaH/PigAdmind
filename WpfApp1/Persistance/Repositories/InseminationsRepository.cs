using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class InseminationsRepository : Repository<Inseminations>, IInseminationsRepository
    {
        public InseminationsRepository(DbContext cont) : base(cont)
        {
        }

        IEnumerable<Inseminations> IInseminationsRepository.GetInseminationsByFemale(string code)
        {
            return DbEntities.Inseminations.Where(b => b.fem_code == code).ToList();
        }

        public void RemoveById(int id)
        {
            var query = from i in DbEntities.Inseminations
                        where i.id == id
                        select i;
            if (query.First() != null)
            {
                DbEntities.Inseminations.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
            }
        }


        public Inseminations GetCurrentInsemination(DatabaseFirst.Females female)
        {
            return DbEntities.Inseminations.FirstOrDefault(i => i.fem_code == female.code && i.status == "actual");
        }

        public Entities DbEntities => Context as Entities;
    }
}
