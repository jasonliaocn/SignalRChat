using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalRChatApi.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private MyChatEntities activeContext;
        public RepositoryBase()
        {
            activeContext = new MyChatEntities();
        }

        public MyChatEntities ActiveContext
        {
            get
            {
                return activeContext;
            }
        }

        private DbSet<T> set = null;

        public DbSet<T> Set
        {
            get
            {
                if (set == null)
                {
                    set = this.ActiveContext.Set<T>();
                }
                return set;
            }
            internal set { set = value; }
        }

        public void Add(T item)
        {
            try
            {
                this.Set.Add(item);
                this.ActiveContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        //public int AddAndGetId(T item)
        //{
        //    this.Set.Add(item);
        //    this.ActiveContext.SaveChanges();
        //    return item.Id;
        //}

        public void AddRange(IEnumerable<T> items)
        {
            try
            {
                //this.ActiveContext.AutoDetectChangesEnabled = false;
                this.Set.AddRange(items);
                this.ActiveContext.SaveChanges();
            }
            finally
            {
                //this.ActiveContext.AutoDetectChangesEnabled = true;
            }
        }

        public void Update(T item, System.Linq.Expressions.Expression<Func<T, object>> propertyExpression = null)
        {
            try
            {
                ////var concurrencyToken = this.ActiveContext.Entry<T>(item).Entity as IConcurrencyToken;
                if (propertyExpression != null)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<System.Reflection.MemberInfo> memberInfos =
                    ((dynamic)propertyExpression.Body).Members;
                    this.ActiveContext.Entry<T>(item).State = EntityState.Unchanged;
                //    if (concurrencyToken != null)
                //    {
                //        this.ActiveContext.Entry<T>(item).Property("ModifiedDate").OriginalValue = concurrencyToken.ModifiedDate;
                //    }
                    foreach (var memberInfo in memberInfos)
                    {
                        this.ActiveContext.Entry<T>(item).Property(memberInfo.Name).IsModified = true;
                    }
                    this.ActiveContext.Configuration.ValidateOnSaveEnabled = false;
                }
                else
                {
                    this.ActiveContext.Entry<T>(item).State = EntityState.Modified;
                ////    if (concurrencyToken != null)
                ////    {
                ////        this.ActiveContext.Entry<T>(item).Property("ModifiedDate").OriginalValue = concurrencyToken.ModifiedDate;
                ////    }
                }
                this.ActiveContext.SaveChanges();
            }
            finally
            {
                this.ActiveContext.Configuration.ValidateOnSaveEnabled = true;
            }

        }

        public void Remove(T item)
        {
            this.Set.Remove(item);
            this.ActiveContext.SaveChanges();
        }

        public IQueryable<T> AsNoTracking()
        {
            return this.Set.AsNoTracking();
        }

        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> anyLambda)
        {
            return this.Set.Any(anyLambda);
        }

        public IQueryable<T> Include(System.Linq.Expressions.Expression<Func<T, object>> subSelector)
        {
            return this.Set.Include(subSelector);
        }

        public IEnumerable<T> GetPageList(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> subSelector,
            System.Linq.Expressions.Expression<Func<T, dynamic>> order)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentException("ERROR_PAGEINDEX");
            }

            IQueryable<T> queryList = subSelector == null ? this.Set : this.Set.Where(subSelector);
            totalCount = queryList.Count();
            return queryList.OrderByDescending(order)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToList();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Set.AsQueryable().AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            return GetEnumerator();
        }

        public IQueryProvider Provider
        {
            get
            {
                return (this.Set.AsQueryable() as IQueryable).Provider;
            }
        }

        public Type ElementType
        {
            get
            {
                return (this.Set.AsQueryable() as IQueryable).ElementType;
            }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get
            {
                return (this.Set.AsQueryable() as IQueryable).Expression;
            }
        }

        //public void Dispose()
        //{
        //    if (ActiveContext != null)
        //        ActiveContext.Dispose();
        //}
    }
}