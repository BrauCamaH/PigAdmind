using System;
using System.Collections;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance.Repositories
{
	class SicksRepository : Repository<Sicks>, ISicksRepository
	{
		public SicksRepository(Entities cont) : base(cont)
		{
		}

		public IEnumerable GetBirthsByFemale(string code)
		{
			throw new NotImplementedException();
		}

		public Entities DbEntities
		{
			get { return DbEntities; }
		}

		public IEnumerable GetSicksByFemale(string code)
		{
			throw new NotImplementedException();
		}
	}
}
