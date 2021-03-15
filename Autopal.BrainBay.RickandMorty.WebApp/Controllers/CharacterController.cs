using System;
using System.Linq;
using System.Net;
using Autopal.BrainBay.RickandMorty.Domain.Model;
using Autopal.BrainBay.RickandMorty.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Autopal.BrainBay.RickandMorty.WebApp.Controllers
{
    [FromDatabaseActionFilter]
    public class CharacterController : Controller
    {
        private readonly ILogger<CharacterController> _logger;

        private readonly ICharacterService _service;

        public CharacterController(ICharacterService service, ILogger<CharacterController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET /[?pageSize=3&pageIndex=10]
        public ActionResult Index(int pageSize = 10, int pageIndex = 0)
        {
            _logger.LogInformation($"Now loading... /Character/Index?pageSize={pageSize}&pageIndex={pageIndex}");
            var paginatedItems = _service.GetCharactersPaginated(pageSize, pageIndex);
            return View(paginatedItems);
        }

        // GET: CharacterController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new StatusCodeResult((int) HttpStatusCode.BadRequest);
            _logger.LogInformation($"Now loading... /Character/Details?id={id}");
            var character = _service.FindCharacter(id.Value);
            if (character == null) return new NotFoundResult();

            return View(character);
        }

        // GET: CharacterController/Create
        public ActionResult Create()
        {
            _logger.LogInformation("Now loading... /Character/Create");
            ViewBag.Gender = new SelectList(_service.GetCharacterGenders(), "Id", "Name");
            ViewBag.Status = new SelectList(_service.GetCharacterStatuses(), "Id", "Name");
            var locations = _service.GetLocations();
            ViewBag.Location = new SelectList(locations, "Id", "Name");
            ViewBag.Origin = new SelectList(locations, "Id", "Name");

            return View(new Character());
        }

        // POST: CharacterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var character = new Character();
                var locations = _service.GetLocations();
                if (ModelState.IsValid)
                {
                    if (collection.TryGetValue(nameof(character.Name), out var name))
                        character.Name = name;
                    _logger.LogInformation($"Now processing... /Character/Create?characterName={character.Name}");
                    if (collection.TryGetValue(nameof(character.Species), out var species))
                        character.Species = species;
                    if (collection.TryGetValue(nameof(character.Type), out var type))
                        character.Type = type;
                    if (collection.TryGetValue(nameof(character.Status), out var status))
                        character.Status = (CharacterStatus) Enum.Parse(typeof(CharacterStatus), status);
                    if (collection.TryGetValue(nameof(character.Gender), out var gender))
                        character.Gender = (CharacterGender) Enum.Parse(typeof(CharacterGender), gender);


                    if (collection.TryGetValue(nameof(character.Origin), out var originValues))
                        if (int.TryParse(originValues.SingleOrDefault(), out var origin))
                            character.Origin = locations.FirstOrDefault(x => x.Id == origin);
                    if (collection.TryGetValue(nameof(character.Location), out var locationValues))
                        if (int.TryParse(locationValues.SingleOrDefault(), out var location))
                            character.Location = locations.FirstOrDefault(x => x.Id == location);
                    _service.CreateCharacter(character);
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Gender = new SelectList(_service.GetCharacterGenders(), "Id", "Name", character.Gender);
                ViewBag.Status = new SelectList(_service.GetCharacterStatuses(), "Id", "Name", character.Status);
                ViewBag.Location = new SelectList(locations, "Id", "Name", character.Location?.Id);
                ViewBag.Origin = new SelectList(locations, "Id", "Name", character.Origin?.Id);
                return View(character);
            }
            catch
            {
                return View();
            }
        }

        // GET: CharacterController/Delete/5
        public ActionResult Delete(int? id)
        {
            _logger.LogInformation($"Now loading... /Character/Delete?id={id}");
            if (id == null) return new StatusCodeResult((int) HttpStatusCode.BadRequest);
            var character = _service.FindCharacter(id.Value);
            if (character == null) return NotFound();

            _service.RemoveCharacter(character);
            return RedirectToAction("Index");
        }

        // POST: CharacterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}