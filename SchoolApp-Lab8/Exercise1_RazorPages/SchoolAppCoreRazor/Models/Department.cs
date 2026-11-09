using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppCoreRazor.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public required string DepartmentName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
