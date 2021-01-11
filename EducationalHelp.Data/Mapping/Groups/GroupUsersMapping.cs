using EducationalHelp.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace EducationalHelp.Data.Mapping.Groups
{
    public class GroupUsersMapping : DataMapper<GroupUsers>
    {
        public override void Map(EntityTypeBuilder<GroupUsers> builder)
        {
            builder.ToTable("GroupUsers");
            builder.HasKey(gu => new { gu.UserId, gu.GroupId });

            builder.HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId);

            builder.HasOne(gu => gu.Group)
                .WithMany()
                .HasForeignKey(gu => gu.GroupId);

            base.Map(builder);
        }
    }
}
