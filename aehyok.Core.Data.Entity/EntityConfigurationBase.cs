using aehyok.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace aehyok.Core.Data.Entity
{
    /// <summary>
    /// 数据实体映射配置基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    /// <typeparam name="TKey">动态主键类型</typeparam>
    //public abstract class EntityConfigurationBase//<TEntity, TKey> : EntityTypeBuilder<TEntity>, IEntityMapper where TEntity : class
    //{
    //    //public EntityConfigurationBase([NotNullAttribute] InternalEntityTypeBuilder builder) : base(builder)
    //    //{

    //    //}

    //    /// <summary>
    //    /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
    //    /// </summary>
    //    /// <param name="configurations">实体映射配置注册器</param>
    //    public void RegistTo()//(ConfigurationRegistrar configurations)
    //    {
    //        //configurations.Add(this);
    //    }
    //}
}
