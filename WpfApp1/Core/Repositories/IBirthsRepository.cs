using System.Collections.Generic;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IBirthsRepository : IRepository<Births>
    {
        IEnumerable<Births> GetBirthsByFemale(string code);
        void RemoveByID(int id);

        Births GetCurrentBirth(DatabaseFirst.Females female);

        Weaning GetWeaning(Births birth);
    }
}
