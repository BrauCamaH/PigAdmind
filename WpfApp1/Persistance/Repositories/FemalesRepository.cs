using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
    class FemalesRepository : Repository<DatabaseFirst.Females>, IFemalesRepository
    {
        public FemalesRepository(DbContext cont) : base(cont)
        {
        }

        public IEnumerable GetFemalesByUser(int id)
        {
            throw new NotImplementedException();
        }

        public DatabaseFirst.Females GetFemaleByCode(string code)
        {
            return DbEntities.Females.FirstOrDefault(f => f.code == code);
        }

        public void RemoveFemaleByCode(string code)
        {
            var query = from f in DbEntities.Females
                        where f.code.Equals(code)
                        select f;
            if (query.First() != null)
            {
                DbEntities.Females.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
            }


        }

        public void SetMaternity(DatabaseFirst.Females female)
        {
            if (female.successbirths != null && female.misbirths != null)
            {
                double maternity = (double)(female.successbirths * 100 / (female.successbirths + female.misbirths));
                female.martenity = maternity;
            }

            //MessageBox.Show(female.martenity + "   " + female.successbirths + "   " + female.misbirths);
        }

        public Entities DbEntities => Context as Entities;
    }
}
