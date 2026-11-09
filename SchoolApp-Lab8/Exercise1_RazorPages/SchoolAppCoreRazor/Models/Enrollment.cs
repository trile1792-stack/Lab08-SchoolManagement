using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SchoolAppCoreRazor.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Grade { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
