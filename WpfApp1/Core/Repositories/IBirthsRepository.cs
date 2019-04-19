using System.Collections;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
	interface IBirthsRepository : IRepository<Births>
	{
		IEnumerable GetBirthsByFemale(string code);
	}
}
