using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance.Repositories;

namespace WpfApp1.Persistance
{
    class GroupsRepository : Repository<PigGroups>, IGroupsRepository
    {
        public GroupsRepository(DbContext cont) : base(cont)
        {
        }

        public IEnumerable<PigGroups> GetGroupsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public PigGroups GetGroupById(int id)
        {
            return DbEntities.PigGroups.FirstOrDefault(g => g.id == id);
        }

        public void RemoveGroupById(int id)
        {
            var query = from p in DbEntities.PigGroups
                        where p.id == id
                        select p;
            if (query.First() != null)
            {
                DbEntities.PigGroups.Remove(query.FirstOrDefault() ?? throw new InvalidOperationException());
            }
        }

        public Entities DbEntities => Context as Entities;
    }
}
