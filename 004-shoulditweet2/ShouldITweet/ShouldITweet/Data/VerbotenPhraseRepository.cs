using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;

namespace ShouldITweet2.Data
{
   
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void AddOrUpdate(T entity) ;
        void Delete(T entity);
    }
    public class VerbotenPhraseRepository<T> : IDisposable,  IRepository<T> where T : class // Could make this generic
    {
        private ShouldITweetDbContext db = new ShouldITweetDbContext();

        public IEnumerable<T> GetAll() 
        {
            return db.Set<T>().AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return db.Set<T>().Find(id);
        }

        public void AddOrUpdate(T entity)
        {
            db.Set<T>().AddOrUpdate(entity);
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}