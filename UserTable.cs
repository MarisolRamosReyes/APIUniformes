using System.ComponentModel.DataAnnotations;

namespace APIUniformes
{
    public class UserTable
    {
        [Key]
        public int IDUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public int Status { get; set; } 
    }
}
