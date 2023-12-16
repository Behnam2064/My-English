using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class Mean
{
    public long Id { get; set; }

    public long MyWordId { get; set; }

    public string Text { get; set; } = null!;

    public virtual MyWord MyWord { get; set; } = null!;
}
}
