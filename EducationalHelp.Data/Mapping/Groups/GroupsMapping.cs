using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping.Groups
{
    public class GroupsMapping : DataMapper<Group>
    {
        public override void Map(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.Property(g => g.Title).IsRequired();
            builder.Property(g => g.Description).IsRequired();

            base.Map(builder);
        }
    }
}
