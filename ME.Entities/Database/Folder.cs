using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class Folder
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? ParentId { get; set; }

    /// <summary>
    /// This is column related to Thumbnail table (icon save at that table)
    /// </summary>
    public long? MyIconId { get; set; }

    public long? TagId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime AddDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<MyWord> MyWords { get; set; } = new List<MyWord>();

    public virtual User User { get; set; } = null!;
}
}
