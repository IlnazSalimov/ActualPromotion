using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ActualPromotion.Models
{
    public partial class ActualPromotionEntities : IDataContext
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
    }
}