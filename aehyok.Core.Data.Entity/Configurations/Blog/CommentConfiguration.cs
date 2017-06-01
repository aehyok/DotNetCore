using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    //public class CommentConfiguration//:EntityConfigurationBase<Comment,int>
    //{
    //    //public CommentConfiguration()
    //    //{
    //    //    HasRequired(item => item.Article).WithMany(item => item.Comments);
    //    //}
    //}

    internal class CommentConfiguration: EntityMappingConfiguration<Comment,int>
    {
        public override void Map(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}
