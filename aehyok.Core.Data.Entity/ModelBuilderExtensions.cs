using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace aehyok.Core.Data.Entity
{
    /// <summary>
    /// ModelBuilder扩展方法
    /// </summary>
    public static class ModelBuilderExtensions
    {
        //获取程序本程序集Assembly中过滤类型（非抽象IsAbstract、IsGenericType、继承自Type）
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly.GetTypes().Where(x =>!x.GetTypeInfo().IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
        }

        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));
            var mappingList = mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>();  //实例化类并进行特定的转换
            foreach (var config in mappingList)
            {
                config.Map(modelBuilder);
            }
        }
    }
}
