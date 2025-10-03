using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Domain.Abstract
{
	public interface IProductSpecificationFactory
	{
		ISpecification ExistsByNameSpecification(string name);
		ISpecification GetAllPagedSpecificatyion(int page, int pageSize);
		ISpecification NoFilterSpecification();
	}
}
