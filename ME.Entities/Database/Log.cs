using ME.Entities.Generals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ME.Entities.Database
{
    public partial class Log
    {
        public long Id { get; set; }

        public long? UserId { get; set; }

        public DateTime AddedTime { get; set; }

        public string Info { get; set; } = null!;

        /// <summary>
        /// like error, information, warning
        /// </summary>
        public int LogType { get; set; }

        /// <summary>
        /// like user or developer log
        /// </summary>
        public LogScope LogScope { get; set; }

        /// <summary>
        /// Json format
        /// </summary>
        public string? Arguments { get; set; }

        [NotMapped] // New version
        public string? ExceptionName { get; set; }
        [NotMapped]
        public string? ClassName { get; set; }

        [NotMapped] // New version
        public string? MethodName { get; set; }

        [NotMapped] // New version
        public int? LineNumber { get; set; }

        public virtual User? User { get; set; }
    }
}
