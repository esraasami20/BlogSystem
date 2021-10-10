using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSystem.DTO
{
    public class ApproveComment
    {
        [Required]
        public bool IsAppeoved { get; set; }
        [Required]
        public string reason { get; set; }
    }
}
