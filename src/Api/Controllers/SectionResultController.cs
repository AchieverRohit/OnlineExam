
namespace thinkschool.OnlineExam.Api.Controllers
{
    public class SectionResultController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  SectionResultController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all SectionResults, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of SectionResults with pagination info.</returns>    
        [HttpPost("GetAllSectionResult")]
        public async Task<ActionResult<ListResponse<SectionResultResDto>>> GetAllSectionResult(GetAllSectionResultReqDto? requestDto)
        { 
            var result = await _servicesCollection.SectionResultServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a SectionResult by SectionResultId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> SectionResult Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> SectionResult details.</returns>
        [HttpGet("GetSectionResultById/{SectionResultId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<SectionResultResDetailDto>>> GetSectionResultById(int  SectionResultId,  bool withDetails = false)
        {
           var result = await _servicesCollection.SectionResultServices.GetById(SectionResultId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new SectionResult.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a SectionResult.</param>
        /// <returns>Added SectionResult details.</returns>
        [HttpPost("AddSectionResult")]
        public async Task<ActionResult<SingleResponse<SectionResultResDto>>> AddSectionResult(AddSectionResultReqDto requestDto)
        {
            var result = await _servicesCollection.SectionResultServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing SectionResult.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a SectionResult.</param>
        /// <returns>Updated SectionResult details.</returns>
        [HttpPost("UpdateSectionResult")]
        public async Task<ActionResult<SingleResponse<SectionResultResDto>>> UpdateSectionResult(UpdateSectionResultReqDto requestDto)
        {
            var result = await _servicesCollection.SectionResultServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a SectionResult.
        /// </summary>
        /// <param name="Id">SectionResult SectionResultId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteSectionResult/{SectionResultId}")]
        public async Task<IActionResult> DeleteSectionResult(int  SectionResultId)
        {
           var result = await _servicesCollection.SectionResultServices.Delete(SectionResultId);
           return HandleResult(result);
        }

    }
}



