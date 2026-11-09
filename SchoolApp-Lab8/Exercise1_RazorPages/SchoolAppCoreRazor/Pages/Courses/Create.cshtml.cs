using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            LoadLists();
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists(Course.DepartmentID);
                return Page();
            }

            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private void LoadLists(object? selectedDepartmentID = null)
        {
            // Dropdown shows "ID - Name" instead of only the ID.
            ViewData["DepartmentID"] = new SelectList(
                _context.Departments.Select(d => new
                {
                    d.DepartmentID,
                    DisplayText = d.DepartmentID + " - " + d.DepartmentName
                }),
                "DepartmentID",
                "DisplayText",
                selectedDepartmentID);
        }
    }
}
