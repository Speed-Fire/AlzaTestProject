using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AlzaTestProject.Extensions
{
	public static class SwaggerSetupExtensions
	{
		public static IServiceCollection AddSetupedSwaggerGen(this IServiceCollection services)
		{
			services.AddSwaggerGen(SetupSwagger);

			return services;
		}

		private static void SetupSwagger(SwaggerGenOptions options)
		{
			SetupSwaggerInfo(options);
			SetupSwaggerMethodComments(options);
		}

		private static void SetupSwaggerMethodComments(SwaggerGenOptions options)
		{
			var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
			options.IncludeXmlComments(xmlpath);
		}

		private static void SetupSwaggerInfo(SwaggerGenOptions options)
		{
			options.SwaggerDoc("v1", new()
			{
				Title = "Alza test API",
				Description = "Contains API from alza case study.",
				TermsOfService = new("https://example.com/terms"),
				Contact = new()
				{
					Name = "Vladislav Sidorovich",
					Email = "vladsidor6730@gmail.com",
					Url = new("https://github.com/Speed-Fire/AlzaTestProject")
				},
				License = new()
				{
					Name = "Alza test API LICX",
					Url = new("https://example.com/license")
				}
			});
		}
	}
}
