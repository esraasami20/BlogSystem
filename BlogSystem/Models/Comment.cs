using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        [MaxLength(200),MinLength(3)]
        public string CommentBody { get; set; }

        [DefaultValue(false)]
        public bool IsAppeoved { get; set; }
        [MaxLength(200), MinLength(3)]
        public string reason { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        [ForeignKey("Vistor")]
        public string VistorId { get; set; }
        public Vistor Vistor { get; set; }


    }
}
