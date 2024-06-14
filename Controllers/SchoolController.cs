using APIUniformes.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUniformes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly DataContext _context;

        public SchoolController(DataContext context)
        {
            _context = context;

        }

        [HttpPost]
        public async Task<ActionResult<List<SchoolTable>>> AddCharacter(SchoolTable School)
        {
            _context.School.Add(School);
            await _context.SaveChangesAsync();

            return Ok(await _context.School.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<SchoolTable>>> GetAllCaracters()
        {
            return Ok(await _context.School.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolTable>> GetCharacter(int id)
        {
            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return BadRequest("Escuela no encontrada");
            }
            return Ok(school);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SchoolTable>> Updateschool(int id, SchoolTable School)
        {
            var schoolToUpdate = await _context.School.FindAsync(id);
            if (schoolToUpdate == null)
            {
                return NotFound("Escuela no encontrado");
            }
            schoolToUpdate.name = School.name;
            schoolToUpdate.address = School.address;
            schoolToUpdate.phone = School.phone;
            schoolToUpdate.Status = School.Status;

            _context.School.Update(schoolToUpdate);
            await _context.SaveChangesAsync();

            return Ok(schoolToUpdate);
        }
        [HttpPut("inactivar/{id}")]
        public async Task<ActionResult> InactivarSchool(int id)
        {
            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound("Escuela no encontrado");
            }

            school.Status = 0;


            _context.School.Update(school);
            await _context.SaveChangesAsync();

            return Ok("Escuela inactivado correctamente");
        }

        [HttpGet("GetActiveSchools")]
        public async Task<ActionResult<IEnumerable<SchoolTable>>> GetActiveSchools()
        {
            return await _context.School.Where(m => m.Status == 1).ToListAsync();
        }
    }
}
