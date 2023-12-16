using System;
using System.Collections.Generic;
using ME.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace ME.DataSource.Contexts;

public partial class MySelfStudyDictionary1Context : DbContext
{
    public MySelfStudyDictionary1Context() 
    {
    }

    public MySelfStudyDictionary1Context(DbContextOptions<MySelfStudyDictionary1Context> options)
        : base(options)
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-N90T10T;User Id=devuser;Password=123456;Database=MySelfStudyDictionary1;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>(entity =>
        {
            entity.ToTable("Folder");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddDateTime)
                .HasColumnType("datetime")
                .HasColumnName("addDateTime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.MyIconId)
                .HasComment("This is column related to Thumbnail table (icon save at that table)")
                .HasColumnName("myIconId");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.TagId).HasColumnName("tagId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Folder_User");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedTime)
                .HasColumnType("datetime")
                .HasColumnName("addedTime");
            entity.Property(e => e.ClassName)
                .HasMaxLength(128)
                .HasColumnName("className");
            entity.Property(e => e.ExceptionName)
                .HasMaxLength(128)
                .HasColumnName("exceptionName");
            entity.Property(e => e.Info).HasColumnName("info");
            entity.Property(e => e.LineNumber).HasColumnName("lineNumber");
            entity.Property(e => e.LogScope)
                .HasComment("like user or developer log")
                .HasColumnName("logScope");
            entity.Property(e => e.LogType)
                .HasComment("like error, information, warning")
                .HasColumnName("logType");
            entity.Property(e => e.MethodName)
                .HasMaxLength(128)
                .HasColumnName("methodName");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Log_User");
        });

        modelBuilder.Entity<Mean>(entity =>
        {
            entity.ToTable("Mean");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MyWordId).HasColumnName("myWordId");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.MyWord).WithMany(p => p.Means)
                .HasForeignKey(d => d.MyWordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mean_MyWord");
        });

        modelBuilder.Entity<MemoTable>(entity =>
        {
            entity.ToTable("MemoTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddDateTime)
                .HasColumnType("datetime")
                .HasColumnName("addDateTime");
            entity.Property(e => e.CountSelected).HasColumnName("countSelected");
            entity.Property(e => e.Section).HasColumnName("section");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.UserId)
                .HasComment("This is memo which user create that (Can suggestions to owner user or all other)")
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.MemoTables)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemoTable_User");
        });

        modelBuilder.Entity<MyWord>(entity =>
        {
            entity.ToTable("MyWord");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddDateTime)
                .HasColumnType("datetime")
                .HasColumnName("addDateTime");
            entity.Property(e => e.Dictation)
                .HasMaxLength(1000)
                .HasColumnName("dictation");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ModifiedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("modifiedDateTime");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.WordType).HasColumnName("wordType");

            entity.HasOne(d => d.Folder).WithMany(p => p.MyWords)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("FK_MyWord_Folder");

            entity.HasOne(d => d.User).WithMany(p => p.MyWords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MyWord_User");
        });

        modelBuilder.Entity<Opposite>(entity =>
        {
            entity.ToTable("Opposite");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MyWordId).HasColumnName("myWordId");
            entity.Property(e => e.MyWordId1).HasColumnName("myWordId1");

            entity.HasOne(d => d.MyWord).WithMany(p => p.OppositeMyWords)
                .HasForeignKey(d => d.MyWordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Opposite_MyWord1");

            entity.HasOne(d => d.MyWordId1Navigation).WithMany(p => p.OppositeMyWordId1Navigations)
                .HasForeignKey(d => d.MyWordId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Opposite_MyWord2");
        });

        modelBuilder.Entity<Synonym>(entity =>
        {
            entity.ToTable("Synonym");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MyWordId).HasColumnName("myWordId");
            entity.Property(e => e.MyWordId1).HasColumnName("myWordId1");

            entity.HasOne(d => d.MyWord).WithMany(p => p.SynonymMyWords)
                .HasForeignKey(d => d.MyWordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Synonym_MyWord1");

            entity.HasOne(d => d.MyWordId1Navigation).WithMany(p => p.SynonymMyWordId1Navigations)
                .HasForeignKey(d => d.MyWordId1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Synonym_MyWord2");
        });

        modelBuilder.Entity<TableViewInfo>(entity =>
        {
            entity.ToTable("TableViewInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TableId).HasColumnName("tableId");
            entity.Property(e => e.TableName)
                .HasMaxLength(128)
                .HasColumnName("tableName");
            entity.Property(e => e.TimeEndView)
                .HasColumnType("datetime")
                .HasColumnName("timeEndView");
            entity.Property(e => e.TimeView)
                .HasColumnType("datetime")
                .HasColumnName("timeView");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.TableViewInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableViewInfo_User");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Thumbnail>(entity =>
        {
            entity.ToTable("Thumbnail");

            entity.HasIndex(e => e.Id, "UQ__Thumbnai__3213E83E0D7200B5").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.FileData).HasColumnName("fileData");
            entity.Property(e => e.FileStreamRowId)
                .HasComment("like My image.jpg file id in Wfile Table")
                .HasColumnName("fileStreamRowId");
            entity.Property(e => e.RowId)
                .HasComment("like a folder id")
                .HasColumnName("rowId");
            entity.Property(e => e.TableId)
                .HasComment("like WFile id")
                .HasColumnName("tableId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Users");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.Mobile)
                .HasMaxLength(13)
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Wdescription>(entity =>
        {
            entity.ToTable("WDescription");

            entity.HasIndex(e => e.Id, "UQ__WDescrip__3213E83E1B17EA9B").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MyWordId).HasColumnName("myWordId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.MyWord).WithMany(p => p.Wdescriptions)
                .HasForeignKey(d => d.MyWordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WDescription_MyWord");

            entity.HasOne(d => d.User).WithMany(p => p.Wdescriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WDescription_User");
        });

        modelBuilder.Entity<Wfile>(entity =>
        {
            entity.ToTable("WFile");

            entity.HasIndex(e => e.Id, "UQ__WFile__3213E83E1318392D").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AddedTime)
                .HasColumnType("datetime")
                .HasColumnName("addedTime");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.FileData).HasColumnName("fileData");
            entity.Property(e => e.FileName)
                .HasMaxLength(128)
                .HasColumnName("fileName");
            entity.Property(e => e.FileType)
                .HasMaxLength(5)
                .HasColumnName("fileType");
            entity.Property(e => e.FolderId).HasColumnName("folderId");
            entity.Property(e => e.ModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("modifiedTime");
            entity.Property(e => e.MyWordId).HasColumnName("myWordId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.MyWord).WithMany(p => p.Wfiles)
                .HasForeignKey(d => d.MyWordId)
                .HasConstraintName("FK_WFile_MyWord");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
