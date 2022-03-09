using Events.Core.Entities;
using Events.Core.Interfaces;
using Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EventsDbContext _dbContext;
        public Repository(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id)!;
        }
        public virtual IReadOnlyCollection<T> List()
        {
            return _dbContext.Set<T>().ToList().AsReadOnly();
        }
        public virtual IReadOnlyCollection<T> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));
            // return the result of the query using the specification's criteria expression
            return secondaryResult
                            .Where(spec.Criteria)
                            .ToList()
                            .AsReadOnly();
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }
        public void Edit(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
