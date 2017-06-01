using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Scaffolding.Configuration.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Data.Entity
{
    /// <summary>
    /// 实体映射接口
    /// </summary>
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder builder);
    }

    public interface IEntityMappingConfiguration<TEntity> : IEntityMappingConfiguration where TEntity : class
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}
