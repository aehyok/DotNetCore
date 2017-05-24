using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    //public class ArticleConfiguration//:EntityConfigurationBase<Article,int>
    //{
    //    /// <summary>
    //    /// 多对多关系映射
    //    /// </summary>
    //    //public ArticleConfiguration()
    //    //{
    //    //    Property(item => item.Title).HasMaxLength(100);
    //    //}
    //}

    internal class ArticleConfiguration : DbEntityConfiguration<Article, int>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(item => item.Title).HasMaxLength(100);
        }
    }
}