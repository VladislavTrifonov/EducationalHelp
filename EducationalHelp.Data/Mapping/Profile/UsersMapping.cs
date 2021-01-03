using System;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalHelp.Data.Mapping.Profile
{
    public class UsersMapping : DataMapper<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Pseudonym).HasMaxLength(150).IsRequired();
            builder.Property(u => u.Login).HasMaxLength(150).IsRequired();
            builder.Property(u => u.PassHash).IsRequired();
            base.Map(builder);
        }
    }
}
