using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooWebApi.Models;
using ZooWebApi.Services;

namespace ZooWebApi.Controllers
{
    [Route("v1/Animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private IDb _db;

        public AnimalsController(IDb db)
        {
            _db = db;
        }

        [HttpGet("GetAnimals")]
        public async Task<IActionResult> GetAnimals()
        {
            List<Animal> animals = await _db.GetAnimalsFromDb();

            return Ok(animals);
        }

        [HttpPost("UpdateAnimal")]
        public async Task<IActionResult> InsertOrUpdateAnimal([FromBody] Animal animal)
        {
            IActionResult result = BadRequest();

            List<Animal> animals = await _db.GetAnimalsFromDb();
            Animal existingAnimal = animals.FirstOrDefault(a => a.ID == animal.ID);
            if (existingAnimal != null)
            {
                animals = await _db.UpdateAnimal(animal);
                result = Ok(animals);
            }
            else
            {
                animals = await _db.InsertAnimal(animal);
                result = CreatedAtAction("InsertOrUpdateAnimal", animals);
            }

            return result;
        }
    }
}
