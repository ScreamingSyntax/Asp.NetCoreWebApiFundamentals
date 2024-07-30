using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        [HttpGet("all")]
        public IActionResult GetAllStudents()
        {

            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };
            return Ok(studentNames);
        }

        [HttpGet("some")]
        public IActionResult GetSomeStudents()
        {

            string[] studentNames = new string[] { "John", "Jane", "Mark" };
            return Ok(studentNames);
        }

    }
}
