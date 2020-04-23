using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EducationalHelp.Core.Entities;
using EducationalHelp.Core;
using EducationalHelp.Data.Exceptions;

namespace EducationalHelp.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> AllData { get; }

        private DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
            AllData = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DataException(null, ex);
            }
        }

        public T GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new DataException("Guid is empty");

            return AllData.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Add(entity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DataException(null, ex);
            }
        }

        public void Update(T newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException(nameof(newEntity));
            try
            {
                newEntity.UpdatedAt = DateTime.UtcNow;
                if (!_context.ChangeTracker.Entries<T>().Contains(_context.Entry<T>(newEntity)))
                    _context.Update(newEntity);

                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new DataException(null, ex);
            }
        }
    }
}