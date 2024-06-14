using APIUniformes.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUniformes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarmentController : ControllerBase
    {
            private readonly DataContext _context;

            public GarmentController(DataContext context)
            {
                _context = context;

            }

            [HttpPost]
            public async Task<ActionResult<List<GarmentTable>>> AddCharacter(GarmentTable Garment)
            {
                _context.Garment.Add(Garment);
                await _context.SaveChangesAsync();

                return Ok(await _context.Garment.ToListAsync());
            }
            [HttpGet]
            public async Task<ActionResult<List<GarmentTable>>> GetAllCaracters()
            {
                return Ok(await _context.Garment.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<GarmentTable>> GetCharacter(int id)
            {
                var garment = await _context.Garment.FindAsync(id);
                if (garment == null)
                {
                    return BadRequest("Prenda no encontrada");
                }
                return Ok(garment);
            }
            [HttpPut("{id}")]
            public async Task<ActionResult<GarmentTable>> Updategarment(int id, GarmentTable Garment)
            {
                var garmentToUpdate = await _context.Garment.FindAsync(id);
                if (garmentToUpdate == null)
                {
                    return NotFound("Prenda no encontrada");
                }
                garmentToUpdate.type = Garment.type;
                garmentToUpdate.description = Garment.description;
                garmentToUpdate.Status = Garment.Status;

                _context.Garment.Update(garmentToUpdate);
                await _context.SaveChangesAsync();

                return Ok(garmentToUpdate);
            }
            [HttpPut("inactivar/{id}")]
            public async Task<ActionResult> InactivarGarment(int id)
            {
                var garment = await _context.Garment.FindAsync(id);
                if (garment == null)
                {
                    return NotFound("Prenda no encontrad");
                }

                garment.Status = 0;


                _context.Garment.Update(garment);
                await _context.SaveChangesAsync();

                return Ok("Prenda inactivada correctamente");
            }

            [HttpGet("GetActiveGarments")]
            public async Task<ActionResult<IEnumerable<GarmentTable>>> GetActiveGarments()
            {
                return await _context.Garment.Where(m => m.Status == 1).ToListAsync();
            }
        }
}
