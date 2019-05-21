using System.Collections;

namespace WpfApp1.Core.Repositories
{
    interface IFemalesRepository : IRepository<DatabaseFirst.Females>
    {
        IEnumerable GetFemalesByUser(int id);
        DatabaseFirst.Females GetFemaleByCode(string code);

        void RemoveFemaleByCode(string code);
    }
}
