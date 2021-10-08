using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [MinLength(3),MaxLength(50)]
        public string BlogName { get; set; }

        [MinLength(3), MaxLength(1000)]
        public string BlogBody { get; set; }

        public string Image { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public virtual List<Comment> Comments { get; set; }
    }
}
