using ME.Entities.Database;
using ME.Entities.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.DataSource.Contexts
{
    public class MySelfStudyDictionary2Db : DbContext, IDataBaseContext
    {

        #region Fields

        private readonly DbContextOptions options;

        private static bool IsEnsureCreated;

        #endregion

        #region Constructors
        public MySelfStudyDictionary2Db(DbContextOptions options,IConfiguration configuration, IFileStreamGenerator streamGenerator) : base(options)
        {
            this.options = options;

            if (!IsEnsureCreated)
            {
                IsEnsureCreated = true;
                Task<bool> task = this.Database.EnsureCreatedAsync();
                task.Wait();
                if(task.Result) 
                {

                    // First time to run 
                    // Trying to generate FILESTREAM tables
                    string FileStreamPath = configuration["Database:FileStreamPath"].ToString();

                    streamGenerator.ConnectionString =
                        streamGenerator.ConvertEFConnectionString(this.Database.GetConnectionString());

                    streamGenerator.Open();
                    streamGenerator.Generate(new Entities.Generals.FileStreamGeneratorArguments()
                    {
                        ColumnName = nameof(WFile.FileData),
                        FileGroupName = nameof(MySelfStudyDictionary2Db) + "FileStream",
                        TableName = nameof(Wfiles),
                        FileStreamPath = FileStreamPath,
                        DatabaseName = nameof(MySelfStudyDictionary2Db),
                        FolderName = "FS",
                        PrimarykeyName = nameof(WFile.Id)
                    });
                    streamGenerator.Close();

                   
                }


            }
        }
        #endregion

        #region DbSets

        public virtual DbSet<Folder> Folders { get; set; }

        public virtual DbSet<Log> Logs { get; set; }

        public virtual DbSet<Mean> Means { get; set; }

        public virtual DbSet<MemoTable> MemoTables { get; set; }

        public virtual DbSet<MyWord> MyWords { get; set; }

        public virtual DbSet<Opposite> Opposites { get; set; }

        public virtual DbSet<Synonym> Synonyms { get; set; }

        public virtual DbSet<TableViewInfo> TableViewInfos { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Thumbnail> Thumbnails { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<WDescription> Wdescriptions { get; set; }

        public virtual DbSet<WFile> Wfiles { get; set; }

        #endregion

    }
}
