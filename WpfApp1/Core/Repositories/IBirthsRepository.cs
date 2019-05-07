using System.Collections.Generic;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IBirthsRepository : IRepository<Births>
    {
        IEnumerable<Births> GetBirthsByFemale(string code);
    }
}
