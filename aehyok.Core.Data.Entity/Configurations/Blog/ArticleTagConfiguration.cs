using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    //public class ArticleTagConfiguration//:EntityConfigurationBase<ArticleTag,int>
    //{
    //    public ArticleTagConfiguration()
    //    {
    //        //级联删除 
    //        HasRequired(item => item.Tag).WithMany().WillCascadeOnDelete(true);
    //        HasRequired(item => item.Article).WithMany().WillCascadeOnDelete(true);
    //    }
    //}

    internal class ArticleTagConfiguration: DbEntityConfiguration<ArticleTag,int>
    {
        public override void Configure(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }
}
