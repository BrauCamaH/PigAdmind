using System;
using System.Collections;
using System.Data.Entity;
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

		public Entities DbEntities
		{
			get { return DbEntities; }
		}
	}
}
