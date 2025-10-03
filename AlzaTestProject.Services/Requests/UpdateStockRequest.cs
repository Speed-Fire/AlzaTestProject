using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Requests
{
	public record UpdateStockRequest(int ProductId, int NewStock);
}
