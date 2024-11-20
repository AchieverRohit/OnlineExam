
namespace thinkschool.OnlineExam.Api.Controllers
{
    public class UserExamController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  UserExamController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all UserExams, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of UserExams with pagination info.</returns>    
        [HttpPost("GetAllUserExam")]
        public async Task<ActionResult<ListResponse<UserExamResDto>>> GetAllUserExam(GetAllUserExamReqDto? requestDto)
        { 
            var result = await _servicesCollection.UserExamServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a UserExam by UserExamId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> UserExam Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> UserExam details.</returns>
        [HttpGet("GetUserExamById/{UserExamId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<UserExamResDetailDto>>> GetUserExamById(int  UserExamId,  bool withDetails = false)
        {
           var result = await _servicesCollection.UserExamServices.GetById(UserExamId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new UserExam.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a UserExam.</param>
        /// <returns>Added UserExam details.</returns>
        [HttpPost("AddUserExam")]
        public async Task<ActionResult<SingleResponse<UserExamResDto>>> AddUserExam(AddUserExamReqDto requestDto)
        {
            var result = await _servicesCollection.UserExamServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing UserExam.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a UserExam.</param>
        /// <returns>Updated UserExam details.</returns>
        [HttpPost("UpdateUserExam")]
        public async Task<ActionResult<SingleResponse<UserExamResDto>>> UpdateUserExam(UpdateUserExamReqDto requestDto)
        {
            var result = await _servicesCollection.UserExamServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a UserExam.
        /// </summary>
        /// <param name="Id">UserExam UserExamId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteUserExam/{UserExamId}")]
        public async Task<IActionResult> DeleteUserExam(int  UserExamId)
        {
           var result = await _servicesCollection.UserExamServices.Delete(UserExamId);
           return HandleResult(result);
        }

    }
}



