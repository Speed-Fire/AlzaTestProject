using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAll();
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
		Task<T> GetById(int id);

		Task<T> Create(T item);
		Task<bool> Update(T item);
		Task<bool> Delete(T item);
	}
}
