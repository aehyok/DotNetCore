using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

    /// <summary>
    /// 泛型映射接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEntityMappingConfiguration<TEntity> : IEntityMappingConfiguration where TEntity : class
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}
