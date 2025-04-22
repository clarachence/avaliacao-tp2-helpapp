using HelpApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpApp.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(x => x.Stock)
            .IsRequired();

            builder.Property(x => x.Image)
           .HasMaxLength(255);


            builder.Property(x => x.CategoryId)
           .HasMaxLength(255);

            builder.HasOne(x => x.Category).WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Product(1, "Cerno da amiseta", "Camiseta da Nike", 200.90m, 100, "camiseta.jpg") { CategoryId = 3 },
                new Product(2, "Tablet", "Tablet Sansung s6 Lite", 49.90m, 200, "tablet.jpg") { CategoryId = 2 },
                new Product(3, "Caderno", "CadTilibra", 25.00m, 50, "smartphone.jpg") { CategoryId = 1 });
        }
    }
}
