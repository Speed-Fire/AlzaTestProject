using AlzaTestProject.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.Services.Extensions
{
	public static class DIExtensions
	{
		public static IServiceCollection AddAlzaTestProjectServices(this IServiceCollection services)
		{
			services.AddTransient<IProductService, ProductService>();

			return services;
		}
	}
}
