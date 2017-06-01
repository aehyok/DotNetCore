using aehyok.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Data.Entity
{
    public abstract class EntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration<TEntity> where TEntity : EntityBase<TKey>
    {
        public abstract void Map(EntityTypeBuilder<TEntity> b);

        public void Map(ModelBuilder builder)
        {
            Map(builder.Entity<TEntity>());
        }
    }
}
