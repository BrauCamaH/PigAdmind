using System.Collections.Generic;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IGroupsRepository : IRepository<PigGroups>
    {
        IEnumerable<PigGroups> GetGroupsByUser(int id);
        PigGroups GetGroupById(int id);

        void RemoveGroupById(int id);
    }
}
