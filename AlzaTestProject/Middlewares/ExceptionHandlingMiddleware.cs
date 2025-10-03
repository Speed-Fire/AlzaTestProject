using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace AlzaTestProject.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;
		private readonly IHostEnvironment _env;

		private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception");

				await HandleExceptionAsync(context, ex, _env);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var problem = new ProblemDetails
			{
				Status = context.Response.StatusCode,
				Title = "An unexpected error occurred",
				Detail = env.IsDevelopment() ? ex.ToString() : "Internal server error",
				Instance = context.Request.Path
			};

			var json = JsonSerializer.Serialize(problem, _jsonSerializerOptions);

			await context.Response.WriteAsync(json);
		}
	}
}
