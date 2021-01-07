using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EducationalHelp.Data.Mapping.Subjects
{
    public class LessonUsersMapping : DataMapper<LessonUsers>
    {
        public override void Map(EntityTypeBuilder<LessonUsers> builder)
        {
            builder.ToTable("LessonUsers");
            builder.HasKey(lu => new { lu.UserId, lu.LessonId });

            builder.HasOne(lu => lu.Lesson)
                .WithMany(l => l.LessonUsers)
                .HasForeignKey(lu => lu.LessonId);

            builder.HasOne(lu => lu.User)
                .WithMany(u => u.LessonUsers)
                .HasForeignKey(lu => lu.UserId);


            base.Map(builder);
        }
    }
}
