using System;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalHelp.Data.Mapping.Files
{
    class LessonFilesMapping : DataMapper<LessonFiles>
    {
        public override void Map(EntityTypeBuilder<LessonFiles> builder)
        {
            builder.HasKey(lf => new {lf.LessonId, lf.FileId});

            builder.HasOne(lf => lf.Lesson)
                .WithMany(l => l.LessonFiles)
                .HasForeignKey(lf => lf.LessonId);

            builder.HasOne(lf => lf.File)
                .WithMany(f => f.LessonFiles)
                .HasForeignKey(lf => lf.FileId);

            base.Map(builder);
        }
    }
}
