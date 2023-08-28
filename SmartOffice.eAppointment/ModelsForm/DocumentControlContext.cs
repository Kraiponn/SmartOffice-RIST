using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.eAppointment.ModelsForm;

namespace SmartOffice.EAppointment.ModelsForm
{
    public partial class DocumentControlContext : DbContext
    {
        public DocumentControlContext()
        {
        }

        public DocumentControlContext(DbContextOptions<DocumentControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<AppointmentOperator> AppointmentOperator { get; set; }
        public DbQuery<VewOperatorAppointment> VewOperatorAppointment { get; set; }
        public virtual DbSet<AppointmentAttachFile> AppointmentAttachFile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.Query<VewOperatorAppointment>().ToView("vewOperatorAppointment")
           .Property(v => v.AppointmentId).HasColumnName("Appointment_ID");
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                

                entity.Property(e => e.StartDate).HasColumnType("datetime");

               

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.ThemeColor).HasMaxLength(100);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");
                entity.Property(e => e.AppointType).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppointmentOperator>(entity =>
            {
                entity.HasKey(e => new { e.AppointmentId, e.OperatorId })
                    .HasName("PK_Reservation_Room_Operator");

                entity.ToTable("Appointment_Operator");

                entity.Property(e => e.AppointmentId)
                    .HasColumnName("Appointment_ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Attend).HasDefaultValueSql("((1))");

                
            });

            modelBuilder.Entity<AppointmentAttachFile>(entity =>
            {
                entity.HasKey(e => new { e.AppointmentId, e.Filename });

                entity.ToTable("Appointment_AttachFile");

                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");

                entity.Property(e => e.Filename).HasMaxLength(300);
            });


        }
    }
}
