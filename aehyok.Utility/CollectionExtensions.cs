using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace aehyok.Utility
{
    /// <summary>
    /// 集合扩展方法类
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     从指定IQueryable[T]集合 中查询指定分页条件的子数据集
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <typeparam name="TKey">实体主键类型</typeparam>
        /// <param name="source">要查询的数据集</param>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">输出符合条件的总记录数</param>
        /// <param name="sortConditions">排序条件集合</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity, TKey>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize,
            out int total, Array[] sortConditions = null) where TEntity : EntityBase<TKey>
        {
            //source.CheckNotNull("source");
            //predicate.CheckNotNull("predicate");

            total = source.Count(predicate);
            if (sortConditions == null || sortConditions.Length == 0)
            {
                source = source.OrderBy(m => m.Id);
            }
            else
            {
                //int count = 0;
                IOrderedQueryable<TEntity> orderSource = null;
                source = orderSource;
            }
            return source != null
                ? source.Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<TEntity>().AsQueryable();
        }
    }
}
