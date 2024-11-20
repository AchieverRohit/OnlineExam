using Microsoft.AspNetCore.Authorization;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
        private readonly IExamService _examService;
        public ExamController(IServicesCollection servicesCollection, IExamService examService)
        {
            _servicesCollection = servicesCollection;
            _examService = examService;
        }

        /// <summary>
        /// Endpoint to get all Exams, with optional pagination.
        /// </summary>
        /// <param name = "requestDto">Request DTO.</param>
        /// <returns>List of Exams with pagination info.</returns>    
        [HttpPost("GetAllExam")]
        public async Task<ActionResult<ListResponse<ExamResDto>>> GetAllExam(GetAllExamReqDto? requestDto)
        {
            var result = await _servicesCollection.ExamServices.GetAll(requestDto);
            return HandleResult(result);
        }

        /// <summary>
        /// Endpoint to get a Exam by ExamId, with optional detailed response.
        /// </summary>
        /// <param name = "Id"> Exam Id.</param>
        /// <param name = "withDetails">Whether to include detailed information.</param>
        /// <returns> Exam details.</returns>
        [HttpGet("GetExamById/{ExamId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ExamResDetailDto>>> GetExamById(int ExamId, bool withDetails = false)
        {
            var result = await _servicesCollection.ExamServices.GetById(ExamId, withDetails);
            return HandleResult(result);
        }

        /// <summary>
        /// Endpoint to add a new Exam.
        /// </summary>
        /// <param name = "requestDto">Request DTO for adding a Exam.</param>
        /// <returns>Added Exam details.</returns>
        [HttpPost("AddExam")]
        public async Task<ActionResult<SingleResponse<ExamResDto>>> AddExam(AddExamReqDto requestDto)
        {
            var result = await _servicesCollection.ExamServices.Save(requestDto);
            return HandleResult(result);
        }

        /// <summary>
        /// Endpoint to update an existing Exam.
        /// </summary>
        /// <param name = "requestDto">Request DTO for updating a Exam.</param>
        /// <returns>Updated Exam details.</returns>
        [HttpPost("UpdateExam")]
        public async Task<ActionResult<SingleResponse<ExamResDto>>> UpdateExam(UpdateExamReqDto requestDto)
        {
            var result = await _servicesCollection.ExamServices.Update(requestDto);
            return HandleResult(result);
        }

        /// <summary>
        /// Endpoint to delete a Exam.
        /// </summary>
        /// <param name = "Id">Exam ExamId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteExam/{ExamId}")]
        public async Task<IActionResult> DeleteExam(int ExamId)
        {
            var result = await _servicesCollection.ExamServices.Delete(ExamId);
            return HandleResult(result);
        }

        /// <summary>
        /// Gets active exams.
        /// </summary>
        /// <param name = "cancellationToken">Token to cancel the asynchronous operation.</param>
        /// <returns>ActionResult containing a ListResponse of ExamDto.</returns>
        [HttpGet("GetActiveExams")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<ListResponse<ExamDto>>> GetActiveExams(CancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                return BadRequest("Cancellation token should not be null.");
            }

            try
            {
                var result = await _examService.GetActiveExams(cancellationToken);
                return HandleResult(result);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}