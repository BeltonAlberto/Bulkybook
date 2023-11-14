using System.ComponentModel.DataAnnotations;

namespace Bulkybook;

public class Category
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
}
