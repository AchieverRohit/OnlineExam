
using Microsoft.AspNetCore.Authorization;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  QuestionController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all Questions, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of Questions with pagination info.</returns>    
        [HttpPost("GetAllQuestion")]
        public async Task<ActionResult<ListResponse<QuestionResDto>>> GetAllQuestion(GetAllQuestionReqDto? requestDto)
        { 
            var result = await _servicesCollection.QuestionServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a Question by QuestionId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> Question Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> Question details.</returns>
        [HttpGet("GetQuestionById/{QuestionId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<QuestionResDetailDto>>> GetQuestionById(int  QuestionId,  bool withDetails = false)
        {
           var result = await _servicesCollection.QuestionServices.GetById(QuestionId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new Question.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a Question.</param>
        /// <returns>Added Question details.</returns>
        [HttpPost("AddQuestion")]
        //[Authorize(Roles = "Teacher")]
        public async Task<ActionResult<SingleResponse<QuestionResDto>>> AddQuestion(AddQuestionReqDto requestDto)
        {
            var result = await _servicesCollection.QuestionServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing Question.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a Question.</param>
        /// <returns>Updated Question details.</returns>
        [HttpPost("UpdateQuestion")]
        public async Task<ActionResult<SingleResponse<QuestionResDto>>> UpdateQuestion(UpdateQuestionReqDto requestDto)
        {
            var result = await _servicesCollection.QuestionServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a Question.
        /// </summary>
        /// <param name="Id">Question QuestionId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteQuestion/{QuestionId}")]
        public async Task<IActionResult> DeleteQuestion(int  QuestionId)
        {
           var result = await _servicesCollection.QuestionServices.Delete(QuestionId);
           return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to add a question with options.
        /// </summary>
        /// <param name="questionDto">The question DTO from the request body.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>ActionResult containing the response.</returns>
        [HttpPost("AddQuestionWithOptions")]
        public async Task<ActionResult<SingleResponse<QuestionDto>>> AddQuestionWithOptions([FromBody] QuestionDto questionDto, CancellationToken cancellationToken)
        {
            if (questionDto == null) return BadRequest("QuestionDto cannot be null.");

            var result = await _servicesCollection.QuestionServices.AddQuestionWithOptions(questionDto, cancellationToken);
            return HandleResult(result);
        }


    }
}



