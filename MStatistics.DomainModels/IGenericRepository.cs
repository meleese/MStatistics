using System.Linq.Expressions;

namespace MStatistics.DomainModels
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets the entity by given id
        /// </summary>
        /// <param name="id">String id (key)</param>
        /// <returns>Entity</returns>
        Task<T> GetById(string id);
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>IEnumerable<Entity></Entitiy></returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Searches for a set of entities given an expression
        /// </summary>
        /// <param name="expression">Expression to run</param>
        /// <returns>IEnumerable<Entity>></returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Adds the entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Task</returns>
        Task Add(T entity);
        /// <summary>
        /// Adds a collection of entities
        /// </summary>
        /// <param name="entities">IEnumerable collection of entities</param>
        /// <returns>Task</returns>
        Task AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Removes a specific entity
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        /// <returns>Task</returns>
        Task Remove(T entity);
        /// <summary>
        /// Removes a collection of entities
        /// </summary>
        /// <param name="entities"IEnumerable collection of entities></param>
        /// <returns>Task</returns>
        Task RemoveRange(IEnumerable<T> entities);
        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Task</returns>
        Task Update(T entity);
    }
}
