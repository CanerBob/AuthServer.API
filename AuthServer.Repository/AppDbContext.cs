namespace AuthServer.Repository;
public class AppDbContext:IdentityDbContext<UserApp,IdentityRole,string>
{  
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<Product> Products { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(builder);
    }
}