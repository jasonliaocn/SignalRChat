using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SignalRChatApi.Repositories
{
    public interface IRepositoryBase<T> : IQueryable<T> where T : class
    {
        DbSet<T> Set { get; }

        void Add(T item);

        //int AddAndGetId(T item);

        void AddRange(IEnumerable<T> items);

        void Update(T item, System.Linq.Expressions.Expression<Func<T, object>> propertyExpression = null);

        void Remove(T item);

        IQueryable<T> AsNoTracking();

        bool Exist(Expression<Func<T, bool>> anyLambda);

        IQueryable<T> Include(Expression<Func<T, object>> subSelector);

        IEnumerable<T> GetPageList(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> subSelector, Expression<Func<T, dynamic>> order);

        IEnumerator<T> GetEnumerator();

        IQueryProvider Provider { get; }
    }
}