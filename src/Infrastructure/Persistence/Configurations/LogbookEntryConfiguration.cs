using Domain.Entities;
using Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LogbookEntryConfiguration : IEntityTypeConfiguration<LogbookEntry>
{
    public void Configure(EntityTypeBuilder<LogbookEntry> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Body)
            .IsRequired();

        builder.HasData(LogbookEntriesSeed.Seed());
    }
}