using ME.Entities.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Interfaces.Services
{
    public interface IDataBaseContext
    {
        public DbSet<Folder> Folders { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Mean> Means { get; set; }

        public DbSet<MemoTable> MemoTables { get; set; }

        public DbSet<MyWord> MyWords { get; set; }

        public DbSet<Opposite> Opposites { get; set; }

        public DbSet<Synonym> Synonyms { get; set; }

        public DbSet<TableViewInfo> TableViewInfos { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Thumbnail> Thumbnails { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WDescription> Wdescriptions { get; set; }

        public DbSet<Wfile> Wfiles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeRole> EmployeeRoles { get; set; }

    }
}
