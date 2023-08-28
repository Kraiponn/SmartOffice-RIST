using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.Models;
using SmartOffice.ModelsForm;

namespace SmartOffice.ModelsDocControl
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
        public virtual DbSet<AppointmentAttachFile> AppointmentAttachFile { get; set; }
        public virtual DbSet<AppointmentOperator> AppointmentOperator { get; set; }
        public virtual DbSet<ApprovalFlow> ApprovalFlow { get; set; }
        public virtual DbSet<ApprovalItem> ApprovalItem { get; set; }
        public virtual DbSet<ApprovalResult> ApprovalResult { get; set; }
        public virtual DbSet<AttachFile> AttachFile { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentCcmail> DocumentCcmail { get; set; }
        public virtual DbSet<DocumentCondition> DocumentCondition { get; set; }
        public virtual DbSet<DocumentConditionHist> DocumentConditionHist { get; set; }
        public virtual DbSet<DocumentFlag> DocumentFlag { get; set; }
        public virtual DbSet<DocumentGroup> DocumentGroup { get; set; }
        public virtual DbSet<DocumentItem> DocumentItem { get; set; }
        public virtual DbSet<DocumentItemCateg> DocumentItemCateg { get; set; }
        public virtual DbSet<DocumentItemDetail> DocumentItemDetail { get; set; }
        public virtual DbSet<DocumentItemProgress> DocumentItemProgress { get; set; }
        public virtual DbSet<DocumentItemValueTableDetail> DocumentItemValueTableDetail { get; set; }
        public virtual DbSet<DocumentOpeGroupCateg> DocumentOpeGroupCateg { get; set; }
        public virtual DbSet<DocumentOperatorTransfer> DocumentOperatorTransfer { get; set; }
        public virtual DbSet<DocumentPermission> DocumentPermission { get; set; }
        public virtual DbSet<DocumentPermissionGroup> DocumentPermissionGroup { get; set; }
        public virtual DbSet<DocumentUploadPdfstamp> DocumentUploadPdfstamp { get; set; }
        public virtual DbSet<GenerateRunning> GenerateRunning { get; set; }
        public virtual DbSet<HitCounter> HitCounter { get; set; }
        public virtual DbSet<InputItem> InputItem { get; set; }
        public virtual DbSet<InputItemList> InputItemList { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<OperatorRole> OperatorRole { get; set; }
        public virtual DbSet<ReservationAttachFile> ReservationAttachFile { get; set; }
        public virtual DbSet<ReservationMasterLocation> ReservationMasterLocation { get; set; }
        public virtual DbSet<ReservationMasterRoom> ReservationMasterRoom { get; set; }
        public virtual DbSet<ReservationMasterRoomImage> ReservationMasterRoomImage { get; set; }
        public virtual DbSet<ReservationMasterRoomLayout> ReservationMasterRoomLayout { get; set; }
        public virtual DbSet<ReservationRoom> ReservationRoom { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleObject> RoleObject { get; set; }
        public virtual DbSet<RoleTransfer> RoleTransfer { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<SpecialRole> SpecialRole { get; set; }
        public virtual DbSet<SupplementItem> SupplementItem { get; set; }
        public virtual DbSet<SupplementItemCateg> SupplementItemCateg { get; set; }
        public virtual DbSet<SupplymentItemDetail> SupplymentItemDetail { get; set; }
        public virtual DbSet<SystemSettings> SystemSettings { get; set; }
        public virtual DbSet<ValueList> ValueList { get; set; }
        public virtual DbSet<ValueTable> ValueTable { get; set; }
        public DbSet<ModelItemList_Result> ModelItemList_Result { get; set; }
        public DbQuery<PendingDocModel> PendingDoc { get; set; }
        public DbSet<WaitApproveModel> ListWaitApprove { get; set; }
        public DbSet<VewSearchDocument> SearchDoc { get; set; }
        public DbQuery<VewSearchRoomData> SearchRoom { get; set; }
        public virtual DbSet<DocumentUploadPdfstamp> DocumentUploadPDFStamp { get; set; }
        public DbSet<ExportForm> ExportForm { get; set; }
        public virtual DbSet<DocumentQRCode> DocumentQRCode { get; set; }

        // Unable to generate entity type for table 'dbo.ApprovalFlowSpecial'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Query<PendingDocModel>().ToView("VewApprovePending")
           .Property(v => v.DocumentNo).HasColumnName("DocumentNo");

            modelBuilder.Entity<VewSearchDocument>()
                        .HasKey(c => new { c.DocumentNo });
            modelBuilder.Query<VewSearchRoomData>().ToView("vewSearchRoomData")
            .Property(v => v.Reservation_ID).HasColumnName("Reservation_ID");

            modelBuilder.Entity<ModelItemList_Result>()
                        .HasKey(c => new { c.DocumentCode, c.ItemCode, c.ItemCateg, c.DocumentNo });
            modelBuilder.Entity<WaitApproveModel>()
                        .HasKey(c => new { c.DocumentCode, c.DocumentNo, c.SeqNo, });

            modelBuilder.Entity<ExportForm>()
                        .HasKey(c => new { c.DocumentCode, c.DocumentNo });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.AppointType).HasMaxLength(20);

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

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppointmentAttachFile>(entity =>
            {
                entity.HasKey(e => new { e.AppointmentId, e.Filename });

                entity.ToTable("Appointment_AttachFile");

                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");

                entity.Property(e => e.Filename).HasMaxLength(300);
            });

            modelBuilder.Entity<AppointmentOperator>(entity =>
            {
                entity.HasKey(e => new { e.AppointmentId, e.OperatorId });

                entity.ToTable("Appointment_Operator");

                entity.Property(e => e.AppointmentId).HasColumnName("Appointment_ID");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApprovalFlow>(entity =>
            {
                entity.HasKey(e => new { e.FlowId, e.SeqNo, e.RoleId });

                entity.Property(e => e.FlowId)
                    .HasColumnName("FlowID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SeqNo).HasDefaultValueSql("('0')");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalItemCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AssignFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.AssignFlagRemark).HasMaxLength(500);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Requirement)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RequirementRemark).HasMaxLength(500);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reject);
                entity.Property(e => e.AssignFlagBySeq);


            });

            modelBuilder.Entity<ApprovalItem>(entity =>
            {
                entity.HasKey(e => e.ApprovalItemCode);

                entity.Property(e => e.ApprovalItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalItemNameE).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameJ).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameT).HasMaxLength(50);

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

            modelBuilder.Entity<ApprovalResult>(entity =>
            {
                entity.HasKey(e => new { e.DocumentNo, e.DocumentCode, e.SeqNo, e.RoleId, e.ApprovalItemCode, e.ApproverOperatorId });

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverOperatorId)
                    .HasColumnName("ApproverOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalItemNameE).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameJ).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameT).HasMaxLength(50);

                entity.Property(e => e.ApproverOperatorName).HasMaxLength(50);

                entity.Property(e => e.AssignFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNameE).HasMaxLength(250);

                entity.Property(e => e.DocumentNameJ).HasMaxLength(250);

                entity.Property(e => e.DocumentNameT).HasMaxLength(250);

                entity.Property(e => e.Judge).HasMaxLength(10);

                entity.Property(e => e.Requirement)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.RoleTransfer)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            
            });

            modelBuilder.Entity<AttachFile>(entity =>
            {
                entity.HasKey(e => e.No);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TotalPage).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocumentCode);

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.AttachedFile)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AttachedFile1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AttachedFile2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentGroupCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentKind)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNameE).HasMaxLength(250);

                entity.Property(e => e.DocumentNameJ).HasMaxLength(250);

                entity.Property(e => e.DocumentNameT).HasMaxLength(250);

                entity.Property(e => e.EmailSend)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCateg)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReviseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandardNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentCcmail>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.SeqNo, e.Mode });

                entity.ToTable("Document_CCMail");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SeqNo).HasDefaultValueSql("('0')");

                entity.Property(e => e.Mode)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ccmail1)
                    .HasColumnName("CCMail1")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ccmail2)
                    .HasColumnName("CCMail2")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentCondition>(entity =>
            {
                entity.ToTable("Document_Condition");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Design).HasMaxLength(300);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Template).HasMaxLength(300);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<DocumentConditionHist>(entity =>
            {
                entity.ToTable("Document_ConditionHist");

                entity.HasIndex(e => e.DocumentCode)
                    .HasName("IX_Document_ConditionHist");

                entity.HasIndex(e => e.DocumentNo)
                    .HasName("IX_Document_ConditionHist_1");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Design).HasMaxLength(300);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Template).HasMaxLength(300);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(400);             
            });

            modelBuilder.Entity<DocumentFlag>(entity =>
            {
                entity.HasKey(e => new { e.DocumentNo, e.UserId });

                entity.ToTable("Document_Flag");

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

            
            });

            modelBuilder.Entity<DocumentGroup>(entity =>
            {
                entity.HasKey(e => e.DocumentGroupCode);

                entity.Property(e => e.DocumentGroupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentGroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GroupCateg)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentItem>(entity =>
            {
                entity.HasKey(e => e.DocumentNo)
                    .HasName("PK_DocumentItem");

                entity.ToTable("Document_Item");

                entity.HasIndex(e => e.DocumentCode)
                    .HasName("IX_Document_Item");

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.AttachedFile)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.AttachedFile1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AttachedFile2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentGroupCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentKind)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNameE).HasMaxLength(250);

                entity.Property(e => e.DocumentNameJ).HasMaxLength(250);

                entity.Property(e => e.DocumentNameT).HasMaxLength(250);

                entity.Property(e => e.DocumentStatus).HasMaxLength(20);

                entity.Property(e => e.EmailSend)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HierarchyDocNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCateg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReqDescription1)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ReqDescription2).HasMaxLength(200);

                entity.Property(e => e.ReqDescription3).HasMaxLength(200);

                entity.Property(e => e.ReqOperatorId)
                    .HasColumnName("ReqOperatorID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReqOperatorName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReviseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StandardNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentItemCateg>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.ItemCateg });

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.ItemCateg, e.ItemCode, e.DocumentNo })
                    .HasName("PK_Document_Item_Detail2");

                entity.ToTable("Document_Item_Detail");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType).HasMaxLength(10);

                entity.Property(e => e.DefaultValue).HasMaxLength(50);

                entity.Property(e => e.DetailOption)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InputItemListItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InputItemOption)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputOption)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategNameE).HasMaxLength(300);

                entity.Property(e => e.ItemCategNameJ).HasMaxLength(300);

                entity.Property(e => e.ItemCategNameT).HasMaxLength(300);

                entity.Property(e => e.ItemNameE).HasMaxLength(200);

                entity.Property(e => e.ItemNameJ).HasMaxLength(200);

                entity.Property(e => e.ItemNameT).HasMaxLength(200);

                entity.Property(e => e.ReadOnly).HasDefaultValueSql("((0))");

                entity.Property(e => e.RemarksTitleE1).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE10).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE2).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE3).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE4).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE5).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE6).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE7).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE8).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleE9).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ1).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ10).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ2).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ3).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ4).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ5).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ6).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ7).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ8).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleJ9).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT1).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT10).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT2).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT3).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT4).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT5).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT6).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT7).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT8).HasMaxLength(300);

                entity.Property(e => e.RemarksTitleT9).HasMaxLength(300);

                entity.Property(e => e.Required).HasDefaultValueSql("((0))");

                entity.Property(e => e.Step).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Unit).HasMaxLength(10);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentItemProgress>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.DocumentNo, e.SeqNo, e.ApproverOperatorId })
                    .HasName("PK_DocumentProgress");

                entity.ToTable("Document_Item_Progress");

                entity.HasIndex(e => e.DocumentCode)
                    .HasName("IX_Document_Item_Progress");

                entity.HasIndex(e => e.DocumentNo)
                    .HasName("IX_Document_Item_Progress_1");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ApproverOperatorId)
                    .HasColumnName("ApproverOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovalItemCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovalItemNameE).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameJ).HasMaxLength(50);

                entity.Property(e => e.ApprovalItemNameT).HasMaxLength(50);

                entity.Property(e => e.ApproverOperatorName).HasMaxLength(50);

                entity.Property(e => e.ApproverOperatorSign).IsUnicode(false);

                entity.Property(e => e.AssignFlag)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.AssignFlagRemark).HasMaxLength(500);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNameE).HasMaxLength(250);

                entity.Property(e => e.DocumentNameJ).HasMaxLength(250);

                entity.Property(e => e.DocumentNameT).HasMaxLength(250);

                entity.Property(e => e.Requirement)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RequirementRemark).HasMaxLength(500);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);           
            });

            modelBuilder.Entity<DocumentItemValueTableDetail>(entity =>
            {
                entity.ToTable("Document_Item_ValueTable_Detail");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType).HasMaxLength(10);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.InputItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InputType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TableCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueE).HasMaxLength(200);

                entity.Property(e => e.ValueJ).HasMaxLength(200);

                entity.Property(e => e.ValueT).HasMaxLength(200);               
            });

            modelBuilder.Entity<DocumentOpeGroupCateg>(entity =>
            {
                entity.HasKey(e => e.OpeGroupCateg)
                    .HasName("PK_DocumentGroupCateg");

                entity.Property(e => e.OpeGroupCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [CurrentTimeStamp]    Script Date: 27/09/2019 09:28:05 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp
");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [HostName]    Script Date: 27/09/2019 09:28:05 ******/
CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()


");

                entity.Property(e => e.OpeGroupName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [CurrentTimeStamp]    Script Date: 27/09/2019 09:28:05 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp
");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [SystemUser]    Script Date: 27/09/2019 09:28:05 ******/
CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user


");
            });

            modelBuilder.Entity<DocumentOperatorTransfer>(entity =>
            {
                entity.HasKey(e => new { e.DocumentNo, e.TransferDate });

                entity.ToTable("Document_Operator_Transfer");

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorId)
                    .IsRequired()
                    .HasColumnName("OperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorIdnew)
                    .IsRequired()
                    .HasColumnName("OperatorIDNew")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorName).HasMaxLength(50);

                entity.Property(e => e.OperatorNameNew).HasMaxLength(50);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentPermission>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.PermissionGroupId, e.PermissionValue })
                    .HasName("PK_Document_Permission_1");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PermissionGroupId)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PermissionValue).HasMaxLength(100);
           

                entity.HasOne(d => d.PermissionGroup)
                    .WithMany(p => p.DocumentPermission)
                    .HasForeignKey(d => d.PermissionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentPermission_DocumentPermissionGroup");
            });

            modelBuilder.Entity<DocumentPermissionGroup>(entity =>
            {
                entity.HasKey(e => e.PermissionGroupId);

                entity.Property(e => e.PermissionGroupId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentUploadPdfstamp>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.PageTo })
                    .HasName("PK_DocumentUploadPDFStamp_1");

                entity.ToTable("DocumentUploadPDFStamp");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Stamp)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DocumentQRCode>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.CreateNo })
                    .HasName("PK_DocumentQRCode_1");

                entity.ToTable("DocumentQRCode");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreateNo);
                entity.Property(e => e.Graphic);
                entity.Property(e => e.Width);
                entity.Property(e => e.Height);
                entity.Property(e => e.Position1);
                entity.Property(e => e.Position2);
                entity.Property(e => e.Page);
                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GenerateRunning>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.Year, e.Month, e.Running, e.DocStatus });

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HitCounter>(entity =>
            {
                entity.HasKey(e => e.Slid)
                    .HasName("PK_Hit");

                entity.Property(e => e.Slid).HasColumnName("SLID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InputItem>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DefaultValue).HasMaxLength(50);

                entity.Property(e => e.DetailOption)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNameE).HasMaxLength(200);

                entity.Property(e => e.ItemNameJ).HasMaxLength(200);

                entity.Property(e => e.ItemNameT).HasMaxLength(200);

                entity.Property(e => e.Step).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.Unit).HasMaxLength(10);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InputItemList>(entity =>
            {
                entity.HasKey(e => new { e.ItemCateg, e.ItemCode });

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

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

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.ItemCateg);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategNameE)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategNameJ)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategNameT)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE1)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE10)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE2)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE3)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE4)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE5)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE6)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE7)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE8)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleE9)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ1)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ10)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ2)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ3)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ4)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ5)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ6)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ7)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ8)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleJ9)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT1)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT10)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT2)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT3)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT4)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT5)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT6)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT7)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT8)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RemarksTitleT9)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.Code });

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value2).HasMaxLength(100);

                entity.Property(e => e.Value3).HasMaxLength(100);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.Property(e => e.FromUserId).HasMaxLength(128);
            });

            modelBuilder.Entity<OperatorRole>(entity =>
            {
                entity.HasKey(e => new { e.OperatorId, e.RoleId });

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorDetail).HasMaxLength(500);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
              
            });

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
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
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

                entity.Property(e => e.Computer).HasDefaultValueSql("((0))");

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

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Projector).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResponsibleBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsibleEmail)
                    .IsRequired()
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

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproveId)
                    .HasColumnName("ApproveID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LayoutId)
                    .IsRequired()
                    .HasColumnName("Layout_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorBldg)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorEmail)
                    .HasMaxLength(50)
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

                entity.Property(e => e.Remarks).HasMaxLength(300);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RoomId)
                    .IsRequired()
                    .HasColumnName("Room_ID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TotalOperator).HasColumnName("Total_Operator");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).HasMaxLength(300);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RoleObject>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.DisabledObject })
                    .HasName("PK_RoleObject_1");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DisabledObject)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

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

            modelBuilder.Entity<RoleTransfer>(entity =>
            {
                entity.HasKey(e => new { e.FromOperatorId, e.ToOperatorId, e.StartDate });

                entity.Property(e => e.FromOperatorId)
                    .HasColumnName("FromOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ToOperatorId)
                    .HasColumnName("ToOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.Property(e => e.UserAccountId)
                    .IsRequired()
                    .HasColumnName("UserAccount_Id")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<SpecialRole>(entity =>
            {
                entity.HasKey(e => e.Department);

                entity.Property(e => e.Department)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.FromRole)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ReplaceRole)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplementItem>(entity =>
            {
                entity.HasKey(e => e.SupplementId);

                entity.HasIndex(e => new { e.SupplementCateg, e.MachineNo, e.OpeGroupCode, e.Ztcno, e.ZtcnoSuffix, e.KeyTime, e.OperationCode, e.StartTime })
                    .HasName("IX_SupplementItem")
                    .IsUnique();

                entity.Property(e => e.SupplementId)
                    .HasColumnName("SupplementID")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndOperatorId)
                    .HasColumnName("EndOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndOperatorName).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.FinalResult)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.KeyTime).HasColumnType("datetime");

                entity.Property(e => e.MachineNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName).HasMaxLength(50);

                entity.Property(e => e.StartOperatorId)
                    .HasColumnName("StartOperatorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartOperatorName).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.SupplementCateg)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ztcno)
                    .HasColumnName("ZTCNo")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ZtcnoSuffix)
                    .HasColumnName("ZTCNoSuffix")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplementItemCateg>(entity =>
            {
                entity.HasKey(e => new { e.DocumentCode, e.ItemCateg })
                    .HasName("PK_SupplementItemCateg_1");

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplymentItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.DocumentNo, e.DocumentCode, e.SeqNo });

                entity.Property(e => e.DocumentNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Item1).HasMaxLength(200);

                entity.Property(e => e.Item10).HasMaxLength(200);

                entity.Property(e => e.Item2).HasMaxLength(200);

                entity.Property(e => e.Item3).HasMaxLength(200);

                entity.Property(e => e.Item4).HasMaxLength(200);

                entity.Property(e => e.Item5).HasMaxLength(200);

                entity.Property(e => e.Item6).HasMaxLength(200);

                entity.Property(e => e.Item7).HasMaxLength(200);

                entity.Property(e => e.Item8).HasMaxLength(200);

                entity.Property(e => e.Item9).HasMaxLength(200);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemSettings>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.Code });

                entity.Property(e => e.GroupCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value2).HasMaxLength(100);
            });

            modelBuilder.Entity<ValueList>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValueE).HasMaxLength(300);

                entity.Property(e => e.ValueJ).HasMaxLength(300);

                entity.Property(e => e.ValueT).HasMaxLength(300);
            });

            modelBuilder.Entity<ValueTable>(entity =>
            {
                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.InputType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TableCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValueE).HasMaxLength(100);

                entity.Property(e => e.ValueJ).HasMaxLength(100);

                entity.Property(e => e.ValueT).HasMaxLength(100);
            });
        }
    }
}
