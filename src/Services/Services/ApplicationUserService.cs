
namespace thinkschool.OnlineExam.Services.Services;
public class ApplicationUserService : IApplicationUserService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ApplicationUserService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allApplicationUsers or fetchesApplicationUsers with pagination based on requestDto
     public async Task<ListResponse<ApplicationUserResDto>> GetAll(GetAllApplicationUserReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ApplicationUser>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ApplicationUser, GetAllApplicationUserReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ApplicationUserResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ApplicationUserResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ApplicationUser to the database
     public async Task<SingleResponse<ApplicationUserResDto>> Save(AddApplicationUserReqDto requestDto)
     {
        var entity = _mapper.Map<ApplicationUser>(requestDto); // Maps the request DTO to the ApplicationUser entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ApplicationUserResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ApplicationUserResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ApplicationUser in the database
     public async Task<SingleResponse<ApplicationUserResDto>> Update(UpdateApplicationUserReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ApplicationUser,string>(requestDto.Id) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.Id)); // Fetches the existing ApplicationUser by Id 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ApplicationUserResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ApplicationUserResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ApplicationUser by Id with optional details
     public async Task<SingleResponse<dynamic>> GetById(string Id, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ApplicationUser>(x => x.Id == Id ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id));// Fetches the  ApplicationUser by Id or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ApplicationUserResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ApplicationUserResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ApplicationUser record
     public async Task<BaseResponse> Delete(string Id)
     {
        var existing = await _baseRepository.GetByIdAsync<ApplicationUser, string>(Id)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, Id)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }
       
}



