using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreenShare.Controllers
{
    [Route("api/[controller]")]
    
    public abstract class BaseApiController<T>: ControllerBase
    {
    }
}
