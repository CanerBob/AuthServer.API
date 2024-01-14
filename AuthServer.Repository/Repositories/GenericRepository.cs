namespace AuthServer.Repository.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntity> _dbset;

    public GenericRepository(AppDbContext context)
    {
        _dbContext = context;
        _dbset=context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbset.AddAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int Id)
    {
        var entity = await _dbset.FindAsync(Id);
        if (entity != null) 
        {
        _dbContext.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public void Remove(TEntity entity)
    {
        _dbset.Remove(entity);
    }

    public TEntity Update(TEntity entity)
    {
        _dbContext.Entry(entity).State= EntityState.Modified;
        return entity;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbset.Where(predicate);
    }
}