using System.Data.SqlClient;
using Lesson_6.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_6.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController: ControllerBase
{
    private readonly IConfiguration _configuration;

    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        var orderByColumn = orderBy.ToLower() switch
        {
            "description" => "Description",
            "category" => "Category",
            "area" => "Area",
            _ => "Name"
        };

        var query = $"SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY {orderByColumn}";

        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            using (var sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();

                var reader = sqlCommand.ExecuteReader();
                var animals = new List<GetAnimalsResponse>();

                while (reader.Read())
                {
                    animals.Add(new GetAnimalsResponse(
                        reader.GetInt32(reader.GetOrdinal("IdAnimal")), 
                        reader.GetString(reader.GetOrdinal("Name")),
                        reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        reader.GetString(reader.GetOrdinal("Category")), 
                        reader.GetString(reader.GetOrdinal("Area"))
                    ));
                }

                return Ok(animals);
            }
        }
    }
    
    [HttpPost]
    public IActionResult AddAnimal(AnimalCreateDto newAnimal)
    {
        var query = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";

        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            using (var sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@Name", newAnimal.Name);
                sqlCommand.Parameters.AddWithValue("@Description", newAnimal.Description);
                sqlCommand.Parameters.AddWithValue("@Category", newAnimal.Category);
                sqlCommand.Parameters.AddWithValue("@Area", newAnimal.Area);

                sqlConnection.Open();
                var result = sqlCommand.ExecuteNonQuery();

                return result > 0 ? StatusCode(201):BadRequest();
            }
        }
    }
    
    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, AnimalUpdateDto animalUpdateDto)
    {
        var query = @"
        UPDATE Animal 
        SET Name = @Name, 
            Description = @Description, 
            Category = @Category, 
            Area = @Area 
        WHERE IdAnimal = @IdAnimal";

        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            using (var sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@IdAnimal", idAnimal);
                sqlCommand.Parameters.AddWithValue("@Name", animalUpdateDto.Name);
                sqlCommand.Parameters.AddWithValue("@Description", animalUpdateDto.Description);
                sqlCommand.Parameters.AddWithValue("@Category", animalUpdateDto.Category);
                sqlCommand.Parameters.AddWithValue("@Area", animalUpdateDto.Area);

                sqlConnection.Open();
                int updatedRows = sqlCommand.ExecuteNonQuery();
                if (updatedRows > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
    
    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            using (var sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.AddWithValue("@IdAnimal", idAnimal);

                sqlConnection.Open();

                var affectedRows = sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    return NoContent(); 
                }
                else
                {
                    return NotFound(); 
                }
            }
        }
    }
}