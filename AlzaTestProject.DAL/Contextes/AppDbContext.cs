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

			modelBuilder.Entity<ProductEntity>()
				.HasData(
					new ProductEntity { Id = 1, Name = "Laptop Pro 15", Description = "High-performance laptop for professionals", ImageUrl = "https://example.com/images/product1.jpg", Price = 1299.99m, Stock = 25, Created = new DateTime(2025, 1, 10), Updated = new DateTime(2025, 1, 12) },
					new ProductEntity { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", ImageUrl = "https://example.com/images/product2.jpg", Price = 49.99m, Stock = 100, Created = new DateTime(2025, 1, 11), Updated = new DateTime(2025, 1, 15) },
					new ProductEntity { Id = 3, Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", ImageUrl = "https://example.com/images/product3.jpg", Price = 89.99m, Stock = 50, Created = new DateTime(2025, 1, 12), Updated = new DateTime(2025, 1, 16) },
					new ProductEntity { Id = 4, Name = "Gaming Monitor 27\"", Description = "High refresh rate monitor", ImageUrl = "https://example.com/images/product4.jpg", Price = 399.99m, Stock = 30, Created = new DateTime(2025, 1, 13), Updated = new DateTime(2025, 1, 17) },
					new ProductEntity { Id = 5, Name = "USB-C Hub", Description = "Multiport USB-C hub", ImageUrl = "https://example.com/images/product5.jpg", Price = 29.99m, Stock = 75, Created = new DateTime(2025, 1, 14), Updated = new DateTime(2025, 1, 18) },
					new ProductEntity { Id = 6, Name = "Noise-Cancelling Headphones", Description = "Wireless over-ear headphones", ImageUrl = "https://example.com/images/product6.jpg", Price = 199.99m, Stock = 40, Created = new DateTime(2025, 1, 15), Updated = new DateTime(2025, 1, 19) },
					new ProductEntity { Id = 7, Name = "Smartphone X", Description = "Latest model with OLED display", ImageUrl = "https://example.com/images/product7.jpg", Price = 999.99m, Stock = 60, Created = new DateTime(2025, 1, 16), Updated = new DateTime(2025, 1, 20) },
					new ProductEntity { Id = 8, Name = "Tablet S", Description = "10-inch tablet with stylus support", ImageUrl = "https://example.com/images/product8.jpg", Price = 499.99m, Stock = 35, Created = new DateTime(2025, 1, 17), Updated = new DateTime(2025, 1, 21) },
					new ProductEntity { Id = 9, Name = "External SSD 1TB", Description = "Fast portable storage", ImageUrl = "https://example.com/images/product9.jpg", Price = 149.99m, Stock = 80, Created = new DateTime(2025, 1, 18), Updated = new DateTime(2025, 1, 22) },
					new ProductEntity { Id = 10, Name = "Webcam HD", Description = "1080p USB webcam", ImageUrl = "https://example.com/images/product10.jpg", Price = 69.99m, Stock = 55, Created = new DateTime(2025, 1, 19), Updated = new DateTime(2025, 1, 23) },
					new ProductEntity { Id = 11, Name = "Bluetooth Speaker", Description = "Portable wireless speaker", ImageUrl = "https://example.com/images/product11.jpg", Price = 89.99m, Stock = 70, Created = new DateTime(2025, 1, 20), Updated = new DateTime(2025, 1, 24) },
					new ProductEntity { Id = 12, Name = "Smartwatch X", Description = "Fitness smartwatch with heart rate monitor", ImageUrl = "https://example.com/images/product12.jpg", Price = 199.99m, Stock = 40, Created = new DateTime(2025, 1, 21), Updated = new DateTime(2025, 1, 25) },
					new ProductEntity { Id = 13, Name = "Portable Charger 20000mAh", Description = "High-capacity power bank", ImageUrl = "https://example.com/images/product13.jpg", Price = 59.99m, Stock = 90, Created = new DateTime(2025, 1, 22), Updated = new DateTime(2025, 1, 26) },
					new ProductEntity { Id = 14, Name = "Action Camera", Description = "4K waterproof action camera", ImageUrl = "https://example.com/images/product14.jpg", Price = 249.99m, Stock = 20, Created = new DateTime(2025, 1, 23), Updated = new DateTime(2025, 1, 27) },
					new ProductEntity { Id = 15, Name = "Wireless Earbuds", Description = "True wireless earbuds with charging case", ImageUrl = "https://example.com/images/product15.jpg", Price = 129.99m, Stock = 65, Created = new DateTime(2025, 1, 24), Updated = new DateTime(2025, 1, 28) },
					new ProductEntity { Id = 16, Name = "Gaming Chair", Description = "Ergonomic gaming chair with lumbar support", ImageUrl = "https://example.com/images/product16.jpg", Price = 299.99m, Stock = 15, Created = new DateTime(2025, 1, 25), Updated = new DateTime(2025, 1, 29) },
					new ProductEntity { Id = 17, Name = "Graphics Tablet", Description = "Drawing tablet with pen support", ImageUrl = "https://example.com/images/product17.jpg", Price = 179.99m, Stock = 30, Created = new DateTime(2025, 1, 26), Updated = new DateTime(2025, 1, 30) },
					new ProductEntity { Id = 18, Name = "Smart Light Bulb", Description = "Wi-Fi controllable LED bulb", ImageUrl = "https://example.com/images/product18.jpg", Price = 24.99m, Stock = 120, Created = new DateTime(2025, 1, 27), Updated = new DateTime(2025, 1, 31) },
					new ProductEntity { Id = 19, Name = "Router AC1200", Description = "Dual-band Wi-Fi router", ImageUrl = "https://example.com/images/product19.jpg", Price = 79.99m, Stock = 45, Created = new DateTime(2025, 1, 28), Updated = new DateTime(2025, 2, 1) },
					new ProductEntity { Id = 20, Name = "Smart Thermostat", Description = "Wi-Fi smart thermostat for home", ImageUrl = "https://example.com/images/product20.jpg", Price = 199.99m, Stock = 25, Created = new DateTime(2025, 1, 29), Updated = new DateTime(2025, 2, 2) },
					new ProductEntity { Id = 21, Name = "Electric Toothbrush", Description = "Rechargeable electric toothbrush", ImageUrl = "https://example.com/images/product21.jpg", Price = 59.99m, Stock = 75, Created = new DateTime(2025, 1, 30), Updated = new DateTime(2025, 2, 3) },
					new ProductEntity { Id = 22, Name = "Coffee Maker", Description = "Automatic coffee maker with timer", ImageUrl = "https://example.com/images/product22.jpg", Price = 129.99m, Stock = 40, Created = new DateTime(2025, 1, 31), Updated = new DateTime(2025, 2, 4) },
					new ProductEntity { Id = 23, Name = "Air Purifier", Description = "HEPA air purifier for home", ImageUrl = "https://example.com/images/product23.jpg", Price = 149.99m, Stock = 30, Created = new DateTime(2025, 2, 1), Updated = new DateTime(2025, 2, 5) },
					new ProductEntity { Id = 24, Name = "Robot Vacuum", Description = "Smart robot vacuum cleaner", ImageUrl = "https://example.com/images/product24.jpg", Price = 299.99m, Stock = 20, Created = new DateTime(2025, 2, 2), Updated = new DateTime(2025, 2, 6) },
					new ProductEntity { Id = 25, Name = "Fitness Tracker", Description = "Waterproof fitness band with heart rate monitor", ImageUrl = "https://example.com/images/product25.jpg", Price = 99.99m, Stock = 60, Created = new DateTime(2025, 2, 3), Updated = new DateTime(2025, 2, 7) },
					new ProductEntity { Id = 26, Name = "HD Projector", Description = "Portable HD projector for home theater", ImageUrl = "https://example.com/images/product26.jpg", Price = 399.99m, Stock = 15, Created = new DateTime(2025, 2, 4), Updated = new DateTime(2025, 2, 8) },
					new ProductEntity { Id = 27, Name = "Smart Door Lock", Description = "Keyless entry door lock with app control", ImageUrl = "https://example.com/images/product27.jpg", Price = 199.99m, Stock = 25, Created = new DateTime(2025, 2, 5), Updated = new DateTime(2025, 2, 9) },
					new ProductEntity { Id = 28, Name = "Digital Camera", Description = "Mirrorless camera with 24MP sensor", ImageUrl = "https://example.com/images/product28.jpg", Price = 899.99m, Stock = 18, Created = new DateTime(2025, 2, 6), Updated = new DateTime(2025, 2, 10) },
					new ProductEntity { Id = 29, Name = "External Hard Drive 2TB", Description = "Portable external hard drive", ImageUrl = "https://example.com/images/product29.jpg", Price = 99.99m, Stock = 50, Created = new DateTime(2025, 2, 7), Updated = new DateTime(2025, 2, 11) },
					new ProductEntity { Id = 30, Name = "Smart Plug", Description = "Wi-Fi controlled smart plug", ImageUrl = "https://example.com/images/product30.jpg", Price = 19.99m, Stock = 150, Created = new DateTime(2025, 2, 8), Updated = new DateTime(2025, 2, 12) },
					new ProductEntity { Id = 31, Name = "Laptop Air 13", Description = "Lightweight laptop for everyday use", ImageUrl = "https://example.com/images/product31.jpg", Price = 999.99m, Stock = 40, Created = new DateTime(2025, 2, 9), Updated = new DateTime(2025, 2, 13) },
					new ProductEntity { Id = 32, Name = "Wireless Keyboard", Description = "Slim wireless keyboard", ImageUrl = "https://example.com/images/product32.jpg", Price = 69.99m, Stock = 60, Created = new DateTime(2025, 2, 10), Updated = new DateTime(2025, 2, 14) },
					new ProductEntity { Id = 33, Name = "HDMI Cable 2m", Description = "High-speed HDMI cable", ImageUrl = "https://example.com/images/product33.jpg", Price = 14.99m, Stock = 200, Created = new DateTime(2025, 2, 11), Updated = new DateTime(2025, 2, 15) },
					new ProductEntity { Id = 34, Name = "Smartphone Case", Description = "Protective smartphone case", ImageUrl = "https://example.com/images/product34.jpg", Price = 29.99m, Stock = 120, Created = new DateTime(2025, 2, 12), Updated = new DateTime(2025, 2, 16) },
					new ProductEntity { Id = 35, Name = "Laptop Stand", Description = "Adjustable aluminum laptop stand", ImageUrl = "https://example.com/images/product35.jpg", Price = 49.99m, Stock = 55, Created = new DateTime(2025, 2, 13), Updated = new DateTime(2025, 2, 17) },
					new ProductEntity { Id = 36, Name = "Gaming Mouse", Description = "High-DPI gaming mouse", ImageUrl = "https://example.com/images/product36.jpg", Price = 59.99m, Stock = 65, Created = new DateTime(2025, 2, 14), Updated = new DateTime(2025, 2, 18) },
					new ProductEntity { Id = 37, Name = "VR Headset", Description = "Virtual reality headset for gaming", ImageUrl = "https://example.com/images/product37.jpg", Price = 399.99m, Stock = 20, Created = new DateTime(2025, 2, 15), Updated = new DateTime(2025, 2, 19) },
					new ProductEntity { Id = 38, Name = "Smart Scale", Description = "Bluetooth smart scale for weight tracking", ImageUrl = "https://example.com/images/product38.jpg", Price = 79.99m, Stock = 35, Created = new DateTime(2025, 2, 16), Updated = new DateTime(2025, 2, 20) },
					new ProductEntity { Id = 39, Name = "Portable Monitor", Description = "14-inch portable USB-C monitor", ImageUrl = "https://example.com/images/product39.jpg", Price = 199.99m, Stock = 25, Created = new DateTime(2025, 2, 17), Updated = new DateTime(2025, 2, 21) },
					new ProductEntity { Id = 40, Name = "Smart Camera", Description = "Wi-Fi security camera with motion detection", ImageUrl = "https://example.com/images/product40.jpg", Price = 149.99m, Stock = 30, Created = new DateTime(2025, 2, 18), Updated = new DateTime(2025, 2, 22) },
					new ProductEntity { Id = 41, Name = "Laptop Docking Station", Description = "USB-C docking station for laptops", ImageUrl = "https://example.com/images/product41.jpg", Price = 179.99m, Stock = 20, Created = new DateTime(2025, 2, 19), Updated = new DateTime(2025, 2, 23) },
					new ProductEntity { Id = 42, Name = "Wireless Charger", Description = "Fast wireless charging pad", ImageUrl = "https://example.com/images/product42.jpg", Price = 39.99m, Stock = 80, Created = new DateTime(2025, 2, 20), Updated = new DateTime(2025, 2, 24) },
					new ProductEntity { Id = 43, Name = "Bluetooth Headset", Description = "Wireless Bluetooth headset with mic", ImageUrl = "https://example.com/images/product43.jpg", Price = 69.99m, Stock = 55, Created = new DateTime(2025, 2, 21), Updated = new DateTime(2025, 2, 25) }
				);
		}

		/// <summary>
		/// Saves all changes made in this context to the database asynchronously.
		/// Automatically sets the <see cref="BaseEntity.Created"/> and <see cref="BaseEntity.Updated"/> 
		/// timestamps for entities being added or modified.
		/// </summary>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> to observe while waiting for the task to complete. 
		/// Defaults to <see cref="CancellationToken.None"/>.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous save operation. The task result contains 
		/// the number of state entries written to the database.
		/// </returns>
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
