using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping.Files
{
    class FilesMapping : DataMapper<File>
    {
        public override void Map(EntityTypeBuilder<File> builder)
        {
            builder.Property(f => f.FullPath).IsRequired();
            builder.Property(f => f.OriginalName).IsRequired();
            builder.HasOne(f => f.User).WithOne().HasForeignKey<File>(f => f.UserId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.ClientNoAction);
            base.Map(builder);
        }
    }
}
