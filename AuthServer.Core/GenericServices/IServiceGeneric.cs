namespace AuthServer.Core.GenericServices;
public interface IServiceGeneric<TEntity,TDto> where TEntity : class where TDto : class
{
    Task<ResponseDTO<TDto>> GetByIdAsync(int Id);
    Task<ResponseDTO<IEnumerable<TDto>>> GetAllAsync();
    Task<ResponseDTO<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
    Task<ResponseDTO<TDto>> AddAsync(TDto entity);
    Task<ResponseDTO<NoDataDTO>> Remove(int Id);
    Task<ResponseDTO<NoDataDTO>> Update(TDto entity,int Id);
}