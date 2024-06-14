using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace APIUniformes
{
    public class GarmentTable
    {
        [Key]
        public int idG { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public int Status { get; set; }
    }
}
