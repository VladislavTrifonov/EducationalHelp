using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping.Files
{
    public class UserFilesMapping : DataMapper<UserFiles>
    {
        public override void Map(EntityTypeBuilder<UserFiles> builder)
        {
            builder.HasKey(uf => new { uf.UserId, uf.FileId });

            builder.HasOne(uf => uf.User)
                .WithMany(u => u.UserFiles)
                .HasForeignKey(uf => uf.UserId);

            builder.HasOne(uf => uf.File)
                .WithMany(f => f.UserFiles)
                .HasForeignKey(uf => uf.FileId);

            builder.Property(uf => uf.Type).IsRequired();

            base.Map(builder);
        }
    }
}
