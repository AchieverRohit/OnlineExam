
using Microsoft.AspNetCore.Authorization;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class SectionController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  SectionController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all Sections, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of Sections with pagination info.</returns>    
        [HttpPost("GetAllSection")]
        public async Task<ActionResult<ListResponse<SectionResDto>>> GetAllSection(GetAllSectionReqDto? requestDto)
        { 
            var result = await _servicesCollection.SectionServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a Section by SectionId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> Section Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> Section details.</returns>
        [HttpGet("GetSectionById/{SectionId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<SectionResDetailDto>>> GetSectionById(int  SectionId,  bool withDetails = false)
        {
           var result = await _servicesCollection.SectionServices.GetById(SectionId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new Section.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a Section.</param>
        /// <returns>Added Section details.</returns>
        [HttpPost("AddSection")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<SingleResponse<SectionResDto>>> AddSection(AddSectionReqDto requestDto)
        {
            var result = await _servicesCollection.SectionServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing Section.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a Section.</param>
        /// <returns>Updated Section details.</returns>
        [HttpPost("UpdateSection")]
        public async Task<ActionResult<SingleResponse<SectionResDto>>> UpdateSection(UpdateSectionReqDto requestDto)
        {
            var result = await _servicesCollection.SectionServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a Section.
        /// </summary>
        /// <param name="Id">Section SectionId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteSection/{SectionId}")]
        public async Task<IActionResult> DeleteSection(int  SectionId)
        {
           var result = await _servicesCollection.SectionServices.Delete(SectionId);
           return HandleResult(result);
        }

    }
}



