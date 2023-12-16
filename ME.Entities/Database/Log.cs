using System;
using System.Collections.Generic;

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
    public int LogScope { get; set; }

    public string? ExceptionName { get; set; }

    public string? ClassName { get; set; }

    public string? MethodName { get; set; }

    public int? LineNumber { get; set; }

    public virtual User? User { get; set; }
}
}
