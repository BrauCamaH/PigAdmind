using System.Collections;

namespace WpfApp1.Core.Repositories
{
	interface ISicksRepository
	{
		IEnumerable GetSicksByFemale(string code);
	}
}
