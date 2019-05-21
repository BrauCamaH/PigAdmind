using System.Collections;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Core.Repositories
{
    interface IGroupsRepository
    {
        IEnumerable GetFemalesByUser(int id);
        PigGroups GetGroupById(int id);

        void RemoveGroupById(int id);
    }
}
