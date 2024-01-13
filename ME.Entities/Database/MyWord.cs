using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ME.Entities.Database
{
    public partial class MyWord
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long? FolderId { get; set; }

        [StringLength(1000)]
        public string Name { get; set; } = null!;

        public int? WordType { get; set; }

        [StringLength(1000)]
        public string? Dictation { get; set; }

        public DateTime AddDateTime { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Folder? Folder { get; set; }

        public virtual ICollection<Mean> Means { get; set; } = new List<Mean>();

        [NotMapped]
        public virtual ICollection<Opposite> OppositeMyWordId1Navigations { get; set; } = new List<Opposite>();
                
        public virtual ICollection<Opposite> OppositeMyWords { get; set; } = new List<Opposite>();

        [NotMapped]
        public virtual ICollection<Synonym> SynonymMyWordId1Navigations { get; set; } = new List<Synonym>();

        public virtual ICollection<Synonym> SynonymMyWords { get; set; } = new List<Synonym>();

        public virtual User User { get; set; } = null!;

        public virtual ICollection<WDescription> Wdescriptions { get; set; } = new List<WDescription>();

        public virtual ICollection<Wfile> Wfiles { get; set; } = new List<Wfile>();
    }
}
