using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ME.Entities.Database
{
    public partial class Folder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }

        public long? ParentId { get; set; }

        /// <summary>
        /// This is column related to Thumbnail table (icon save at that table)
        /// </summary>
        public long? MyIconId { get; set; }

        public long? TagId { get; set; }

        [StringLength(128)]
        public string Name { get; set; } = null!;

        public DateTime AddDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        public virtual ICollection<MyWord> MyWords { get; set; }

        public virtual User User { get; set; } = null!;

        public Folder()
        {
            MyWords = new List<MyWord>();
        }
    }
}
