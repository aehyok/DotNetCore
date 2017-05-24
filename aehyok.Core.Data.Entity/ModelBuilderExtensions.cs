using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Data.Entity
{
    internal static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity, TKey>(
          this ModelBuilder modelBuilder,
          DbEntityConfiguration<TEntity,TKey> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }

    internal abstract class DbEntityConfiguration<TEntity,TKey> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
