using Microsoft.AspNetCore.Authorization;
using thinkschool.OnlineExam.Core.Models.ExamDtos;

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
       // [Authorize(Roles = "Teacher")]
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
        //[Authorize(Roles = "Teacher")]
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

        /// <summary>
        /// API endpoint to retrieve exams by teacher ID.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher whose exams are to be retrieved.</param>
        /// <param name="cancellationToken">The cancellation token for task cancellation.</param>
        /// <returns>An action result containing a list response of exam DTOs.</returns>
        [HttpGet("GetExamsByTeacherId")]
        //[Authorize(Roles = "Teacher")]
        public async Task<ActionResult<ListResponse<ExamDto>>> GetExamsByTeacherId(string teacherId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(teacherId))
            {
                return BadRequest("Teacher ID cannot be null or empty.");
            }

            try
            {
                var result = await _examService.GetExamsByTeacherId(teacherId, cancellationToken);
                return HandleResult(result);
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "An error occurred while retrieving exams.");
            }
        }

        /// <summary>
        /// Updates the exam details.
        /// </summary>
        /// <param name="examDto">The exam data transfer object.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
        /// <returns>An Action result containing the SingleResponse of the operation.</returns>
        [HttpPatch("PatchExam")]
        public async Task<ActionResult<SingleResponse<ExamDto>>> PatchExam([FromBody] ExamDto examDto, CancellationToken cancellationToken)
        {
            if (examDto == null) return BadRequest("Exam data is required.");

            var result = await _examService.UpdateExam(examDto, cancellationToken);
            return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to retrieve exam details by examId.
        /// </summary>
        /// <param name="examId">The ID of the exam.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>An ActionResult containing the exam details.</returns>
        [HttpGet("GetExamWithDetails/{examId}")]
        public async Task<ActionResult<SingleResponse<ExamDetailsResponseDto>>> GetExamWithDetails(int examId, CancellationToken cancellationToken)
        {
            if (examId <= 0)
            {
                return BadRequest("Exam ID must be greater than zero.");
            }

            var result = await _examService.GetExamWithDetails(examId, cancellationToken);
            return HandleResult(result);
        }

        /// <summary>
        /// API endpoint to get exam data by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of exam data wrapped in a ListResponse.</returns>
        [HttpPost("GetExamDataByUserId")]
        public async Task<ActionResult<ListResponse<GetExamDataViewModel>>> GetExamDataByUserId([FromBody] string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("User ID cannot be null or empty.");

            var result = await _examService.GetExamDataByUserId(userId, cancellationToken);
            return HandleResult(result);
        }
    }
}