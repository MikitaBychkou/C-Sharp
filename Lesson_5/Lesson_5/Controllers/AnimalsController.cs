using System.Data.Common;
using Lesson_5.Model;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_5.Controllers;

[ApiController]
[Route("animals")]
public class AnimalsController: ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Id = 1, Kategoria = "kot", Imie = "pit", Masa = 10, KolorSiersci = "zielony" },
        new Animal { Id = 2, Kategoria = "pies", Imie = "max", Masa = 15, KolorSiersci = "bialy" },
        new Animal { Id = 3, Kategoria = "kot", Imie = "rob", Masa = 20, KolorSiersci = "czarny" }
    };
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        return Ok(animal);

    }

    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.Id == id);
        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return NoContent();
    }
    
    [HttpDelete]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.Id == id);
        if (animalToDelete == null)
        {
            return NotFound($"Animal with id {id} was not  found");
        }

        _animals.Remove(animalToDelete);
        return NoContent();
    }
}