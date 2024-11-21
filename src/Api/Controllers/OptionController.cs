
using Microsoft.AspNetCore.Authorization;

namespace thinkschool.OnlineExam.Api.Controllers
{
    public class OptionController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  OptionController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all Options, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of Options with pagination info.</returns>    
        [HttpPost("GetAllOption")]
        public async Task<ActionResult<ListResponse<OptionResDto>>> GetAllOption(GetAllOptionReqDto? requestDto)
        { 
            var result = await _servicesCollection.OptionServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a Option by OptionId, with optional detailed response.
        /// </summary>
        /// <param name="Id"> Option Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> Option details.</returns>
        [HttpGet("GetOptionById/{OptionId}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<OptionResDetailDto>>> GetOptionById(int  OptionId,  bool withDetails = false)
        {
           var result = await _servicesCollection.OptionServices.GetById(OptionId, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new Option.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a Option.</param>
        /// <returns>Added Option details.</returns>
        [HttpPost("AddOption")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<SingleResponse<OptionResDto>>> AddOption(AddOptionReqDto requestDto)
        {
            var result = await _servicesCollection.OptionServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing Option.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a Option.</param>
        /// <returns>Updated Option details.</returns>
        [HttpPost("UpdateOption")]
        public async Task<ActionResult<SingleResponse<OptionResDto>>> UpdateOption(UpdateOptionReqDto requestDto)
        {
            var result = await _servicesCollection.OptionServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a Option.
        /// </summary>
        /// <param name="Id">Option OptionId.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteOption/{OptionId}")]
        public async Task<IActionResult> DeleteOption(int  OptionId)
        {
           var result = await _servicesCollection.OptionServices.Delete(OptionId);
           return HandleResult(result);
        }

    }
}



