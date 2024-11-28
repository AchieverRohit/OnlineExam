
using thinkschool.OnlineExam.Core.Models.ExamResultDtos;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class ExamResultController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  ExamResultController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all ExamResults, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of ExamResults with pagination info.</returns>    
        [HttpPost("GetAllExamResult")]
        public async Task<ActionResult<ListResponse<ExamResultResDto>>> GetAllExamResult(GetAllExamResultReqDto? requestDto)
        { 
            var result = await _servicesCollection.ExamResultServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a ExamResult by ExamResultId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> ExamResult Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> ExamResult details.</returns>
        [HttpGet("GetExamResultById/{ExamResultId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ExamResultResDetailDto>>> GetExamResultById(int  ExamResultId,  bool withDetails = false)
        {
           var result = await _servicesCollection.ExamResultServices.GetById(ExamResultId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new ExamResult.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a ExamResult.</param>
        /// <returns>Added ExamResult details.</returns>
        [HttpPost("AddExamResult")]
        public async Task<ActionResult<SingleResponse<ExamResultResDto>>> AddExamResult(AddExamResultReqDto requestDto)
        {
            var result = await _servicesCollection.ExamResultServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing ExamResult.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a ExamResult.</param>
        /// <returns>Updated ExamResult details.</returns>
        [HttpPost("UpdateExamResult")]
        public async Task<ActionResult<SingleResponse<ExamResultResDto>>> UpdateExamResult(UpdateExamResultReqDto requestDto)
        {
            var result = await _servicesCollection.ExamResultServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a ExamResult.
        /// </summary>
        /// <param name="Id">ExamResult ExamResultId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteExamResult/{ExamResultId}")]
        public async Task<IActionResult> DeleteExamResult(int  ExamResultId)
        {
           var result = await _servicesCollection.ExamResultServices.Delete(ExamResultId);
           return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to calculate and create an exam result.
        /// </summary>
        /// <param name="userExamId">The ID of the user exam.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Result containing the ExamResultDto or an error message.</returns>
        [HttpGet("CalculateAndCreateExamResult")]
        public async Task<ActionResult<SingleResponse<ExamResultShortDto>>> CalculateAndCreateExamResult(int userExamId, CancellationToken cancellationToken)
        {
            var result = await _servicesCollection.ExamResultServices.CalculateAndCreateExamResult(userExamId, cancellationToken);
            return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to get exam results by exam ID.
        /// </summary>
        /// <param name="examId">The ID of the exam.</param>
        /// <param name="cancellationToken">CancellationToken for async operation.</param>
        /// <returns>A ListResponse containing a list of ExamResultDto.</returns>
        [HttpPost("GetExamResultsByExamId")]
        public async Task<ActionResult<ListResponse<ExamResultDto>>> GetExamResultsByExamId([FromBody]int examId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _servicesCollection.ExamResultServices.GetExamResultsByExamId(examId, cancellationToken);
                return HandleResult(result);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging according to your logging framework)
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Endpoint to get exam results by user exam ID.
        /// </summary>
        /// <param name="userExamId">The ID of the user exam.</param>
        /// <param name="cancellationToken">Token for canceling the operation.</param>
        /// <returns>Returns exam result details or error status.</returns>
        [HttpPost("GetExamResultByUserExamId")]
        public async Task<ActionResult<SingleResponse<ExamResultDetailsViewModel>>> GetExamResultByUserExamId([FromBody] int userExamId, CancellationToken cancellationToken)
        {
            if (userExamId <= 0)
            {
                return BadRequest("User exam ID must be greater than 0.");
            }

            var result = await _servicesCollection.ExamResultServices.GetExamResultByUserExamId(userExamId, cancellationToken);
            return HandleResult(result);
        }

    }
}



