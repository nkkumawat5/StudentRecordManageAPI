using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController(StudentDbContext context) : ControllerBase
    {
        // get all data 
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
        
            return Ok(await context.Students.ToListAsync());
        }

        //public async Task<ActionResult<List<Student>>> GetStudents()
        //{
        //    return Ok(context.Students.ToList());
        //}


        // get data using ID
        [HttpGet("{ID}")]
        public async Task<ActionResult<Student>> GetStudentByID(int ID)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.Id == ID);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        // create new student 
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Students.Add(student);
                    await context.SaveChangesAsync();
                    return Ok(student);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // update data 
        [HttpPut]
        public async Task<ActionResult<Student>> UpdateCreateStudent(int ID, Student Std)
        {
            if(ID != Std.Id)
            {
                return BadRequest();
            }
            context.Entry(Std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(Std);
        }

        // delete particular id data        
        [HttpDelete("{ID}")]
        public async Task<ActionResult<Student>> DeleteStudent(int ID)
        {
            var std = await context.Students.FirstOrDefaultAsync(s => s.Id == ID);
            if (std == null)
            {
                return NotFound();
            }
            context.Students.Remove(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        // all data delete 
        [HttpDelete] 
        public async Task<ActionResult> DeleteAllStudents() 
        {
            var students = await context.Students.ToListAsync();
            context.Students.RemoveRange(students);
            await context.SaveChangesAsync();
            return NoContent();
        }

      //  get data using Name
        [HttpGet("Name")]
    public async Task<ActionResult<Student>> GetStudentByName(string Name)
    {
        var student = await context.Students.FirstOrDefaultAsync(s => s.StudentName == Name);
        if (student == null)
        {
            return NotFound();
        }
        return student;
    }

    //[HttpGet("name")] // Change the route parameter to "name"
    //public async Task<ActionResult<Student>> GetStudentByName(string name) // Change the parameter type to string
    //{
    //    var student = await context.Students.FirstOrDefaultAsync(s => s.StudentName == name); // Query the database for a student with the specified name
    //    if (student == null)
    //    {
    //        return NotFound(); // If no student is found with the specified name, return a 404 Not Found response
    //    }
    //    return student; // If a student with the specified name is found, return the student
    //}


}
}

