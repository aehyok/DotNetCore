using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Authorization
{
    internal class PostRoleConfiguration : EntityMappingConfiguration<PostRole, int>
    {
        public override void Map(EntityTypeBuilder<PostRole> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}
