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
		Task<IEnumerable<T>> GetAll(ISpecification specification);
		Task<T?> GetById(int id);
		Task<bool> Exists(ISpecification specification);

		T Add(T item);
		Task<T> Update(T item);
		void Delete(T item);
	}
}
