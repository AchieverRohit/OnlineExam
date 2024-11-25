
using thinkschool.OnlineExam.Core.Models.UserAnswerDtos;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class UserAnswerController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  UserAnswerController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all UserAnswers, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of UserAnswers with pagination info.</returns>    
        [HttpPost("GetAllUserAnswer")]
        public async Task<ActionResult<ListResponse<UserAnswerResDto>>> GetAllUserAnswer(GetAllUserAnswerReqDto? requestDto)
        { 
            var result = await _servicesCollection.UserAnswerServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a UserAnswer by UserAnswerId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> UserAnswer Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> UserAnswer details.</returns>
        [HttpGet("GetUserAnswerById/{UserAnswerId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<UserAnswerResDetailDto>>> GetUserAnswerById(int  UserAnswerId,  bool withDetails = false)
        {
           var result = await _servicesCollection.UserAnswerServices.GetById(UserAnswerId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new UserAnswer.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a UserAnswer.</param>
        /// <returns>Added UserAnswer details.</returns>
        [HttpPost("AddUserAnswer")]
        public async Task<ActionResult<SingleResponse<UserAnswerResDto>>> AddUserAnswer(AddUserAnswerReqDto requestDto)
        {
            var result = await _servicesCollection.UserAnswerServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing UserAnswer.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a UserAnswer.</param>
        /// <returns>Updated UserAnswer details.</returns>
        [HttpPost("UpdateUserAnswer")]
        public async Task<ActionResult<SingleResponse<UserAnswerResDto>>> UpdateUserAnswer(UpdateUserAnswerReqDto requestDto)
        {
            var result = await _servicesCollection.UserAnswerServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a UserAnswer.
        /// </summary>
        /// <param name="Id">UserAnswer UserAnswerId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteUserAnswer/{UserAnswerId}")]
        public async Task<IActionResult> DeleteUserAnswer(int  UserAnswerId)
        {
           var result = await _servicesCollection.UserAnswerServices.Delete(UserAnswerId);
           return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to submit an answer.
        /// </summary>
        /// <param name="submitAnswerDto">The answer details provided in the request body.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>A standardized action result containing the submission result.</returns>
        [HttpPost("SubmitAnswer")]
        public async Task<ActionResult<SingleResponse<string>>> SubmitAnswer([FromBody] SubmitAnswerDto submitAnswerDto, CancellationToken cancellationToken)
        {
            if (submitAnswerDto == null) return BadRequest("Invalid submission data.");

            var result = await _servicesCollection.UserAnswerServices.SubmitAnswer(submitAnswerDto, cancellationToken);
            return HandleResult(result);
        }
    }
}



