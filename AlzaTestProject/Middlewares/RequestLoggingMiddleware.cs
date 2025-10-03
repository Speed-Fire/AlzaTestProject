using System.Diagnostics;

namespace AlzaTestProject.Middlewares
{
	public static class RequestLoggingMiddleware
	{
		/// <summary>
		/// Adds middleware for logging request info
		/// </summary>
		/// <param name="app"></param>
		public static void UseRequestLogging(this WebApplication app)
		{
			app.Use(async (context, next) =>
			{
				var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
				logger.LogInformation("HTTP {Method} {Path} started", context.Request.Method, context.Request.Path);
				var sw = Stopwatch.StartNew();
				
				await next();
				
				sw.Stop();
				
				logger.LogInformation("HTTP {Method} {Path} finished with {StatusCode} in {Elapsed}ms",
					context.Request.Method,
					context.Request.Path,
					context.Response.StatusCode,
					sw.ElapsedMilliseconds);
			});
		}
	}
}
