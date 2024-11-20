namespace thinkschool.OnlineExam.DataAccess.Interfaces;
public interface IBaseRepository
{
    TEntity Add<TEntity>(TEntity entity) where TEntity : class;
    Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
    Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    Task<int> CountAsync<TEntity>() where TEntity : class;
    Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    void Delete<TEntity>(TEntity entity) where TEntity : class;
    void DeleteById<TEntity>(int id) where TEntity : class;
    void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    IQueryable<TEntity> Get<TEntity>() where TEntity : class;
    IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> where, bool withoutTracking) where TEntity : class;
    IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class;
    IQueryable<TEntity> Get<TEntity>(params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class;
    Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
    Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    Task<(List<TEntity>, PageDetails)> GetAllWithPaginationAsync<TEntity, TQuery>(TQuery request, Expression<Func<TEntity, bool>>? predicate = null)
        where TEntity : class
        where TQuery : BasePagination;
    Task<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id)
        where TEntity : class
        where TKey : notnull;
    Task<TEntity?> GetFirstAsync<TEntity>() where TEntity : class;
    Task<TEntity?> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    void Update<TEntity>(TEntity entity) where TEntity : class;
    void UpdateList<TEntity>(List<TEntity> entityList) where TEntity : class;
}
