using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrainingManagementSystem.DataAccess.Data;
using TrainingManagementSystem.DataAccess.Repositories.Interfaces;

namespace TrainingManagementSystem.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity); // Courses.Add(Course)
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                // _context.Product.Include("Category,Users,...)
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return query.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id)!;
        }
        public T GetFirstOrDefault(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public void SoftDelete(T entity)
        {
            var property = entity.GetType().GetProperty("IsDeleted");
            if (property != null)
            {
                property.SetValue(entity, true);
                _dbSet.Update(entity);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(T).Name} does not support soft delete.");
            }
        }
    }
}
