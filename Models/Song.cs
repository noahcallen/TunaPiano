using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Tuna.Models;

public class Song
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public int ArtistId { get; set; }
    public string Album { get; set; }
    public decimal Length { get; set; }
    public List<Genre> Genres { get; set; }
    public Artist Artist { get; set; }
}