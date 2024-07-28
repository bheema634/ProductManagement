using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product_Management_System.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected BaseApiController() : base()
        {

        }
        protected IActionResult OKorNotFount<T>(T result)
        {
            return (IActionResult)Ok(result) ?? NotFound();
        }
    }
}
