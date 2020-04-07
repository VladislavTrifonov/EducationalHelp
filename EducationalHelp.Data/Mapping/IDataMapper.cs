using EducationalHelp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalHelp.Data.Mapping
{
    public interface IDataMapper<Entity> where Entity : BaseEntity
    {
        void Map(EntityTypeBuilder<Entity> builder);
    }
}
