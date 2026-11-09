using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (item == null)
            {
                return NotFound();
            }
            Enrollment = item;
            LoadLists(Enrollment.CourseID, Enrollment.StudentID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists(Enrollment.CourseID, Enrollment.StudentID);
                return Page();
            }

            _context.Attach(Enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(Enrollment.EnrollmentID))
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

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }

        private void LoadLists(object? selectedCourseID = null, object? selectedStudentID = null)
        {
            // Dropdown shows "ID - Name" instead of only the ID.
            ViewData["CourseID"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    c.CourseID,
                    DisplayText = c.CourseID + " - " + c.Title
                }),
                "CourseID",
                "DisplayText",
                selectedCourseID);
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new
                {
                    s.StudentID,
                    DisplayText = s.StudentID + " - " + s.LastName + " " + s.FirstName
                }),
                "StudentID",
                "DisplayText",
                selectedStudentID);
        }
    }
}
