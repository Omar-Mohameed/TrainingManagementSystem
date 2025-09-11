using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagementSystem.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        T GetById(int id);
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );
        void Add(T entity);
        void Delete(int id); // hard delete
        void SoftDelete(T entity); // Soft Delete
    }
}
