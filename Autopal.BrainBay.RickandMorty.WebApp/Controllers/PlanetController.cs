using System.Net;
using Autopal.BrainBay.RickandMorty.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Autopal.BrainBay.RickandMorty.WebApp.Controllers
{
    [FromDatabaseActionFilter]
    public class PlanetController : Controller
    {
        private readonly ILogger<CharacterController> _logger;

        private readonly ICharacterService _service;

        public PlanetController(ICharacterService service, ILogger<CharacterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET /[?pageSize=3&pageIndex=10]
        public ActionResult Index(int pageSize = 10, int pageIndex = 0)
        {
            _logger.LogInformation($"Now loading... /Character/Index?pageSize={pageSize}&pageIndex={pageIndex}");
            var paginatedItems = _service.GetLocationsPaginated(pageSize, pageIndex);
            return View(paginatedItems);
        }

        // GET: PlanetController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null) return new StatusCodeResult((int) HttpStatusCode.BadRequest);
            _logger.LogInformation($"Now loading... /Planet/Details?id={id}");
            var location = _service.FindLocation(id);
            if (location == null) return new NotFoundResult();

            return View(location);
        }
    }
}