using Brio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Brio.Models
{
    public partial class Entities : IDataContext
    {
        DbEntityEntry<TEntity> IDataContext.Entry<TEntity>(TEntity entity)
        {
            return this.Entry(entity);
        }

        DbSet<TEntity> IDataContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        int SaveChanges()
        {
            return this.SaveChanges();
        }

        void IDataContext.Dispose()
        {
            this.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.SaveChangesAsync();
        }
    }
}