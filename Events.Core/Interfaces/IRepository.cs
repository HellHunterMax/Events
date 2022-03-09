using Events.Core.Entities;

namespace Events.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        IReadOnlyCollection<T> List();
        IReadOnlyCollection<T> List(ISpecification<T> spec);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
