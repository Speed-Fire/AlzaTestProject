using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	public interface IRepository<T>
	{
		Task<int> Count(ISpecification specification, CancellationToken cancellationToken);
		Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
		Task<IEnumerable<T>> GetAll(ISpecification specification, CancellationToken cancellationToken = default);
		Task<T?> GetById(int id, CancellationToken cancellationToken = default);
		Task<bool> Exists(ISpecification specification, CancellationToken cancellationToken = default);

		T Add(T item);
		Task<T> Update(T item, CancellationToken cancellationToken = default);
		void Delete(T item);
	}
}
