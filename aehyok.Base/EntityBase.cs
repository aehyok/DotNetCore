using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace aehyok.Base
{
    /// <summary>
    /// 可持久化到数据库的数据模型基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey>:IEntity<TKey>
    {
        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key]
        public TKey Id { get; set; }

        public EntityBase()
        {

        }
    }
}
