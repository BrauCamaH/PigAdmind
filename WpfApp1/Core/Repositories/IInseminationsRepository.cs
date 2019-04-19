using System.Collections;

namespace WpfApp1.Core.Repositories
{
	interface IInseminationsRepository
	{
		IEnumerable GetInseminationsByFemale(string code);
	}
}
