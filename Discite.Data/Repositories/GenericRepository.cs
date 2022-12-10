
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.Discite.Models;

namespace ProjectDiscite.Data.Repositories
{
    public abstract class GenericRepository<T> where T : class, IModelWithId
    {
        protected readonly TravelAgencyDbContext dbContext;

        public GenericRepository()
        {
            this.dbContext = new TravelAgencyDbContext();
        }

        public GenericRepository(TravelAgencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public virtual T Insert(T model)
        {
            var entry = dbContext.Set<T>().Add(model);
            dbContext.SaveChanges();
            return entry.Entity;
        }

        public virtual T Update(T model)
        {
            dbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return model;
        }

        public virtual void Delete(int id)
        {
            var item = dbContext.Find<T>(id);
            dbContext.Set<T>().Remove(item);
            dbContext.SaveChanges();
        }

    }
}
