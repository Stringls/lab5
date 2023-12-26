using Microsoft.AspNetCore.Mvc;
using WebApplicationExample2.Models;

namespace WebApplicationExample2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // If the ModelState is valid, process the Person object
            try
            {
                // You can pass the person data to a view or any other action
                return RedirectToAction("PersonDetails", new { id = person.Id });
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or return an appropriate error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("PersonDetails/{id}")]
        public IActionResult PersonDetails(int id)
        {
            // Retrieve the person from the database or any other data source
            var person = new Person
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };

            // Pass the person data to a view or any other action
            return View("PersonDetails", person);
        }
    }

}
