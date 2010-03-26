﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace DotNetKillswitch.Core.Persistence
{
    /// <summary>
    /// Default implementation of <see cref="IRepository{T}"/> with NHibernate as a backend.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IRepository<T>
    {
        public GenericRepository(IKillswitchPersistence database)
        {
            Persistence = database;
        }

        /// <summary>
        /// Gets the <see cref="IKillswitchPersistence"/> to use.
        /// </summary>
        public IKillswitchPersistence Persistence { get; private set; }

        /// <summary>
        /// Implementation of <see cref="IEnumerable{T}.GetEnumerator"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return Linq().AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Implementation of <see cref="IQueryable.Expression"/>.
        /// </summary>
        public Expression Expression
        {
            get { return Linq().Expression; }
        }

        /// <summary>
        /// Implemenation of <see cref="IQueryable.ElementType"/>.
        /// </summary>
        public Type ElementType
        {
            get { return Linq().ElementType; }
        }

        /// <summary>
        /// Implementation of <see cref="IQueryable.Provider"/>.
        /// </summary>
        public IQueryProvider Provider
        {
            get { return Linq().Provider; }
        }

        /// <summary>
        /// See <see cref="IRepository{T}.Add"/>.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            ISession session = GetSession();
            session.SaveOrUpdate(entity);
        }

        /// <summary>
        /// See <see cref="IRepository{T}.Remove"/>
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            ISession session = GetSession();
            session.Delete(entity);
        }

        /// <summary>
        /// Gets the underlying <see cref="IQueryable{T}"/> implementation.
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Linq()
        {
            ISession session = GetSession();
            return session.Linq<T>();
        }

        /// <summary>
        /// Gets the underlying <see cref="ISession"/>.
        /// </summary>
        /// <returns></returns>
        private ISession GetSession()
        {
            return Persistence.GetCurrentSession();
        }
    }
}
