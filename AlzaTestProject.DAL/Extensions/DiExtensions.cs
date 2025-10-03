using AlzaTestProject.DAL.Contextes;
using AlzaTestProject.DAL.Repositories;
using AlzaTestProject.DAL.Specifications.Factories;
using AlzaTestProject.Domain.Abstract;
using AlzaTestProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Extensions
{
	public static class DiExtensions
	{
		public static IServiceCollection AddDAL(this IServiceCollection services,
			Action<DbContextOptionsBuilder> contextOptionsAction)
		{
			services
				.AddDbContext<AppDbContext>(contextOptionsAction)
				.AddScoped<IRepository<Product>, ProductsRepository>()
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddSingleton<IProductSpecificationFactory, ProductSpecificationFactory>();

			return services;
		}
	}
}
