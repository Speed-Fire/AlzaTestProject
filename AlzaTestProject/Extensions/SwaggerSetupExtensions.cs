using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AlzaTestProject.Extensions
{
	public static partial class SwaggerSetupExtensions
	{
		#region AddSwaggerGen

		public static IServiceCollection AddSetupedSwaggerGen(this IServiceCollection services,
			WebApplicationBuilder builder)
		{
			services.AddSwaggerGen(opts =>
			{
				SetupSwaggerVersioning(opts, builder);
				SetupSwaggerMethodComments(opts);
				SetupControllerNaming(opts);
			});

			return services;
		}

		private static void SetupControllerNaming(SwaggerGenOptions opts)
		{
			opts.TagActionsBy(api =>
			{
				var cad = api.ActionDescriptor as ControllerActionDescriptor;
				var name = cad?.ControllerTypeInfo.Name.Replace("Controller", "") ?? "Unknown";

				name = VersionDropRegex().Replace(name, "");
				return [name];
			});
		}

		private static void SetupSwaggerVersioning(SwaggerGenOptions options,
			WebApplicationBuilder builder)
		{
			var provider = builder.Services.BuildServiceProvider()
					 .GetRequiredService<IApiVersionDescriptionProvider>();

			foreach(var description in provider.ApiVersionDescriptions)
			{
				SetupSwaggerInfo(description, options);
			}
		}

		private static void SetupSwaggerMethodComments(SwaggerGenOptions options)
		{
			var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
			options.IncludeXmlComments(xmlpath, true);
		}

		private static void SetupSwaggerInfo(ApiVersionDescription versionDescription, SwaggerGenOptions options)
		{
			options.SwaggerDoc(versionDescription.GroupName, new()
			{
				Title = $"Alza test API {versionDescription.ApiVersion}",
				Version = versionDescription.ApiVersion.ToString(),
				Description = "Contains API from alza case study.",
				TermsOfService = new("https://example.com/terms"),
				Contact = new()
				{
					Name = "Vladislav Sidorovich",
					Email = "email",
					Url = new("https://github.com/Speed-Fire/AlzaTestProject")
				},
				License = new()
				{
					Name = "Alza test API LICX",
					Url = new("https://example.com/license")
				}
			});
		}

		#endregion

		#region UseSwaggerUI

		public static void UseSetupedSwaggerUI(this WebApplication app)
		{
			var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

			app.UseSwaggerUI(opts =>
			{
				opts.RoutePrefix = "swagger";
				foreach (var description in provider.ApiVersionDescriptions)
				{
					opts.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
				}
			});
		}

		[GeneratedRegex(@"V\d+$", RegexOptions.IgnoreCase, "ru-RU")]
		private static partial Regex VersionDropRegex();

		#endregion
	}
}
