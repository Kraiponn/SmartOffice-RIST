using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class EReservationContext : DbContext
    {
        public EReservationContext()
        {
        }

        public EReservationContext(DbContextOptions<EReservationContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<ReservationAttachFile> ReservationAttachFile { get; set; }
        public virtual DbSet<ReservationMasterLocation> ReservationMasterLocation { get; set; }
        public virtual DbSet<ReservationMasterRoom> ReservationMasterRoom { get; set; }
        public virtual DbSet<ReservationMasterRoomImage> ReservationMasterRoomImage { get; set; }
        public virtual DbSet<ReservationMasterRoomLayout> ReservationMasterRoomLayout { get; set; }
        public virtual DbSet<ReservationRoom> ReservationRoom { get; set; }
        public virtual DbSet<ReservationMasterAsset>   ReservationMasterAsset { get; set; }
        public virtual DbSet<ReservationMasterAssetImage> ReservationMasterAssetImage { get; set; }
        public virtual DbSet<ReservationMasterAssetLayout> ReservationMasterAssetLayout { get; set; }
        public virtual DbSet<ReservationAsset> ReservationAsset { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=rthsrv18;Initial Catalog=DocumentControlDemo;Persist Security Info=True;User ID=sa;Password=pwpolicy;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            

            modelBuilder.Entity<ReservationAttachFile>(entity =>
            {
                entity.HasKey(e => new { e.ReservationId, e.Type, e.Filename });

                entity.ToTable("Reservation_AttachFile");

                entity.Property(e => e.ReservationId).HasColumnName("Reservation_ID");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Filename).HasMaxLength(300);
            });

            modelBuilder.Entity<ReservationMasterLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("Reservation_MasterLocation");

                entity.Property(e => e.LocationId)
                    .HasColumnName("Location_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.Building)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationMasterRoom>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("Reservation_MasterRoom");

                entity.Property(e => e.RoomId)
                    .HasColumnName("Room_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasColumnName("Location_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ResponsibleBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleEmail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleTelNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TvconferenceIp)
                    .HasColumnName("TVConferenceIP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationMasterRoomImage>(entity =>
            {
                entity.HasKey(e => new { e.RoomId, e.DisplayOrder });

                entity.ToTable("Reservation_MasterRoomImage");

                entity.Property(e => e.RoomId)
                    .HasColumnName("Room_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationMasterRoomLayout>(entity =>
            {
                entity.HasKey(e => e.LayoutId);

                entity.ToTable("Reservation_MasterRoomLayout");

                entity.Property(e => e.LayoutId)
                    .HasColumnName("Layout_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .IsRequired()
                    .HasColumnName("Room_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationRoom>(entity =>
            {
                entity.HasKey(e => e.ReservationId);

                entity.ToTable("Reservation_Room");

                entity.Property(e => e.ReservationId).HasColumnName("Reservation_ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId)
                    .IsRequired()
                    .HasColumnName("Layout_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorOrganizationname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorPhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RoomId)
                    .IsRequired()
                    .HasColumnName("Room_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.TotalOperator).HasColumnName("Total_Operator");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OperatorBldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Bldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(300);
            });

            modelBuilder.Entity<ReservationMasterAsset>(entity =>
            {
                entity.HasKey(e => e.AssetId);

                entity.ToTable("Reservation_MasterAsset");

                entity.Property(e => e.AssetId)
                    .HasColumnName("Asset_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .IsRequired()
                    .HasColumnName("Location_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ResponsibleBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleEmail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleTelNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationMasterAssetImage>(entity =>
            {
                entity.HasKey(e => new { e.AssetId, e.DisplayOrder });

                entity.ToTable("Reservation_MasterAssetImage");

                entity.Property(e => e.AssetId)
                    .HasColumnName("Asset_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationMasterAssetLayout>(entity =>
            {
                entity.HasKey(e => e.LayoutId);

                entity.ToTable("Reservation_MasterAssetLayout");

                entity.Property(e => e.LayoutId)
                    .HasColumnName("Layout_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AssetId)
                    .IsRequired()
                    .HasColumnName("Asset_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReservationAsset>(entity =>
            {
                entity.HasKey(e => e.ReservationId);

                entity.ToTable("Reservation_Asset");

                entity.Property(e => e.ReservationId).HasColumnName("Reservation_ID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId)
                    .IsRequired()
                    .HasColumnName("Layout_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorOrganizationname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorPhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.AssetId)
                    .IsRequired()
                    .HasColumnName("Asset_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.OperatorBldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Bldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnID)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.Remarks).HasMaxLength(300);
            });
        }
    }
}
