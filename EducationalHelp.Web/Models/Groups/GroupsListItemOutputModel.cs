using EducationalHelp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalHelp.Web.Models.Groups
{
    public class GroupsListItemOutputModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int MembersCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public GroupsListItemOutputModel(Group group, int membersCount)
        {
            this.Id = group.Id;
            this.Title = group.Title;
            this.Description = group.Description;
            this.MembersCount = membersCount;
            this.CreatedAt = group.CreatedAt;
            this.DeletedAt = group.DeletedAt;
            this.UpdatedAt = group.UpdatedAt;
        }
    }
}
