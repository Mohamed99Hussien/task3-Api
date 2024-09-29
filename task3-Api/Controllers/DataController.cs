using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace task3_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet("anonymous")]
        public IActionResult GetAnonymousData()
        {
            return Ok("This is accessible by anyone.");
        }

        [Authorize]
        [HttpGet("all-users")]
        public IActionResult GetAllUsers()
        {
            return Ok("This is accessible by all authenticated users.");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("teachers-only")]
        public IActionResult GetTeachersOnlyData()
        {
            return Ok("This is accessible by teachers only.");
        }

        [Authorize(Roles = "Student")]
        [HttpGet("students-only")]
        public IActionResult GetStudentsOnlyData()
        {
            return Ok("This is accessible by students only.");
        }
    }
}
