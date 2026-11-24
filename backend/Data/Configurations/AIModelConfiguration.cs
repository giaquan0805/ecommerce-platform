using ecommerce_platform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce_platform.Data.Configurations
{
    public class AIModelConfiguration
        : IEntityTypeConfiguration<AIModel>
    {
        public void Configure(EntityTypeBuilder<AIModel> builder)
        {
            builder.ToTable("AIModels");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(m => m.Provider)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.ModelCode)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(m => m.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(m => new
            {
                m.Provider,
                m.ModelCode
            }).IsUnique();
        }
    }
}