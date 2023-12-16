using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class Opposite
{
    public long Id { get; set; }

    public long MyWordId { get; set; }

    public long MyWordId1 { get; set; }

    public virtual MyWord MyWord { get; set; } = null!;

    public virtual MyWord MyWordId1Navigation { get; set; } = null!;
}
}
