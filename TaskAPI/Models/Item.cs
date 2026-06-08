using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskAPI.Models;

public partial class Item
{
    public int Id { get; set; }

    public int TaskId { get; set; }
    [Required]
    [StringLength(100, ErrorMessage ="el nombre no puede exceder los 100 caracteres")]
    public string Name { get; set; } = null!;

    public bool IsDone { get; set; }

    [JsonIgnore]
    public virtual Task? Task { get; set; }
}
