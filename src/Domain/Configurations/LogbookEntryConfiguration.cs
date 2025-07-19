using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class LogbookEntryConfiguration : IEntityTypeConfiguration<LogbookEntry>
{
    public void Configure(EntityTypeBuilder<LogbookEntry> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Body)
            .IsRequired();
    }
}