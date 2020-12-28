using System;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalHelp.Data.Mapping.Subjects
{
    public class LessonsMapping : DataMapper<Lesson>
    {
        public override void Map(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(l => l.Title).HasMaxLength(150).IsRequired();
            builder.Property(l => l.Label).HasMaxLength(50);
            builder.Property(l => l.Description);
            builder.Property(l => l.DateStart).IsRequired();
            builder.Property(l => l.DateEnd).IsRequired();
            builder.Property(l => l.IsVisited);
            builder.Property(l => l.SelfMark);
            builder.Property(l => l.Homework);
            builder.Property(l => l.Notes);
            builder.HasOne(l => l.Subject).WithMany(s => s.Lessons).HasForeignKey(l => l.SubjectId);
            builder.HasOne(l => l.User).WithMany().HasForeignKey(l => l.UserId).IsRequired().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.ClientNoAction);
            base.Map(builder);
        }
    }
}
