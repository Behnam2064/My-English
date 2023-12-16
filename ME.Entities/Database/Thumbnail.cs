using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{
    public partial class Thumbnail
{
    public Guid Id { get; set; }

    /// <summary>
    /// like WFile id
    /// </summary>
    public long TableId { get; set; }

    /// <summary>
    /// like My image.jpg file id in Wfile Table
    /// </summary>
    public Guid? FileStreamRowId { get; set; }

    /// <summary>
    /// like a folder id
    /// </summary>
    public long? RowId { get; set; }

    public byte[] FileData { get; set; } = null!;
}
}
