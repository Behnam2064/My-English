using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ME.Entities.Database
{
    public partial class Wfile
    {
        public Guid Id { get; set; }

        public long UserId { get; set; }

        public long? MyWordId { get; set; }

        [StringLength(128)]
        public string? FileName { get; set; }

        public long? FolderId { get; set; }

        [StringLength(4)]
        public string? FileType { get; set; }

        public int? Category { get; set; }

        public DateTime AddedTime { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public byte[]? FileData { get; set; }

        public virtual MyWord? MyWord { get; set; }
    }
}
