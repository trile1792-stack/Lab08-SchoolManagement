using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreMVC.Models;

namespace SchoolAppCoreMVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enrollments.Include(x => x.Course).Include(x => x.Student).ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.Include(x => x.Course).Include(x => x.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    c.CourseID,
                    DisplayText = c.CourseID + " - " + c.Title
                }),
                "CourseID",
                "DisplayText");
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new
                {
                    s.StudentID,
                    DisplayText = s.StudentID + " - " + s.LastName + " " + s.FirstName
                }),
                "StudentID",
                "DisplayText");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    c.CourseID,
                    DisplayText = c.CourseID + " - " + c.Title
                }),
                "CourseID",
                "DisplayText",
                enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new
                {
                    s.StudentID,
                    DisplayText = s.StudentID + " - " + s.LastName + " " + s.FirstName
                }),
                "StudentID",
                "DisplayText",
                enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    c.CourseID,
                    DisplayText = c.CourseID + " - " + c.Title
                }),
                "CourseID",
                "DisplayText",
                enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new
                {
                    s.StudentID,
                    DisplayText = s.StudentID + " - " + s.LastName + " " + s.FirstName
                }),
                "StudentID",
                "DisplayText",
                enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    c.CourseID,
                    DisplayText = c.CourseID + " - " + c.Title
                }),
                "CourseID",
                "DisplayText",
                enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(
                _context.Students.Select(s => new
                {
                    s.StudentID,
                    DisplayText = s.StudentID + " - " + s.LastName + " " + s.FirstName
                }),
                "StudentID",
                "DisplayText",
                enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.Include(x => x.Course).Include(x => x.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}
