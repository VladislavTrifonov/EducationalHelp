using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping.Subjects
{
    internal class SubjectsMapping : DataMapper<Subject>
    {
        public override void Map(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Subjects");
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Description);
            builder.Property(t => t.Teacher).HasMaxLength(50);
            builder.HasOne(t => t.Annotation).WithOne().HasForeignKey<Subject>(t => t.AnnotationId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Program).WithOne().HasForeignKey<Subject>(t => t.ProgramId).IsRequired(false);
            builder.HasOne(t => t.ValuationTools).WithOne().HasForeignKey<Subject>(t => t.ValuationToolsId).OnDelete(DeleteBehavior.NoAction);
            base.Map(builder);
        }
    }
}
