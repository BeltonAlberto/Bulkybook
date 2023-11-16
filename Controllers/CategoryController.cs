using Microsoft.AspNetCore.Mvc;
namespace Bulkybook.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Category> categoryTable = _dbContext.Categories.ToList();
        return View(categoryTable);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //add new Category to Database
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category userCategory)
    {

        if (userCategory == null)
        {
            return BadRequest(); //
        }

        //if (string.IsNullOrWhiteSpace(userCategory.Name))
        //{
        //    ModelState.AddModelError("Name", "Name cannot be empty");
        //}


        if (ModelState.IsValid)
        {
            if (int.TryParse(userCategory.Name.Replace(' ', '0').Trim(), out _))
            {
                ModelState.AddModelError("Name", "Name cannot be a number");
            }
            _dbContext.Categories.Add(userCategory);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        else
        {
            return View(userCategory);
        }
    }
}

