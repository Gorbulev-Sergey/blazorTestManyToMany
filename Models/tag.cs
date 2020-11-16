using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blazorTestManyToMany.Models
{
    [Table("tags")]
    public class tag
    {
        [Key]
        public int Id { get; set; }
        public string text { get; set; }
        public List<post> posts { get; set; } = new List<post>();
    }
}
