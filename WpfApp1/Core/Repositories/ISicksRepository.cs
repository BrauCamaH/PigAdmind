using System.Collections.Generic;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface ISicksRepository : IRepository<Sicks>
    {
        IEnumerable<Sicks> GetSicksByFemale(string code);

        void RemoveById(int id);
    }
}
