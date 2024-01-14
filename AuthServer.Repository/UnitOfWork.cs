using AuthServer.Core.UnitOfWork;

namespace AuthServer.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    public UnitOfWork(AppDbContext appDbContext) 
    {
    _dbContext = appDbContext;
    }
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
       await _dbContext.SaveChangesAsync();
    }
}