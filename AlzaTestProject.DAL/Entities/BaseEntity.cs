using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Entities
{
	internal abstract class BaseEntity
	{
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
	}
}
