
namespace thinkschool.OnlineExam.Services.Services;
public class OptionService : IOptionService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public OptionService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allOptions or fetchesOptions with pagination based on requestDto
     public async Task<ListResponse<OptionResDto>> GetAll(GetAllOptionReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<Option>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<Option, GetAllOptionReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<OptionResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<OptionResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new Option to the database
     public async Task<SingleResponse<OptionResDto>> Save(AddOptionReqDto requestDto)
     {
        var entity = _mapper.Map<Option>(requestDto); // Maps the request DTO to the Option entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<OptionResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<OptionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing Option in the database
     public async Task<SingleResponse<OptionResDto>> Update(UpdateOptionReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<Option,int>(requestDto.OptionId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.OptionId)); // Fetches the existing Option by OptionId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<OptionResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<OptionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a Option by OptionId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int OptionId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<Option>(x => x.OptionId == OptionId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, OptionId));// Fetches the  Option by OptionId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<OptionResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<OptionResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  Option record
     public async Task<BaseResponse> Delete(int OptionId)
     {
        var existing = await _baseRepository.GetByIdAsync<Option, int>(OptionId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, OptionId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}



