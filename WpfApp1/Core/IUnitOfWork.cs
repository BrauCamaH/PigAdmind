using System;

namespace WpfApp1.Core
{
	interface IUnitOfWork : IDisposable
	{
		int Complete();
	}
}
