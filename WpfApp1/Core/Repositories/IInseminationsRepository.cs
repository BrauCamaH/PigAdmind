using System.Collections.Generic;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IInseminationsRepository : IRepository<Inseminations>
    {
        IEnumerable<Inseminations> GetInseminationsByFemale(string code);

        void RemoveById(int id);

        Inseminations GetCurrentInsemination(DatabaseFirst.Females female);
    }
}
