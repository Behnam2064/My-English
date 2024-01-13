using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ME.Entities.Database
{
    public partial class Synonym
    {
        public long Id { get; set; }

        public long MyWordId { get; set; }

        [NotMapped]
        public long MyWordId1 { get; set; }

        public virtual MyWord MyWord { get; set; } = null!;

        [NotMapped]
        public virtual MyWord MyWordId1Navigation { get; set; } = null!;
    }
}
