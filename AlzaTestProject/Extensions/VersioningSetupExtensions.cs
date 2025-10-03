namespace AlzaTestProject.Extensions
{
	public static class VersioningSetupExtensions
	{
		public static IServiceCollection AddSetupedApiVersioning(this IServiceCollection services)
		{
			services
				.AddApiVersioning(opts =>
				{
					opts.AssumeDefaultVersionWhenUnspecified = true;
					opts.DefaultApiVersion = new(1, 0);
					opts.ReportApiVersions = true;
				})
				.AddApiExplorer(opts =>
				{
					opts.GroupNameFormat = "'v'VVV";
					opts.SubstituteApiVersionInUrl = true;
				});

			return services;
		}
	}
}
