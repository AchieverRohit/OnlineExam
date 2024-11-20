namespace thinkschool.OnlineExam.DataAccess.Repositories;
public class BaseRepository(IAppDbContext dbContext) : IBaseRepository
{
    protected readonly IAppDbContext _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private DbSet<TEntity> DbSet<TEntity>() where TEntity : class
    {
        return _context.Set<TEntity>();
    }
    public TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        return DbSet<TEntity>().Add(entity).Entity;
    }
    public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        return (await DbSet<TEntity>().AddAsync(entity)).Entity;
    }
    public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        await DbSet<TEntity>().AddRangeAsync(entities);
        return entities;
    }
    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await DbSet<TEntity>().AnyAsync(predicate);
    }
    public async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await DbSet<TEntity>().CountAsync(predicate);
    }
    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        return await DbSet<TEntity>().CountAsync();
    }
    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        DbSet<TEntity>().Remove(entity);
    }
    public void DeleteById<TEntity>(int id) where TEntity : class
    {
        var entity = DbSet<TEntity>().Find(id);
        if (entity != null)
            Delete(entity);
    }
    public void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        DbSet<TEntity>().RemoveRange(entities);
    }
    public IQueryable<TEntity> Get<TEntity>() where TEntity : class
    {
        return DbSet<TEntity>().AsQueryable();
    }
    public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> where, bool withoutTracking) where TEntity : class
    {
        var query = withoutTracking ? DbSet<TEntity>().AsNoTracking() : DbSet<TEntity>();
        return query.Where(where);
    }
    public IQueryable<TEntity> Get<TEntity>(params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class
    {
        var query = DbSet<TEntity>().AsQueryable();
        foreach (var navigation in navigationProperties)
        {
            query = query.Include(navigation);
        }
        return query;
    }
    public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class
    {
        var query = DbSet<TEntity>().AsQueryable();
        foreach (var navigation in navigationProperties)
        {
            query = query.Include(navigation);
        }
        return query.Where(where);
    }
    public async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await DbSet<TEntity>().Where(predicate).ToListAsync();
    }
    public async Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        return await DbSet<TEntity>().ToListAsync();
    }
    public async Task<(List<TEntity>, PageDetails)> GetAllWithPaginationAsync<TEntity, TQuery>(TQuery request, Expression<Func<TEntity, bool>>? predicate = null) where TQuery : BasePagination where TEntity : class
    {
        var query = predicate != null ? DbSet<TEntity>().Where(predicate).AsQueryable() : DbSet<TEntity>().AsQueryable();
        query = query.BuildFilterExpression(request);
        //If lazy loading is not enabled, you can use the following code to retrieve navigation entity records
        //  query = query.BuildIncludeExp(typeof(TEntity));
        if (!string.IsNullOrEmpty(request.Options!.SortBy))
        {
            query = query.BuildSortingExp(request.Options!.SortBy, request.Options!.SortDescending);
        }
        var (Query, TotalCount) = query.BuildPagingExp(request.Options!.PageNum, request.Options!.PageSize);
        query = Query;
        // execute query
        var result = await query.ToListAsync();
        return (result, new PageDetails(request.Options!.PageNum, request.Options!.PageSize, TotalCount));
    }
    public async Task<TEntity?> GetByIdAsync<TEntity, TKey>(TKey id) where TEntity : class where TKey : notnull
    {
        return await DbSet<TEntity>().FindAsync(id);
    }
    public async Task<TEntity?> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        return await DbSet<TEntity>().Where(predicate).FirstOrDefaultAsync();
    }
    public async Task<TEntity?> GetFirstAsync<TEntity>() where TEntity : class
    {
        return await DbSet<TEntity>().FirstOrDefaultAsync();
    }
    public void Update<TEntity>(TEntity entity) where TEntity : class
    {
        DbSet<TEntity>().Update(entity);
    }
    public void UpdateList<TEntity>(List<TEntity> entityList) where TEntity : class
    {
        foreach (var entity in entityList)
        {
            // Attach the entity if it is not already tracked
            if (DbSet<TEntity>().Entry(entity).State == EntityState.Detached)
            {
                DbSet<TEntity>().Attach(entity);
            }
            DbSet<TEntity>().Entry(entity).State = EntityState.Modified;
        }
    }
}
