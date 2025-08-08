using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(s => s.SaleNumber)
                .HasColumnName("SaleNumber")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(s => s.Date)
                .HasColumnName("Date")
                .IsRequired();

            builder.Property(s => s.Cancelled)
                .HasColumnName("Cancelled")
                .IsRequired();

            // Configuração do Value Object Customer usando OwnsOne
            builder.OwnsOne(s => s.Customer, customer =>
            {
                customer.Property(c => c.Id)
                    .HasColumnName("CustomerId")
                    .IsRequired();

                customer.Property(c => c.Name)
                    .HasColumnName("CustomerName")
                    .HasMaxLength(200)
                    .IsRequired();

                // Criar índice dentro da configuração do OwnsOne
                customer.HasIndex(c => c.Id)
                    .HasDatabaseName("IX_Sales_CustomerId");
            });

            // Configuração do Value Object Branch usando OwnsOne
            builder.OwnsOne(s => s.Branch, branch =>
            {
                branch.Property(b => b.Id)
                    .HasColumnName("BranchId")
                    .IsRequired();

                branch.Property(b => b.Description)
                    .HasColumnName("BranchDescription")
                    .HasMaxLength(200)
                    .IsRequired();

                // Criar índice dentro da configuração do OwnsOne
                branch.HasIndex(b => b.Id)
                    .HasDatabaseName("IX_Sales_BranchId");
            });

            // Configuração da propriedade calculada Total (ignorada no banco)
            builder.Ignore(s => s.Total);

            // Configuração da coleção de itens
            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do backing field para a coleção privada
            var navigation = builder.Metadata.FindNavigation(nameof(Sale.Items));
            if (navigation != null)
            {
                navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            }

            // Índices básicos
            builder.HasIndex(s => s.SaleNumber)
                .IsUnique();

            builder.HasIndex(s => s.Date);
        }
    }
}
