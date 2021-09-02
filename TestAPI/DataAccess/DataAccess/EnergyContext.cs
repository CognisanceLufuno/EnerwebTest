using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Lufuno.DataAccess
{
    public class EnergyContext : DbContext
    {
        public EnergyContext() : base()
        {

        }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<Record> Records { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Record>()
        //     .HasRequired(s => s.Upload)
        //     .WithMany(g => g.Records)
        //     .HasForeignKey<int>(s => s.FileId);
        //}
    }
}
