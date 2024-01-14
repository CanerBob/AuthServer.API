using AuthServer.Service.Mappers;

namespace AuthServer.Service.Services;
public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<TEntity> _genericRepository;
    public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = genericRepository;
    }
    public async Task<ResponseDTO<TDto>> AddAsync(TDto entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
        await _genericRepository.AddAsync(newEntity);
        await _unitOfWork.SaveChangesAsync();
        var newDTO=ObjectMapper.Mapper.Map<TDto>(newEntity);
        return ResponseDTO<TDto>.Succes(newDTO,200);
    }
    public async Task<ResponseDTO<IEnumerable<TDto>>> GetAllAsync()
    {
        var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
        return ResponseDTO<IEnumerable<TDto>>.Succes(products,200);
    }
    public async Task<ResponseDTO<TDto>> GetByIdAsync(int Id)
    {
       var product=await _genericRepository.GetByIdAsync(Id);
        if (product == null) 
        {
            return ResponseDTO<TDto>.Fail("Id Not Found", 404, true);
        }
        return ResponseDTO<TDto>.Succes(ObjectMapper.Mapper.Map<TDto>(product), 200);
    }
    public async Task<ResponseDTO<NoDataDTO>> Remove(int Id)
    {
        var isExistEntity = await _genericRepository.GetByIdAsync(Id);
        if(isExistEntity == null) 
        {
            return ResponseDTO<NoDataDTO>.Fail("Id Not Found", 404, true);
        };
        _genericRepository.Remove(isExistEntity);
        await _unitOfWork.SaveChangesAsync();
        return ResponseDTO<NoDataDTO>.Succes(204);
    }
    public async Task<ResponseDTO<NoDataDTO>> Update(TDto entity,int Id)
    {
        var isExistEntity=await _genericRepository.GetByIdAsync(Id);
        if (isExistEntity == null) 
        {
            return ResponseDTO<NoDataDTO>.Fail("I Not Found", 404, true);
        }
        var updateEntity=ObjectMapper.Mapper.Map<TEntity>(entity);
        _genericRepository.Update(updateEntity);
        await _unitOfWork.SaveChangesAsync();
        return ResponseDTO<NoDataDTO>.Succes(204);
    }
    public async Task<ResponseDTO<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        var list=_genericRepository.Where(predicate);
        return ResponseDTO<IEnumerable<TDto>>.Succes(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()),200);
    }
}