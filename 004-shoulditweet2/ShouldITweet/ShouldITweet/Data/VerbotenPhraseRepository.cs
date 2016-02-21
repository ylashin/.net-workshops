using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;

namespace ShouldITweet2.Data
{
    public interface IRepository
    {
        T GetById<T>(Guid id) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        void AddOrUpdate<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
    public class VerbotenPhraseRepository : IRepository, IDisposable
    {
        private ShouldITweetDbContext db = new ShouldITweetDbContext();

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return db.Set<T>().AsEnumerable();
        }

        public T GetById<T>(Guid id) where T : class
        {
            return db.Set<T>().Find(id);
        }

        public void AddOrUpdate<T>(T entity) where T : class
        {
            db.Set<T>().AddOrUpdate(entity);
            db.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
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