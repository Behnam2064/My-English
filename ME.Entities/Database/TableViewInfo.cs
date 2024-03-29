﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ME.Entities.Database
{
    public partial class TableViewInfo
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long TableId { get; set; }

        [StringLength(128)]
        public string TableName { get; set; } = null!;

        public DateTime TimeView { get; set; }

        public DateTime? TimeEndView { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
