using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
	[BindProperties]
	public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        //Post
        public async Task<IActionResult> OnPost()
        {
            //Custome Validation 
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The DisplayOrder cannot exactly match the Name.");
            }
			//Server Side Validations
			if (ModelState.IsValid)
            {
				await _db.Category.AddAsync(Category);
				await _db.SaveChangesAsync();
				TempData["success"] = "Category created successfully";
				return RedirectToPage("Index");
			}
            return Page();
            
        }
    }
}
