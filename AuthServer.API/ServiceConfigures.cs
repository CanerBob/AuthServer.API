namespace AuthServer.API;
public class ServiceConfigures
{
    public ServiceConfigures(IConfiguration builder,IServiceCollection Services)
    {
        //Services.AddIdentity<UserApp, IdentityRole>(opt =>
        //{
        //    opt.User.RequireUniqueEmail = true;
        //}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        //Services.AddScoped<IAuthenticationService, AuthenticationService>();
        //Services.AddScoped<IUserService, UserService>();
        //Services.AddScoped<ITokenService, TokenService>();
        //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        ////Birden fazla generic aldığı için virgül kullandık!!!
        //Services.AddScoped(typeof(IServiceGeneric<,>), (typeof(ServiceGeneric<,>)));
        //Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}