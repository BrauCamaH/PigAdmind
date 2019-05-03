using System.Collections;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
	class BirthsRepository : Repository<Births>, IBirthsRepository
	{
		public BirthsRepository(Entities cont) : base(cont)
		{
		}

		public IEnumerable GetBirthsByFemale(string code)
		{
			return DbEntities.Births.Where(b => b.fem_code == code).ToList();
		}

		public Entities DbEntities
		{
			get { return new Entities(); }
		}
	}
}
