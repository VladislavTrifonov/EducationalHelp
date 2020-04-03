using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EducationalHelp.Core.Entities;
using EducationalHelp.Core;

namespace EducationalHelp.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        public bool AutoSave { get; set; } = true;

        public IQueryable<T> AllData { get; }

        private DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
            AllData = _context.Set<T>().AsNoTracking();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Remove(entity);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(null, ex);
            }
        }

        public T GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return AllData.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.Add(entity);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(null, ex);
            }
        }

        public void Update(T newEntity)
        {
            if (newEntity == null)
                throw new ArgumentNullException(nameof(newEntity));

            try
            {
                _context.Update(newEntity);

                if (AutoSave)
                    _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(null, ex);
            }
        }
    }
}