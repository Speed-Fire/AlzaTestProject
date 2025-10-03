using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AlzaTestProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.UniqueConstraint("AK_Products_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Created", "Description", "ImageUrl", "Name", "Price", "Stock", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-performance laptop for professionals", "https://example.com/images/product1.jpg", "Laptop Pro 15", 1299.99m, 25, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ergonomic wireless mouse", "https://example.com/images/product2.jpg", "Wireless Mouse", 49.99m, 100, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "RGB mechanical keyboard", "https://example.com/images/product3.jpg", "Mechanical Keyboard", 89.99m, 50, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "High refresh rate monitor", "https://example.com/images/product4.jpg", "Gaming Monitor 27\"", 399.99m, 30, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Multiport USB-C hub", "https://example.com/images/product5.jpg", "USB-C Hub", 29.99m, 75, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless over-ear headphones", "https://example.com/images/product6.jpg", "Noise-Cancelling Headphones", 199.99m, 40, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Latest model with OLED display", "https://example.com/images/product7.jpg", "Smartphone X", 999.99m, 60, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "10-inch tablet with stylus support", "https://example.com/images/product8.jpg", "Tablet S", 499.99m, 35, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fast portable storage", "https://example.com/images/product9.jpg", "External SSD 1TB", 149.99m, 80, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "1080p USB webcam", "https://example.com/images/product10.jpg", "Webcam HD", 69.99m, 55, new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portable wireless speaker", "https://example.com/images/product11.jpg", "Bluetooth Speaker", 89.99m, 70, new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fitness smartwatch with heart rate monitor", "https://example.com/images/product12.jpg", "Smartwatch X", 199.99m, 40, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-capacity power bank", "https://example.com/images/product13.jpg", "Portable Charger 20000mAh", 59.99m, 90, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "4K waterproof action camera", "https://example.com/images/product14.jpg", "Action Camera", 249.99m, 20, new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "True wireless earbuds with charging case", "https://example.com/images/product15.jpg", "Wireless Earbuds", 129.99m, 65, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ergonomic gaming chair with lumbar support", "https://example.com/images/product16.jpg", "Gaming Chair", 299.99m, 15, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drawing tablet with pen support", "https://example.com/images/product17.jpg", "Graphics Tablet", 179.99m, 30, new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi controllable LED bulb", "https://example.com/images/product18.jpg", "Smart Light Bulb", 24.99m, 120, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dual-band Wi-Fi router", "https://example.com/images/product19.jpg", "Router AC1200", 79.99m, 45, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi smart thermostat for home", "https://example.com/images/product20.jpg", "Smart Thermostat", 199.99m, 25, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rechargeable electric toothbrush", "https://example.com/images/product21.jpg", "Electric Toothbrush", 59.99m, 75, new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Automatic coffee maker with timer", "https://example.com/images/product22.jpg", "Coffee Maker", 129.99m, 40, new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HEPA air purifier for home", "https://example.com/images/product23.jpg", "Air Purifier", 149.99m, 30, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smart robot vacuum cleaner", "https://example.com/images/product24.jpg", "Robot Vacuum", 299.99m, 20, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Waterproof fitness band with heart rate monitor", "https://example.com/images/product25.jpg", "Fitness Tracker", 99.99m, 60, new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portable HD projector for home theater", "https://example.com/images/product26.jpg", "HD Projector", 399.99m, 15, new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keyless entry door lock with app control", "https://example.com/images/product27.jpg", "Smart Door Lock", 199.99m, 25, new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mirrorless camera with 24MP sensor", "https://example.com/images/product28.jpg", "Digital Camera", 899.99m, 18, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portable external hard drive", "https://example.com/images/product29.jpg", "External Hard Drive 2TB", 99.99m, 50, new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi controlled smart plug", "https://example.com/images/product30.jpg", "Smart Plug", 19.99m, 150, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lightweight laptop for everyday use", "https://example.com/images/product31.jpg", "Laptop Air 13", 999.99m, 40, new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Slim wireless keyboard", "https://example.com/images/product32.jpg", "Wireless Keyboard", 69.99m, 60, new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-speed HDMI cable", "https://example.com/images/product33.jpg", "HDMI Cable 2m", 14.99m, 200, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Protective smartphone case", "https://example.com/images/product34.jpg", "Smartphone Case", 29.99m, 120, new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adjustable aluminum laptop stand", "https://example.com/images/product35.jpg", "Laptop Stand", 49.99m, 55, new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-DPI gaming mouse", "https://example.com/images/product36.jpg", "Gaming Mouse", 59.99m, 65, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virtual reality headset for gaming", "https://example.com/images/product37.jpg", "VR Headset", 399.99m, 20, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bluetooth smart scale for weight tracking", "https://example.com/images/product38.jpg", "Smart Scale", 79.99m, 35, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "14-inch portable USB-C monitor", "https://example.com/images/product39.jpg", "Portable Monitor", 199.99m, 25, new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wi-Fi security camera with motion detection", "https://example.com/images/product40.jpg", "Smart Camera", 149.99m, 30, new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "USB-C docking station for laptops", "https://example.com/images/product41.jpg", "Laptop Docking Station", 179.99m, 20, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fast wireless charging pad", "https://example.com/images/product42.jpg", "Wireless Charger", 39.99m, 80, new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless Bluetooth headset with mic", "https://example.com/images/product43.jpg", "Bluetooth Headset", 69.99m, 55, new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
