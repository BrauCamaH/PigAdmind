using WpfApp1.Core;
using WpfApp1.Core.Repositories;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance.Repositories;

namespace WpfApp1.Persistance
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly Entities _context;

        public UnitOfWork(Entities context)
        {
            _context = context;

            Births = new BirthsRepository(_context);
            Females = new FemalesRepository(_context);
            Inseminations = new InseminationsRepository(_context);
            Births = new BirthsRepository(_context);
            Sicks = new SicksRepository(_context);
            Groups = new GroupsRepository(_context);
            Sales = new SalesRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IFemalesRepository Females { get; private set; }
        public IInseminationsRepository Inseminations { get; private set; }
        public IBirthsRepository Births { get; private set; }
        public ISicksRepository Sicks { get; private set; }
        public IGroupsRepository Groups { get; private set; }

        public ISalesRepository Sales { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
