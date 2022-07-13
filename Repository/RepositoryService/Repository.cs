using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }

        public T Get(int Id)
        {
            return entities.SingleOrDefault(x => x.Id == Id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entityObject = entities.SingleOrDefault(x => x.Id == entity.Id);
            if(entityObject != null)
            {
                entityObject.IsActive = false;
                entities.Update(entityObject);
            }
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
        }
        public IEnumerable<T> ByQuery(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }
        public bool SaveChanges()
        {
            var isSaved = false;
            try
            {
                _applicationDbContext.SaveChanges();
                isSaved = true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                //TODO: Logging Exception
            }

            return isSaved;
        }
    }
}
