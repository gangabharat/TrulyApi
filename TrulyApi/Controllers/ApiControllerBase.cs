using Microsoft.AspNetCore.Mvc;
using TrulyApi.Services.Interfaces;

namespace TrulyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        //private readonly IDateTime _dateTime;

        //public IDateTime DateService { get { return _dateTime; } }
        //public ApiControllerBase(IDateTime dateTime)
        //{
        //    _dateTime = dateTime;
        //}
    }
}
