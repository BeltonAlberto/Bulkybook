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

    //add new Category
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category userCategory)
    {

        if (userCategory is null)
        {
            return BadRequest(); //
        }

        if (string.IsNullOrWhiteSpace(userCategory.Name))
        {
            ModelState.AddModelError("Name", "Name cannot be empty");
        }
        else if (userCategory.Name.Any(char.IsDigit))
        {
            ModelState.AddModelError("Name", "Name cannot contain numbers");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(userCategory);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index", "Category");
        }
        else
        {
            return View(userCategory);
        }
    }

    // Edit Category 
    [HttpGet]
    public IActionResult Edit(int? id)
    {

        if (id is null || id is 0)
        {
            return NotFound();
        }
        Category? userCategoryFromDb = _dbContext.Categories.Find(id);
        if (userCategoryFromDb is null)
        {
            return NotFound();
        }
        return View(userCategoryFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category userCategory)
    {
        if (userCategory is null)
        {
            return BadRequest(); //
        }

        if (string.IsNullOrWhiteSpace(userCategory.Name))
        {
            ModelState.AddModelError("Name", "Name cannot be empty");
        }
        else if (userCategory.Name.Any(char.IsDigit))
        {
            ModelState.AddModelError("Name", "Name cannot contain numbers");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(userCategory);
            _dbContext.SaveChanges();
            TempData["success"] = "Category Modified Successfully";
            return RedirectToAction("Index", "Category");
        }
        else
        {
            return View(userCategory);
        }

    }
    // destroy an existing Category
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null || id is 0)
        {
            return NotFound();
        }
        Category? userCategoryFromDb = _dbContext.Categories.Find(id);
        if (userCategoryFromDb is null)
        {
            return NotFound();
        }
        return View(userCategoryFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category userCategory)
    {
        if (userCategory is null)
        {
            return NotFound();
        }
        _dbContext.Categories.Remove(userCategory);
        _dbContext.SaveChanges();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index", "Category");
    }
}

