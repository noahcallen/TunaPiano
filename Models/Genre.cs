using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Tuna.Models;

public class Genre
{
    public int Id { get; set; }
    [Required]
    public string Description { get; set; }
    public List<Song> Songs { get; set; }
}