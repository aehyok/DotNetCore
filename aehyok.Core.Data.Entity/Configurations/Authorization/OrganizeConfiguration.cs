using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Model.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aehyok.Core.Data.Entity.Configurations.Authorization
{
    internal class OrganizeConfiguration : EntityMappingConfiguration<Organize, int>
    {
        //public OrganizeConfiguration()
        //{
        //    HasMany(m => m.PostList).WithRequired(n => n.Organize);
        //}
        public override void Map(EntityTypeBuilder<Organize> builder)
        {
            builder.HasMany(m => m.PostList).WithOne(n => n.Organize);
        }
    }
}
