using Microsoft.AspNetCore.Mvc;
namespace Bulkybook.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> categoryTable = _dbContext.Categories.ToList();
        return View(categoryTable);

    }
}
