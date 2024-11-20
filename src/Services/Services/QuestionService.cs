
namespace thinkschool.OnlineExam.Services.Services;
public class QuestionService : IQuestionService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public QuestionService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allQuestions or fetchesQuestions with pagination based on requestDto
     public async Task<ListResponse<QuestionResDto>> GetAll(GetAllQuestionReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<Question>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<Question, GetAllQuestionReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<QuestionResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<QuestionResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new Question to the database
     public async Task<SingleResponse<QuestionResDto>> Save(AddQuestionReqDto requestDto)
     {
        var entity = _mapper.Map<Question>(requestDto); // Maps the request DTO to the Question entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<QuestionResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<QuestionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing Question in the database
     public async Task<SingleResponse<QuestionResDto>> Update(UpdateQuestionReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<Question,int>(requestDto.QuestionId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.QuestionId)); // Fetches the existing Question by QuestionId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<QuestionResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<QuestionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a Question by QuestionId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int QuestionId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<Question>(x => x.QuestionId == QuestionId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, QuestionId));// Fetches the  Question by QuestionId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<QuestionResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<QuestionResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  Question record
     public async Task<BaseResponse> Delete(int QuestionId)
     {
        var existing = await _baseRepository.GetByIdAsync<Question, int>(QuestionId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, QuestionId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}



