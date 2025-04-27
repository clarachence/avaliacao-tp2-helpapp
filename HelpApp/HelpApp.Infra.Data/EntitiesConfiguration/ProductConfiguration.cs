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
           .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new
                {
                    Id = 1,
                    Name = "Camiseta",
                    Description = "Camiseta da Nike",
                    Price = 200.90m,
                    Stock = 100,
                    Image = "camiseta.jpg",
                    CategoryId = 3
                },
                 new
                 {
                     Id = 2,
                     Name = "Tablet",
                     Description = "Tablet Sansung s6 Lite",
                     Price = 29.90m,
                     Stock = 200,
                     Image = "tablet.jpg",
                     CategoryId = 2
                 },
                  new
                  {
                      Id = 3,
                      Name = "Caderno",
                      Description = "Caderno Tilibra",
                      Price = 24.00m,
                      Stock = 50,
                      Image = "caderno.jpg",
                      CategoryId = 1
                  }
                  );
             
              
        }
    }
}
