using IMS.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult HandleException(Exception ex)
        {
            if (ex is NotFoundException)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            
            if (ex is ValidationException validationEx)
            {
                return BadRequest(new { success = false, message = ex.Message, errors = validationEx.Errors });
            }
            
            // Log the exception here if needed
            
            return StatusCode(500, new { success = false, message = "An error occurred while processing your request" });
        }
        
        protected IActionResult NotFound(string message = "Resource not found")
        {
            return StatusCode(404, new { success = false, message });
        }
        
        protected IActionResult BadRequest(string message = "Invalid request")
        {
            return StatusCode(400, new { success = false, message });
        }
        
        protected IActionResult Unauthorized(string message = "You are not authorized to perform this action")
        {
            return StatusCode(401, new { success = false, message });
        }
        
        protected IActionResult Forbidden(string message = "You do not have permission to access this resource")
        {
            return StatusCode(403, new { success = false, message });
        }
        
        protected IActionResult ServerError(string message = "An error occurred while processing your request")
        {
            return StatusCode(500, new { success = false, message });
        }
    }
} 