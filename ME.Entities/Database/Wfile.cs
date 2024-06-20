using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ME.Entities.Database
{
    public partial class WFile
    {
        public Guid Id { get; set; }

        /// <summary>
        /// You don't need to disable the enforcement of the foreign key constraint 
        /// for your purpose, you just need to allow NULL values for the foreign key 
        /// which is called an optional one-to-many relationship (in contrast to a 
        /// required relationship which doesn't allow NULL values of the foreign key).
        /// https://stackoverflow.com/questions/10324231/how-to-prevent-code-first-from-enabling-foreign-key-constraint-on-a-relationship
        /// </summary>
        public long? UserId { get; set; }

        public long? MyWordId { get; set; }

        [StringLength(128)]
        public required string FileName { get; set; }

        public long? FolderId { get; set; }

        [StringLength(4)]
        public string? FileType { get; set; }

        public int? Category { get; set; }

        public DateTime AddedTime { get; set; }

        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// This field is created manually by Addo.Net.
        /// The reason is to be able to use the FileStream feature in EF.
        /// (Just testing)
        /// </summary>
        //[NotMapped]
        public byte[]? FileData { get; set; }

        /// <summary>
        /// This field is created manually by Addo.Net.
        /// The reason is to be able to use the FileStream feature in EF.
        /// (Just testing)
        /// </summary>
        //[NotMapped]
        //public Guid IdFile { get; set; }

        [NotMapped]
        public virtual MyWord? MyWord { get; set; }
    }
}
