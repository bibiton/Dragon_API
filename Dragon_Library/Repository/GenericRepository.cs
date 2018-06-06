using Dragon_Library.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;

namespace Dragon_Library.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        
        public DbContext _context
        {
            get;
            set;
        }
        public GenericRepository()
            : this(new Dragon_Library.Models.Entity.DragonFighterEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
            this._context.Configuration.AutoDetectChangesEnabled = false;
            this._context.Configuration.ValidateOnSaveEnabled = false;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
            this._context.Configuration.AutoDetectChangesEnabled = false;
            this._context.Configuration.ValidateOnSaveEnabled = false;
        }



        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public virtual void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }
        }


        public virtual void CreateAll(IEnumerable<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var item in instance)
                {
                    this._context.Set<TEntity>().Add(item);
                }
                
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                this.SaveChanges();
            }
        }

        public virtual void UpdateAll(IEnumerable<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var item in instance)
                {
                    this._context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                this.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes all the specified instance.
        /// </summary>
        /// <param name="instance"></param>
        public void DeleteAll(IEnumerable<TEntity> instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                foreach (var item in instance)
                {
                    this._context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }

                this.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                    new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    }))
            {
                return this._context.Set<TEntity>().FirstOrDefault(predicate);
            }
        }

        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                    new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    }))
            {
                return this._context.Set<TEntity>().Where(predicate).ToList();
            }
        }

        /// <summary>
        /// Gets the specified predicate counts.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                    new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    }))
            {
                return this._context.Set<TEntity>().Where(predicate).Count();
            }
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        //public List<TEntity> GetAll()
        //{
        //    using (var t = new TransactionScope(TransactionScopeOption.Suppress,
        //           new TransactionOptions
        //           {
        //               IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
        //           }))
        //    {
        //        return this._context.Set<TEntity>().AsQueryable().ToList();
        //    }
        //}

        public List<TEntity> GetAll()
        {
            using (var t = new TransactionScope(TransactionScopeOption.Suppress,
                   new TransactionOptions
                   {
                       IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                   }))
            {
                return this._context.Set<TEntity>().AsQueryable().ToList();
            }
        }

        public void SaveChanges()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                SqlException se = null;
                Exception next = ex;

                while (next.InnerException != null)
                {
                    se = next.InnerException as SqlException;
                    next = next.InnerException;
                }

                if (se != null)
                {
                    //if (se.Number == 2627)
                    //    throw new Exception("不可新增重複的資料", se);
                    //if (se.Number == 547)
                    //    throw new Exception("已有關聯資料，不可刪除", se);
                    //else
                        throw se;
                }
                else
                {
                    throw ex;
                }

            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
