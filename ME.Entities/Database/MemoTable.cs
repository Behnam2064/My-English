using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class MemoTable
{
    public long Id { get; set; }

    /// <summary>
    /// This is memo which user create that (Can suggestions to owner user or all other)
    /// </summary>
    public long UserId { get; set; }

    public long Section { get; set; }

    public string Text { get; set; } = null!;

    public DateTime AddDateTime { get; set; }

    public long CountSelected { get; set; }

    public virtual User User { get; set; } = null!;
}
}
