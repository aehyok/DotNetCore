﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Core.Data.Entity.Configurations.Blog
{
    /// <summary>
    /// 文章类属性映射
    /// </summary>
    internal class ArticleConfiguration : EntityMappingConfiguration<Article,int>
    {
        public override void Map(EntityTypeBuilder<Article> builder)
        {
            builder.Property(item => item.Title).HasMaxLength(100);
            builder.ToTable("Article");
            //http://www.cnblogs.com/chen-jie/p/7376676.html
            builder.HasQueryFilter(item => !item.IsDeleted);  //设置全局过滤软删除字段  IgnoreQueryFilters
            //取消全局查询过滤条件的限制
            //_blogArticleRepository.Entities.IgnoreQueryFilters().ToList();
        }
    }
}