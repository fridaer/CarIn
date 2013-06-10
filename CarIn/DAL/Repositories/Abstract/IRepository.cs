using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarIn.Models.Entities.Abstract;

namespace CarIn.DAL.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        System.Data.Entity.DbContext Model { get; }

        IQueryable<T> FindAll(Func<T, bool> filter = null);
        T FindByID(int id);
        void Update(T entity);
        void Add(T entity);
        void Delete(T entity);

        void Commit();
        void AddForBulk(T entity);
        void TruncateTable(string tableName );

        void Dispose();
    }
}