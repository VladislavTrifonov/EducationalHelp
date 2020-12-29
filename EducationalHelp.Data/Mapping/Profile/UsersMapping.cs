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
            builder.Property(u => u.PassHash).IsRequired();
            builder.HasOne(u => u.Avatar).WithOne().HasForeignKey<User>(u => u.AvatarId).IsRequired(false).OnDelete(DeleteBehavior.ClientNoAction);
            base.Map(builder);
        }
    }
}
