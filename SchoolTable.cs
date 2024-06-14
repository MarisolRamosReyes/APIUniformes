using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace APIUniformes
{
    public class SchoolTable
    {

        [Key]
        public int idSc { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int Status { get; set; }
    }

}
