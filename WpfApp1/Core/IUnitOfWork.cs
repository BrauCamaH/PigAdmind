using System;
using WpfApp1.Core.Repositories;

namespace WpfApp1.Core
{
    interface IUnitOfWork : IDisposable
    {
        IFemalesRepository Females { get; }
        IInseminationsRepository Inseminations { get; }
        IBirthsRepository Births { get; }
        ISicksRepository Sicks { get; }

        IGroupsRepository Groups { get; }

        ISalesRepository Sales { get; }

        IWeaningRepository Weaning { get; }

        int Complete();
    }
}
