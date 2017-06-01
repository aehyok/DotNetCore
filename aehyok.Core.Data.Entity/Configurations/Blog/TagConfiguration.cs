using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    /// <summary>
    /// 标签类属性映射
    /// </summary>
    internal class TagConfiguration : EntityMappingConfiguration<Tag,int>
    {
        public override void Map(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}
