using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blazorTestManyToMany.Models
{
    [Table("posts")]
    public class post
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public List<tag> tags { get; set; } = new List<tag>();
    }
}
