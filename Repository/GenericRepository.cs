using NaijaQuickFix.Helper;
using NaijaQuickFix.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NaijaQuickFix.Repository
{
    public class GenericRepository<T> where T: class
    {
        
            protected NaijaQuickFixxEntities db = null;
            protected DbSet<T> table = null;
            public GenericRepository()
            {
                this.db = new NaijaQuickFixxEntities();
                table = db.Set<T>();
            }
        //public GenericRepository(string db):this()
        //{

        //}
            public IEnumerable<T> SelectAll()
            {
                return table.ToList();
            }
            public T SelectByID(object id)
            {
                return table.Find(id);
            }
            public T Insert(T obj)
            {
                table.Add(obj);
                Save();
                return obj;
            }
            public void Update(T obj)
            {
                table.Attach(obj);
                db.Entry(obj).State = EntityState.Modified;
                Save();
            }
            public void Delete(object id)
            {
                T existing = table.Find(id);
                table.Remove(existing);
                Save();
            }
            public void Save()
            {
                db.SaveChanges();
            }
        public PagedData<T> SelectPaging(Expression<Func<T, bool>> filter = null, int page = 0, int pageSize = 0)
        {
            PagedData<T> item = new PagedData<T>();
            IQueryable<T> query = table;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if ((pageSize * (page - 1)) >= 0 && pageSize > 0)
            {
                item.Data = query.AsEnumerable().Skip(pageSize * (page - 1)).Take(pageSize);
                item.CurrentPage = page;
                item.NumberOfPages = (query.Count() / pageSize) + (query.Count() % pageSize == 0 ? 0 : 1);
            }
            return item;

        }
    }
}