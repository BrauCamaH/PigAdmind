using System.Collections;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IGroupsRepository : IRepository<PigGroups>
    {
        IEnumerable GetGroupsByUser(int id);
        PigGroups GetGroupById(int id);

        void RemoveGroupById(int id);
    }
}
