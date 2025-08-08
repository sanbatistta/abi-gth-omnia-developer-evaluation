using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(si => si.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Configuração do Value Object Product usando OwnsOne
            builder.OwnsOne(si => si.Product, product =>
            {
                product.Property(p => p.Id)
                    .HasColumnName("ProductId")
                    .IsRequired();

                product.Property(p => p.Name)
                    .HasColumnName("ProductName")
                    .HasMaxLength(200)
                    .IsRequired();

                // Criar índice dentro da configuração do OwnsOne
                product.HasIndex(p => p.Id)
                    .HasDatabaseName("IX_SaleItems_ProductId");
            });

            // Propriedades calculadas (ignoradas no banco)
            builder.Ignore(si => si.Discount);
            builder.Ignore(si => si.Total);

            // Configuração da chave estrangeira para Sale
            builder.Property<Guid>("SaleId")
                .HasColumnName("SaleId")
                .IsRequired();

            // Índice na chave estrangeira
            builder.HasIndex("SaleId");
        }
    }
}
