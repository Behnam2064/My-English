using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class Wdescription
{
    public Guid Id { get; set; }

    public long UserId { get; set; }

    public long MyWordId { get; set; }

    public byte[] Description { get; set; } = null!;

    public virtual MyWord MyWord { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
}
