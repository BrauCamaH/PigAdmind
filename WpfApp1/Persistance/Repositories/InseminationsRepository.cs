using System.Collections;
using System.Data.Entity;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
	class InseminationsRepository : Repository<Inseminations>, IInseminationsRepository
	{
		public InseminationsRepository(DbContext cont) : base(cont)
		{
		}

		public IEnumerable GetInseminationsByFemale(string code)
		{
			throw new System.NotImplementedException();
		}

		public Entities DbEntities
		{
			get { return DbEntities; }
		}
	}
}
