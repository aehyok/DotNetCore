using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Authorization
{
    public class UserPostConfiguration : EntityMappingConfiguration<UserPost, int>
    {
        public override void Map(EntityTypeBuilder<UserPost> builder)
        {
            //builder.HasOne(m => m.User).WithMany().WillCascadeOnDelete(false);
            //builder.HasOne(m => m.Post).WithMany().OnDelete();
            builder.HasKey(item => item.Id);
        }
    }
}
