using Chinook.Data.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chinook.Data.Configurations
{
    public class MediaTypeConfiguration
    {
        public MediaTypeConfiguration(EntityTypeBuilder<MediaType> entity)
        {
            entity.Property(e => e.Name).HasMaxLength(120);
        }
    }
}
