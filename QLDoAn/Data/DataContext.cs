using Azure;
using Microsoft.EntityFrameworkCore;
using QLDoAn.Models;
using quanlidoan.Data;

namespace quanlidoan.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {

        }
        #region DbSet
        public DbSet<student> student { get; set; }
        public DbSet<teacher> teacher { get; set; }

        public DbSet<Topic> Topic { get; set; }
        public DbSet<topicType> topicType { get; set; }
        public DbSet<account> account { get; set; }
     
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasOne(h => h.student)
                .WithMany()
                .HasForeignKey(h => h.StudentID);

            modelBuilder.Entity<Topic>()
                .HasOne(h => h.teacher)
                .WithMany()
                .HasForeignKey(h => h.TeacherID);


            modelBuilder.Entity<Topic>()
               .HasOne(h => h.topicType)
               .WithMany()
               .HasForeignKey(h => h.TopicTypeID);


            modelBuilder.Entity<student>()
                .HasOne(h => h.account)
                .WithMany()
                .HasForeignKey(h => h.AccountID);


            modelBuilder.Entity<teacher>()
                .HasOne(h => h.account)
                .WithMany()
                .HasForeignKey(h => h.AccountID);
        }


    }
}

