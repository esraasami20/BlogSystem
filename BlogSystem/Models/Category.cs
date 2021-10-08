using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string CategoryName { get; set; }


        public virtual List<Blog> Blogs { get; set; }
    }
}
