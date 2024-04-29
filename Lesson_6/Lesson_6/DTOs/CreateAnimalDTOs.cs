using System.ComponentModel.DataAnnotations;

namespace Lesson_6.DTOs;

public record AnimalCreateDto(
    [Required] [MaxLength(200)] string Name, 
    [Required] [MaxLength(200)] string Description, 
    [Required] [MaxLength(200)] string Category, 
    [Required] [MaxLength(200)] string Area);
