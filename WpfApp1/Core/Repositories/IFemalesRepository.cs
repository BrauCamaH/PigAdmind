using System.Collections;

namespace WpfApp1.Core.Repositories
{
    interface IFemalesRepository : IRepository<DatabaseFirst.Females>
    {
        IEnumerable GetFemalesByUser(int id);
    }
}
