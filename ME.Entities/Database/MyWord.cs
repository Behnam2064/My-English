using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class MyWord
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? FolderId { get; set; }

    public string Name { get; set; } = null!;

    public int? WordType { get; set; }

    public string? Dictation { get; set; }

    public DateTime AddDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Folder? Folder { get; set; }

    public virtual ICollection<Mean> Means { get; set; } = new List<Mean>();

    public virtual ICollection<Opposite> OppositeMyWordId1Navigations { get; set; } = new List<Opposite>();

    public virtual ICollection<Opposite> OppositeMyWords { get; set; } = new List<Opposite>();

    public virtual ICollection<Synonym> SynonymMyWordId1Navigations { get; set; } = new List<Synonym>();

    public virtual ICollection<Synonym> SynonymMyWords { get; set; } = new List<Synonym>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Wdescription> Wdescriptions { get; set; } = new List<Wdescription>();

    public virtual ICollection<Wfile> Wfiles { get; set; } = new List<Wfile>();
}
}
