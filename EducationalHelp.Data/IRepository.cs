using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EducationalHelp.Core;

namespace EducationalHelp.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Automatically save data after updating it
        /// </summary>
        bool AutoSave { get; set; }

        /// <summary>
        /// Contains all data of repository
        /// </summary>
        IQueryable<T> AllData { get; }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="newEntity">New entity</param>
        void Update(T newEntity);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Gets entity by Id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity with <paramref name="id"/> </returns>
        T GetById(Guid id);
    }
}