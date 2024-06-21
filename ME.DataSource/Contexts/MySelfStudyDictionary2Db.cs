using ME.Entities.Database;
using ME.Entities.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.DataSource.Contexts
{
    public class MySelfStudyDictionary2Db : DbContext , IDataBaseContext
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


        /*
         *MySql
                protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    //base.OnConfiguring(optionsBuilder);
                    ConnectionString = config[nameof(DatabaseTCD) + ":" + "ConnectionString"];
                    optionsBuilder.UseMySQL(ConnectionString);
                }


         */


/*
        SQL Server
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

*/

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

        public DbSet<Wfile> Wfiles { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        #endregion
    }
}
