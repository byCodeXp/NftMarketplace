using Microsoft.AspNetCore.Mvc;
using Web.Middlewares.ExceptionsHandlerMiddleware.Models;

namespace Web.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ErrorDetails), 400)]
[ProducesResponseType(typeof(ErrorDetails), 500)]
public abstract class ApiController : ControllerBase
{
    
}