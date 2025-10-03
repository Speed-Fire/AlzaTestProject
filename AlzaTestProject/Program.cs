
using AlzaTestProject.DAL.Contextes;
using AlzaTestProject.DAL.Extensions;
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
				contextBuilder.UseSqlite(builder.Configuration
					.GetConnectionString("DefaultConnection"));
			});
            builder.Services.AddAlzaTestProjectServices();

			builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
