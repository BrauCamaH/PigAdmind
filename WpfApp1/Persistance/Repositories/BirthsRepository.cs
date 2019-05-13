using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class BirthsRepository : Repository<Births>, IBirthsRepository
    {
        public BirthsRepository(DbContext cont) : base(cont)
        {
        }

        IEnumerable<Births> IBirthsRepository.GetBirthsByFemale(string code)
        {
            return DbEntities.Births.Where(b => b.fem_code == code).ToList();
        }

        public void RemoveByID(int id)
        {
            var query = from b in DbEntities.Births
                        where b.id == id
                        select b;
            if (query.First() != null)
            {
                DbEntities.Births.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
            }
        }


        public Entities DbEntities => Context as Entities;
    }
}
