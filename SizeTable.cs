using System.ComponentModel.DataAnnotations;

namespace APIUniformes
{
    public class SizeTable
    {
        [Key]
        public int idS { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public int Status { get; set; }
    }
}
