using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    //public class TagConfiguration //:EntityConfigurationBase<Tag,int>
    //{
    //    //public TagConfiguration()
    //    //{
    //    //    Property(item => item.Name).HasMaxLength(50);
    //    //}
    //}

    internal class TagConfiguration : DbEntityConfiguration<Tag, int>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}
