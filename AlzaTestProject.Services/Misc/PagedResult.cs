using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Misc
{
	public class PagedResult<TDto>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }
		public IEnumerable<TDto> Items { get; set; } = [];
	}
}
