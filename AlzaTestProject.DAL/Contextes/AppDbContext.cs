using AlzaTestProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlzaTestProject.DAL.Contextes
{
	public class AppDbContext : DbContext
	{
		internal DbSet<ProductEntity> Products => Set<ProductEntity>();

		public AppDbContext() { }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<ProductEntity>()
				.HasAlternateKey(p => p.Name);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entities = ChangeTracker
				.Entries<BaseEntity>()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

			var now = DateTime.UtcNow;

			foreach(var entity in entities)
			{
				if(entity.State == EntityState.Added)
				{
					entity.Entity.Created = now;
					entity.Entity.Updated = now;
				}
				else
				{
					// protection of updating creation time
					entity.Property(p => p.Created).IsModified = false;
					entity.Entity.Updated = now;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
