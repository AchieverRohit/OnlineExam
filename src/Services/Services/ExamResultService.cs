
namespace thinkschool.OnlineExam.Services.Services;
public class ExamResultService : IExamResultService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ExamResultService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allExamResults or fetchesExamResults with pagination based on requestDto
     public async Task<ListResponse<ExamResultResDto>> GetAll(GetAllExamResultReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ExamResult>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ExamResult, GetAllExamResultReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ExamResultResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ExamResultResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ExamResult to the database
     public async Task<SingleResponse<ExamResultResDto>> Save(AddExamResultReqDto requestDto)
     {
        var entity = _mapper.Map<ExamResult>(requestDto); // Maps the request DTO to the ExamResult entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResultResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ExamResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ExamResult in the database
     public async Task<SingleResponse<ExamResultResDto>> Update(UpdateExamResultReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ExamResult,int>(requestDto.ExamResultId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ExamResultId)); // Fetches the existing ExamResult by ExamResultId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResultResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ExamResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ExamResult by ExamResultId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int ExamResultId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ExamResult>(x => x.ExamResultId == ExamResultId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamResultId));// Fetches the  ExamResult by ExamResultId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ExamResultResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ExamResultResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ExamResult record
     public async Task<BaseResponse> Delete(int ExamResultId)
     {
        var existing = await _baseRepository.GetByIdAsync<ExamResult, int>(ExamResultId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamResultId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}



