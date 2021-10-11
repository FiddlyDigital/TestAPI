using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestAPI.DAL.Configuration;

namespace TestAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase

    {
        public readonly ILogger<UsersController> _logger;
        public readonly IUnitOfWork _unitOfWork;

        public BaseController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
    }
}
