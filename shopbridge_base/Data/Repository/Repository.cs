using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Shopbridge_Context dbcontext;
        private DbSet<T> set;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this.dbcontext = _dbcontext;
            this.set = _dbcontext.Set<T>();

            //this.dbcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<T> GetById(int id)
        {
            return await set.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await set.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await set.AddAsync(entity);
            await dbcontext.SaveChangesAsync();
           
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await set.FindAsync(id);
            if (this.dbcontext.Entry(entity).State == EntityState.Detached)
                this.dbcontext.Set<T>().Attach(entity);

            this.dbcontext.Set<T>().Remove(entity);
            
            await dbcontext.SaveChangesAsync();
            
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet<T> dbSet = this.dbcontext.Set<T>();
            dbSet.Attach(entity);
            this.dbcontext.Entry(entity).State = EntityState.Modified;
            await dbcontext.SaveChangesAsync();

        }

    }
}
