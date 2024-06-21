using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Database
{
    public class Dictation
    {
        public long Id { get; set; }
        public long MyWordId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        public virtual MyWord? MyWord { get; set; }
    }
}
