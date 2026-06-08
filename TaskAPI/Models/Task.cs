
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskAPI.Models;

public partial class Task
{
    public int? Id { get; set; }
    [Required]
    [StringLength(100, ErrorMessage ="el titulo no debe exeder los 100 caracteres")]
    public string Title { get; set; } = null!;
    [StringLength(500, ErrorMessage = "la descripcion no debe exeder los 500 caracteres")]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsCompleted { get; set; }
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
