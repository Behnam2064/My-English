using System;
using System.Collections.Generic;

namespace ME.Entities.Database
{

public partial class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Mobile { get; set; }

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<MemoTable> MemoTables { get; set; } = new List<MemoTable>();

    public virtual ICollection<MyWord> MyWords { get; set; } = new List<MyWord>();

    public virtual ICollection<TableViewInfo> TableViewInfos { get; set; } = new List<TableViewInfo>();

    public virtual ICollection<Wdescription> Wdescriptions { get; set; } = new List<Wdescription>();
}
}
