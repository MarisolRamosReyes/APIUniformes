using APIUniformes.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIUniformes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;

        }

        [HttpPost]
        public async Task<ActionResult<List<UserTable>>> AddCharacter(UserTable Usuario)
        {
            _context.Usuario.Add(Usuario);
            await _context.SaveChangesAsync();

            return Ok(await _context.Usuario.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<UserTable>>> GetAllCaracters()
        {
            return Ok(await _context.Usuario.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTable>> GetCharacter(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return BadRequest("usuario no encontrado");
            }
            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserTable>> UpdateUsuario(int id, UserTable Usuario)
        {
            var UsuarioToUpdate = await _context.Usuario.FindAsync(id);
            if (UsuarioToUpdate == null)
            {
                return NotFound("Usuario no encontrado");
            }
            UsuarioToUpdate.Nombre = Usuario.Nombre;
            UsuarioToUpdate.Contrasena = Usuario.Contrasena;
            UsuarioToUpdate.Status = Usuario.Status;

            _context.Usuario.Update(UsuarioToUpdate);
            await _context.SaveChangesAsync();

            return Ok(UsuarioToUpdate);
        }
        [HttpPut("inactivar/{id}")]
        public async Task<ActionResult> InactivarUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            
            usuario.Status = 0;


            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario inactivado correctamente");
        }

        [HttpGet("GetActiveUsuarios")]
        public async Task<ActionResult<IEnumerable<UserTable>>> GetActiveUsuarios()
        {
            return await _context.Usuario.Where(m => m.Status == 1).ToListAsync();

        }

        [HttpGet("autenticar")]
        public async Task<ActionResult<UserTable>> AutenticarUsuario([FromQuery] string nombre, [FromQuery] string contrasena)
        {
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Nombre == nombre && u.Contrasena == contrasena);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos");
            }
            return Ok(usuario);
        }
    }
}
