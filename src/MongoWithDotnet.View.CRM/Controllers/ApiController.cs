using Microsoft.AspNetCore.Mvc;

namespace MongoWithDotnet.View.CRM.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
}