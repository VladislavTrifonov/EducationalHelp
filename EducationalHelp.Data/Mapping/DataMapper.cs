using EducationalHelp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping
{
    public abstract class DataMapper<Entity> where Entity : BaseEntity
    {
        public virtual void Map(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CreatedAt).HasDefaultValueSql("getutcdate()").IsRequired();
            builder.Property(t => t.UpdatedAt).HasDefaultValue(DateTime.MinValue);
            builder.Property(t => t.DeletedAt).HasDefaultValue(DateTime.MinValue);
        }
    }
}
