using System.ComponentModel.DataAnnotations;

namespace task3_Api.Models
{
    public class SchoolUser
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public string SchoolName { get; set; }

        public double PerformanceRate { get; set; }

        public string Role { get; set; } // Role can be "Teacher" or "Student"
    }
}
