using Microsoft.AspNetCore.Mvc;

namespace QPizza.API.Controllers
{
    [Route("[controller]")]
    public class PizzaController : ApiController
    {
        [HttpGet]
        public IActionResult ListPizzas()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
