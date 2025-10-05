
using AlzaTestProject.DAL.Contextes;
using AlzaTestProject.DAL.Extensions;
using AlzaTestProject.Domain.Requests;
using AlzaTestProject.Extensions;
using AlzaTestProject.Infrastructure.Extensions;
using AlzaTestProject.Middlewares;
using AlzaTestProject.Services.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AlzaTestProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
			builder.Services.AddDAL(contextBuilder =>
			{
				contextBuilder.UseSqlite(GetDbConnectionString(builder));
			});
            builder.Services.AddAlzaTestProjectServices();
            builder.Services.AddStockUpdateWorker();

            if (args.Contains("--use-kafka", StringComparer.OrdinalIgnoreCase))
            {
				builder.Services.AddKafka(builder.Configuration);

				builder.Services.AddKafkaQueue<UpdateStockRequest>(builder.Configuration, "UpdateStock");
			}

			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSetupedApiVersioning();
            builder.Services.AddSetupedSwaggerGen(builder);

            var app = builder.Build();

            // applying migrations
			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				db.Database.Migrate(); 
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSetupedSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRequestLogging();

			app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static string GetDbConnectionString(WebApplicationBuilder builder)
        {
            var constr = builder.Configuration
                .GetConnectionString("DefaultConnection")!;

            if (string.IsNullOrWhiteSpace(constr))
                throw new InvalidOperationException("Database connection string is not found.");

            return constr;
        }
    }
}
