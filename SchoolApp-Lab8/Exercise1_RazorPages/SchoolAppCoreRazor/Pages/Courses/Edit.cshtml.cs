using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            if (item == null)
            {
                return NotFound();
            }
            Course = item;
            LoadLists(Course.DepartmentID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists(Course.DepartmentID);
                return Page();
            }

            _context.Attach(Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseID == id);
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
