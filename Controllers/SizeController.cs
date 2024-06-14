using APIUniformes.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUniformes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly DataContext _context;

        public SizeController(DataContext context)
        {
            _context = context;

        }

        [HttpPost]
        public async Task<ActionResult<List<SizeTable>>> AddCharacter(SizeTable Size)
        {
            _context.Size.Add(Size);
            await _context.SaveChangesAsync();

            return Ok(await _context.Size.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<SizeTable>>> GetAllCaracters()
        {
            return Ok(await _context.Size.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeTable>> GetCharacter(int id)
        {
            var Size = await _context.Size.FindAsync(id);
            if (Size == null)
            {
                return BadRequest("Size no encontrado");
            }
            return Ok(Size);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<SizeTable>> UpdateSize(int id, SizeTable Size)
        {
            var SizeToUpdate = await _context.Size.FindAsync(id);
            if (SizeToUpdate == null)
            {
                return NotFound("Size no encontrado");
            }
            SizeToUpdate.Size = Size.Size;
            SizeToUpdate.Price = Size.Price;
            SizeToUpdate.Status = Size.Status;

            _context.Size.Update(SizeToUpdate);
            await _context.SaveChangesAsync();

            return Ok(SizeToUpdate);
        }
        [HttpPut("inactivar/{id}")]
        public async Task<ActionResult> InactivarSize(int id)
        {
            var Size = await _context.Size.FindAsync(id);
            if (Size == null)
            {
                return NotFound("Size no encontrado");
            }

            Size.Status = 0;


            _context.Size.Update(Size);
            await _context.SaveChangesAsync();

            return Ok("Size inactivado correctamente");
        }

        [HttpGet("GetActiveSizes")]
        public async Task<ActionResult<IEnumerable<SizeTable>>> GetActiveSizes()
        {
            return await _context.Size.Where(m => m.Status == 1).ToListAsync();
        }
    }
}
