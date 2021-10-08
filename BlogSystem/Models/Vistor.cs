using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    public class Vistor
    {
        [Key, ForeignKey("ApplicationUser")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string VistorId { get; set; }
        [DefaultValue(false)]
        public bool Isdeleted { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
