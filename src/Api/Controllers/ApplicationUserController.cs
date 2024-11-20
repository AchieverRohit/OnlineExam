
namespace thinkschool.OnlineExam.Api.Controllers
{
    public class ApplicationUserController : BaseController
    {
        private readonly IServicesCollection _servicesCollection;
                
        public  ApplicationUserController(IServicesCollection servicesCollection)
        {
            _servicesCollection = servicesCollection;
            
        }    
         
        /// <summary>
        /// Endpoint to get all ApplicationUsers, with optional pagination.
        /// </summary>
        /// <param name="requestDto">Request DTO.</param>
        /// <returns>List of ApplicationUsers with pagination info.</returns>    
        [HttpPost("GetAllApplicationUser")]
        public async Task<ActionResult<ListResponse<ApplicationUserResDto>>> GetAllApplicationUser(GetAllApplicationUserReqDto? requestDto)
        { 
            var result = await _servicesCollection.ApplicationUserServices.GetAll(requestDto);
            return HandleResult(result);
        }
            
        /// <summary>
        /// Endpoint to get a ApplicationUser by Id, with optional detailed response.
        /// </summary>
        /// <param name="Id"> ApplicationUser Id.</param>
        /// <param name="withDetails">Whether to include detailed information.</param>
        /// <returns> ApplicationUser details.</returns>
        [HttpGet("GetApplicationUserById/{Id}/{withDetails}")]
        public async Task<ActionResult<SingleResponse<ApplicationUserResDetailDto>>> GetApplicationUserById(string  Id,  bool withDetails = false)
        {
           var result = await _servicesCollection.ApplicationUserServices.GetById(Id, withDetails);
           return HandleResult(result);
        }
        
     
        /// <summary>
        /// Endpoint to add a new ApplicationUser.
        /// </summary>
        /// <param name="requestDto">Request DTO for adding a ApplicationUser.</param>
        /// <returns>Added ApplicationUser details.</returns>
        [HttpPost("AddApplicationUser")]
        public async Task<ActionResult<SingleResponse<ApplicationUserResDto>>> AddApplicationUser(AddApplicationUserReqDto requestDto)
        {
            var result = await _servicesCollection.ApplicationUserServices.Save(requestDto);
            return HandleResult(result);
        }
    
        
        /// <summary>
        /// Endpoint to update an existing ApplicationUser.
        /// </summary>
        /// <param name="requestDto">Request DTO for updating a ApplicationUser.</param>
        /// <returns>Updated ApplicationUser details.</returns>
        [HttpPost("UpdateApplicationUser")]
        public async Task<ActionResult<SingleResponse<ApplicationUserResDto>>> UpdateApplicationUser(UpdateApplicationUserReqDto requestDto)
        {
            var result = await _servicesCollection.ApplicationUserServices.Update(requestDto);
            return HandleResult(result);
        }


         
        /// <summary>
        /// Endpoint to delete a ApplicationUser.
        /// </summary>
        /// <param name="Id">ApplicationUser Id.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpDelete("DeleteApplicationUser/{Id}")]
        public async Task<IActionResult> DeleteApplicationUser(string  Id)
        {
           var result = await _servicesCollection.ApplicationUserServices.Delete(Id);
           return HandleResult(result);
        }

    }
}



