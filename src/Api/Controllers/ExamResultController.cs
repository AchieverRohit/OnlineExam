
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

    }
}



