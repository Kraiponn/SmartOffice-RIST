using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.Models;
using SmartOffice.ModelsForm;

namespace SmartOffice.ModelsEsmartOffice
{
    public partial class ESmartOfficeContext : DbContext
    {
        public ESmartOfficeContext()
        {
        }

        public ESmartOfficeContext(DbContextOptions<ESmartOfficeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetGroup> AspNetGroup { get; set; }
        public virtual DbSet<AspNetGroupMenu> AspNetGroupMenu { get; set; }
        public virtual DbSet<AspNetMenu> AspNetMenu { get; set; }
        public virtual DbSet<AspNetMenuControl> AspNetMenuControl { get; set; }
        public virtual DbSet<AspNetMenuRoles> AspNetMenuRoles { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<ConfirmPasswordReset> ConfirmPasswordReset { get; set; }
        public virtual DbSet<ControlConfig> ControlConfig { get; set; }
        public virtual DbSet<ControlPart> ControlPart { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<GenerateNumber> GenerateNumber { get; set; }
        public virtual DbSet<ImgHeader> ImgHeader { get; set; }
        public virtual DbSet<ImgTextHeader> ImgTextHeader { get; set; }
        public virtual DbSet<NnewsDetail> NnewsDetail { get; set; }
        public virtual DbSet<NnewsHeader> NnewsHeader { get; set; }
        public DbSet<FormMenu> FormMenu { get; set; }
        public DbSet<MenuCMMHeader> MenuCMMHeader { get; set; }
        public DbSet<MenuCMMFooter> menuCMMFooter { get; set; }
        public DbSet<SubSystems> subSystems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<FormMenu>()
                        .HasKey(c => new { c.GroupCateg, c.MenuIdentity });
            modelBuilder.Entity<MenuCMMHeader>()
                        .HasKey(c => new { c.GroupCateg, c.DisplayOrder });
            modelBuilder.Entity<MenuCMMFooter>()
                        .HasKey(c => new { c.GroupCateg, c.DisplayOrder });
            modelBuilder.Entity<SubSystems>()
                        .HasKey(c => new { c.GroupCateg, c.GroupSub, c.MenuIdentity, c.DisplayOrder });

            modelBuilder.Entity<AspNetGroup>(entity =>
            {
                entity.HasKey(e => e.GroupCateg);

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.DivisionCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ImagePath).HasMaxLength(200);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AspNetGroupMenu>(entity =>
            {
                entity.HasKey(e => e.GroupMenuId)
                    .HasName("PK_AspNetGroupMenu_1");

                entity.Property(e => e.GroupMenuId)
                    .HasColumnName("GroupMenuID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Badges).HasMaxLength(100);

                entity.Property(e => e.BadgesName).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.GroupCateg)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GroupMenuName).HasMaxLength(100);

                entity.Property(e => e.IconClass).HasMaxLength(100);

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupCategNavigation)
                    .WithMany(p => p.AspNetGroupMenu)
                    .HasForeignKey(d => d.GroupCateg)
                    .HasConstraintName("FK_AspNetGroupMenu_AspNetGroup_DocumentGroupCateg");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.AspNetGroupMenu)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetMenu>(entity =>
            {
                entity.HasKey(e => e.MenuIdentity);

                entity.Property(e => e.MenuIdentity).ValueGeneratedNever();

                entity.Property(e => e.Action)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Badges)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BadgesName)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Controller)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.IconClass)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MenuNameE).HasMaxLength(250);

                entity.Property(e => e.MenuNameJ).HasMaxLength(250);

                entity.Property(e => e.MenuNameT).HasMaxLength(250);

                entity.Property(e => e.MenuUrl)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Param)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AspNetMenuControl>(entity =>
            {
                entity.HasKey(e => new { e.MenuIdentity, e.MenuIdentityParent, e.DisplayOrder, e.GroupMenuId });

                entity.Property(e => e.GroupMenuId).HasColumnName("GroupMenuID");

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupMenu)
                    .WithMany(p => p.AspNetMenuControl)
                    .HasForeignKey(d => d.GroupMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MenuIdentityNavigation)
                    .WithMany(p => p.AspNetMenuControlMenuIdentityNavigation)
                    .HasForeignKey(d => d.MenuIdentity)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MenuIdentityParentNavigation)
                    .WithMany(p => p.AspNetMenuControlMenuIdentityParentNavigation)
                    .HasForeignKey(d => d.MenuIdentityParent)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetMenuRoles>(entity =>
            {
                entity.HasKey(e => new { e.MenuIdentity, e.RoleId });

                entity.Property(e => e.RoleId).HasMaxLength(200);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MenuIdentityNavigation)
                    .WithMany(p => p.AspNetMenuRoles)
                    .HasForeignKey(d => d.MenuIdentity)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetMenuRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.UserId).HasMaxLength(200);

                entity.Property(e => e.RoleId).HasMaxLength(200);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.UserId).HasMaxLength(200);

                entity.Property(e => e.LoginProvider).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id)
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GroupCateg)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.OperatorSign).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.HasOne(d => d.GroupCategNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.GroupCateg)
                    .HasConstraintName("FK_AspNetUsers_AspNetGroup_DocumentGroupCateg");

                entity.Property(e => e.Bldg).HasMaxLength(2);

                entity.Property(e => e.LastChangePassword)
                    .HasColumnType("datetime");
                entity.Property(e => e.LastLogin)
                    .HasColumnType("datetime");
                entity.Property(e => e.LastLogout)
                    .HasColumnType("datetime");
                    
            });

            modelBuilder.Entity<ConfirmPasswordReset>(entity =>
            {
                entity.ToTable("ConfirmPassword_Reset");

                entity.Property(e => e.ActiveStatus).HasColumnName("active_status");

                entity.Property(e => e.ComputerName)
                    .HasColumnName("computer_name")
                    .HasMaxLength(50);

                entity.Property(e => e.ConfirmId)
                    .HasColumnName("confirm_id")
                    .HasMaxLength(50);

                entity.Property(e => e.ConfrimDate)
                    .HasColumnName("confrim_date")
                    .HasColumnType("date");

                entity.Property(e => e.RequestDate)
                    .HasColumnName("request_date")
                    .HasColumnType("date");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });
            

            modelBuilder.Entity<ControlConfig>(entity =>
            {
                entity.HasKey(e => new { e.PartId, e.GroupCateg, e.ConfigOrder });

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BgD).HasMaxLength(100);

                entity.Property(e => e.BgH).HasMaxLength(100);

                entity.Property(e => e.ColorButton).HasMaxLength(100);

                entity.Property(e => e.ColorTextH).HasMaxLength(100);

                entity.Property(e => e.CorlorTextD).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TextH).HasMaxLength(100);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupCategNavigation)
                    .WithMany(p => p.ControlConfig)
                    .HasForeignKey(d => d.GroupCateg)
                    .HasConstraintName("FK_ControlConfig_AspNetGroup_DocumentGroupCateg");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.ControlConfig)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ControlPart>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK_AspNetPart");

                entity.Property(e => e.PartId)
                    .HasColumnName("PartID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Controller).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.PartName).HasMaxLength(100);

                entity.Property(e => e.PathUrl).HasMaxLength(100);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.HasKey(e => e.DivisionCode);

                entity.Property(e => e.DivisionCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DivisionName).HasMaxLength(50);
            });
         

            modelBuilder.Entity<GenerateNumber>(entity =>
            {
                entity.HasKey(e => new { e.Year, e.Month, e.TypeId });

                entity.Property(e => e.TypeId)
                    .HasColumnName("Type_id")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ImgHeader>(entity =>
            {
                entity.HasKey(e => new { e.PartId, e.GroupCateg, e.ImgOrder });

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ImgPath).HasMaxLength(100);

                entity.Property(e => e.ImgType).HasMaxLength(100);

                entity.Property(e => e.Link).HasDefaultValueSql("((0))");

                entity.Property(e => e.LinkName).HasMaxLength(100);

                entity.Property(e => e.LinkPath).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupCategNavigation)
                    .WithMany(p => p.ImgHeader)
                    .HasForeignKey(d => d.GroupCateg)
                    .HasConstraintName("FK_ImgHeader_AspNetGroup_DocumentGroupCateg");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.ImgHeader)
                    .HasForeignKey(d => d.PartId);
            });

            modelBuilder.Entity<ImgTextHeader>(entity =>
            {
                entity.HasKey(e => new { e.PartId, e.GroupCateg, e.ImgOrder, e.TextHorder });

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TextHorder).HasColumnName("TextHOrder");

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Link).HasDefaultValueSql("((0))");

                entity.Property(e => e.LinkPath).HasMaxLength(100);

                entity.Property(e => e.TextD).HasMaxLength(300);

                entity.Property(e => e.TextH)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ImgHeader)
                    .WithMany(p => p.ImgTextHeader)
                    .HasForeignKey(d => new { d.PartId, d.GroupCateg, d.ImgOrder })
                    .HasConstraintName("FK_ImgTextHeader_ImgHeader");
            });

            modelBuilder.Entity<NnewsDetail>(entity =>
            {
                entity.HasKey(e => new { e.PartId, e.GroupCateg, e.NewHorder, e.NewDorder })
                    .HasName("PK_NewsDetail");

                entity.ToTable("NNewsDetail");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NewHorder).HasColumnName("NewHOrder");

                entity.Property(e => e.NewDorder).HasColumnName("NewDOrder");

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ItemType).HasMaxLength(100);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Value).HasMaxLength(400);

                entity.Property(e => e.Value1).HasMaxLength(400);

                entity.HasOne(d => d.NnewsHeader)
                    .WithMany(p => p.NnewsDetail)
                    .HasForeignKey(d => new { d.PartId, d.GroupCateg, d.NewHorder })
                    .HasConstraintName("FK_NewsDetail_NewsHeader");
            });

            modelBuilder.Entity<NnewsHeader>(entity =>
            {
                entity.HasKey(e => new { e.PartId, e.GroupCateg, e.NewHorder })
                    .HasName("PK_NewsHeader");

                entity.ToTable("NNewsHeader");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.GroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NewHorder).HasColumnName("NewHOrder");

                entity.Property(e => e.Badges).HasMaxLength(100);

                entity.Property(e => e.BadgesName).HasMaxLength(100);

                entity.Property(e => e.CreateBy).HasMaxLength(8);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Disable).HasDefaultValueSql("((1))");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.IconClass).HasMaxLength(100);

                entity.Property(e => e.ImgPath).HasMaxLength(100);

                entity.Property(e => e.LinkPath).HasMaxLength(100);

                entity.Property(e => e.NewsType).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title1).HasMaxLength(400);

                entity.Property(e => e.UpdateBy).HasMaxLength(8);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.GroupCategNavigation)
                    .WithMany(p => p.NnewsHeader)
                    .HasForeignKey(d => d.GroupCateg)
                    .HasConstraintName("FK_NewsHeader_AspNetGroup_DocumentGroupCateg");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.NnewsHeader)
                    .HasForeignKey(d => d.PartId)
                    .HasConstraintName("FK_NewsHeader_ControlPart_PartID");
            });
        }
    }
}
