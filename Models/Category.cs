using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulkybook;

public class Category
{
    [Key]
    public int Id { get; set; }
    public required string? Name { get; set; }
    [Range(1, 100, ErrorMessage = "Display Order must between 1 - 100")]
    [DisplayName("Display Order")]
    public int DisplayOrder { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
