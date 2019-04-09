using WpfApp1.Core;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Persistance
{
	class UnitOfWork : IUnitOfWork
	{
		private readonly Entities _context;

		public UnitOfWork(Entities context)
		{
			context = _context;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public int Complete()
		{
			return _context.SaveChanges();
		}


	}
}
