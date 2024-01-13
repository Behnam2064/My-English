using ME.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.DataSource.Contexts
{
    public class MySelfStudyDictionary2Db : DbContext
    {

        #region Fields

        private readonly DbContextOptions options;

        private static bool IsEnsureCreated;

        #endregion

        #region Constructors
        public MySelfStudyDictionary2Db(DbContextOptions options) : base(options)
        {
            this.options = options;

            if (!IsEnsureCreated)
            {
                IsEnsureCreated = true;
                this.Database.EnsureCreatedAsync().Wait();
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

        public virtual DbSet<Wdescription> Wdescriptions { get; set; }

        public virtual DbSet<Wfile> Wfiles { get; set; }

        #endregion

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
    }
}
