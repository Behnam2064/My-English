using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Generals
{
    public class FileStreamGeneratorArguments
    {
        public required string FileStreamPath { get; set; }

        /// <summary>
        /// This folder name should not exist
        /// </summary>
        public required string FolderName { get; set; }

        public required string FileGroupName { get; set; }

        /// <summary>
        /// To create column saving binary
        /// </summary>
        public required string TableName { get; set; }

        /// <summary>
        /// Column to save binary data
        /// </summary>
        public required string ColumnName { get; set; }

        public required string DatabaseName { get; set; }

        public required string PrimarykeyName  { get; set; }

        
    }
}
