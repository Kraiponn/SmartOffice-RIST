using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartOffice.ModelsHRMSLocal
{
    public partial class HRMSLocalContext : DbContext
    {
        public HRMSLocalContext()
        {
        }

        public HRMSLocalContext(DbContextOptions<HRMSLocalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HrmsEmployee> HrmsEmployee { get; set; }
        public virtual DbSet<InternalTelephone> InternalTelephone { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<HrmsEmployee>(entity =>
            {
                entity.HasKey(e => e.Codempid);

                entity.ToTable("HRMS_Employee");

                entity.Property(e => e.Codempid)
                    .HasColumnName("CODEMPID")
                    .HasMaxLength(8)
                    .ValueGeneratedNever();

                entity.Property(e => e.Age)
                    .HasColumnName("AGE")
                    .HasMaxLength(4000);

                entity.Property(e => e.Company)
                    .HasColumnName("COMPANY")
                    .HasMaxLength(4000);

                entity.Property(e => e.Department)
                    .HasColumnName("DEPARTMENT")
                    .HasMaxLength(4000);

                entity.Property(e => e.Department2)
                    .HasColumnName("DEPARTMENT2")
                    .HasMaxLength(4000);

                entity.Property(e => e.Department3)
                    .HasColumnName("DEPARTMENT3")
                    .HasMaxLength(4000);

                entity.Property(e => e.Division)
                    .HasColumnName("DIVISION")
                    .HasMaxLength(4000);

                entity.Property(e => e.Dtebirth).HasColumnName("DTEBIRTH");

                entity.Property(e => e.Email1)
                    .HasColumnName("EMAIL1")
                    .HasMaxLength(50);

                entity.Property(e => e.Email2)
                    .HasColumnName("EMAIL2")
                    .HasMaxLength(50);

                entity.Property(e => e.Emplevel).HasColumnName("EMPLEVEL");

                entity.Property(e => e.Inactive)
                    .HasColumnName("INACTIVE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Namempe)
                    .HasColumnName("NAMEMPE")
                    .HasMaxLength(60);

                entity.Property(e => e.Namempt)
                    .HasColumnName("NAMEMPT")
                    .HasMaxLength(60);

                entity.Property(e => e.Nationality)
                    .HasColumnName("NATIONALITY")
                    .HasMaxLength(4000);

                entity.Property(e => e.Organizationname)
                    .HasColumnName("ORGANIZATIONNAME")
                    .HasMaxLength(4000);

                entity.Property(e => e.Passport)
                    .HasColumnName("PASSPORT")
                    .HasMaxLength(20);

                entity.Property(e => e.Position)
                    .HasColumnName("POSITION")
                    .HasMaxLength(4000);

                entity.Property(e => e.Section)
                    .HasColumnName("SECTION")
                    .HasMaxLength(4000);

                entity.Property(e => e.WorkingDate)
                    .HasColumnName("WORKING_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Codcalen)
                    .HasColumnName("CODCALEN")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<InternalTelephone>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.Property(e => e.FaxNo).HasMaxLength(100);

                entity.Property(e => e.Group1).HasMaxLength(300);

                entity.Property(e => e.Group2).HasMaxLength(300);

                entity.Property(e => e.Group3).HasMaxLength(300);

                entity.Property(e => e.Id).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.TelNo).HasMaxLength(100);

                entity.Property(e => e.Title1).HasMaxLength(300);

                entity.Property(e => e.Title2).HasMaxLength(300);
            });
        }
    }
}
