using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ME.Entities.Database
{

    public partial class User
    {
        public long Id { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [MaxLength(20)]
        public string Username { get; set; } = null!;
        [MaxLength(64)]
        public string? Password { get; set; }
        [MaxLength(13)]
        public string? Mobile { get; set; }

        public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

        public virtual ICollection<MemoTable> MemoTables { get; set; } = new List<MemoTable>();

        public virtual ICollection<MyWord> MyWords { get; set; } = new List<MyWord>();

        public virtual ICollection<TableViewInfo> TableViewInfos { get; set; } = new List<TableViewInfo>();

        public virtual ICollection<WDescription> Wdescriptions { get; set; } = new List<WDescription>();
    }
}
