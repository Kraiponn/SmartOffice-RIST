using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.SurveyApp.ViewModel;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class ManagementControlContext : DbContext
    {
        public ManagementControlContext()
        {
        }

        public ManagementControlContext(DbContextOptions<ManagementControlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InputItem> InputItem { get; set; }
        public virtual DbSet<InputItemList> InputItemList { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<JounalVoucherTempleate> JounalVoucherTempleate { get; set; }
        public virtual DbSet<JournalVoucher> JournalVoucher { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<SupplementItemCateg> SupplementItemCateg { get; set; }
        public virtual DbSet<SupplementItemDetail> SupplementItemDetail { get; set; }
        public virtual DbSet<ValueList> ValueList { get; set; }
        public DbQuery<VewSupplementItemDetail_CovidCheckList> VewSupplementItemDetail { get; set; }
        public DbQuery<VewFormInputHistoryBydate> VewFormInputHistoryBydate { get; set; }
        public DbQuery<VewFormInputHistory> VewFormInputHistory { get; set; }
        public DbQuery<VewMaster_Supplement_Item> VewMaster_Supplement_Item { get; set; }
        
        // Unable to generate entity type for table 'dbo.OperationMonthTrans'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Query<VewSupplementItemDetail_CovidCheckList>().ToView("vewSupplementItemListDetail")
          .Property(v => v.SupplementID).HasColumnName("SupplementID");


            modelBuilder.Query<VewFormInputHistoryBydate>().ToView("vewFormInputHistoryByDateKey")
          .Property(v => v.ZTCNo).HasColumnName("ZTCNo");


            modelBuilder.Query<VewFormInputHistory>().ToView("vewFormInputHistory")
        .Property(v => v.ZTCNo).HasColumnName("ZTCNo");

            modelBuilder.Query<VewMaster_Supplement_Item>().ToView("vewMaster_Supplement_Item")
          .Property(v => v.OperationCode).HasColumnName("OperationCode");

            modelBuilder.Entity<InputItem>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.Calculation)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DefaultValue).HasMaxLength(50);

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.Unit).HasMaxLength(10);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InputItemList>(entity =>
            {
                entity.HasKey(e => new { e.ItemCateg, e.ItemCode });

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.HasKey(e => e.ItemCateg);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.ItemCategName).HasMaxLength(50);

                entity.Property(e => e.Remarks1).HasMaxLength(50);

                entity.Property(e => e.Remarks2).HasMaxLength(50);

                entity.Property(e => e.Remarks3).HasMaxLength(50);

                entity.Property(e => e.Remarks4).HasMaxLength(50);

                entity.Property(e => e.Remarks5).HasMaxLength(50);

                entity.Property(e => e.RemarksColor1)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor2)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor3)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor4)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor5)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksTitle1).HasMaxLength(50);

                entity.Property(e => e.RemarksTitle2).HasMaxLength(50);

                entity.Property(e => e.RemarksTitle3).HasMaxLength(50);

                entity.Property(e => e.RemarksTitle4).HasMaxLength(50);

                entity.Property(e => e.RemarksTitle5).HasMaxLength(50);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");
            });

            modelBuilder.Entity<JounalVoucherTempleate>(entity =>
            {
                entity.HasKey(e => new { e.SectionCode, e.DisplayOrder });

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Resume)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubledgerType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierEmpCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierEmpName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuspenseAccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxArea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxClass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxEx)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JournalVoucher>(entity =>
            {
                entity.HasKey(e => new { e.Monthly, e.SectionCode, e.DisplayOrder });

                entity.Property(e => e.Monthly)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ExplanationRemark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Subledger)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubledgerType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxArea)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxClass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxEx)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.OperationCode);

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.InputKind)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName).HasMaxLength(50);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");
            });

            modelBuilder.Entity<SupplementItemCateg>(entity =>
            {
                entity.HasKey(e => new { e.OperationCode, e.Ztcno, e.Ztcname, e.ItemCateg });

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ztcno)
                    .HasColumnName("ZTCNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ztcname)
                    .HasColumnName("ZTCName")
                    .HasMaxLength(50);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");
            });

            modelBuilder.Entity<SupplementItemDetail>(entity =>
            {
                entity.HasKey(e => e.SupplementId)
                    .HasName("PK_SupplementItem");

                entity.Property(e => e.SupplementId).HasColumnName("SupplementID");

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.FinalResult).HasMaxLength(100);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.KeyTime).HasColumnType("datetime");

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName).HasMaxLength(50);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");

                entity.Property(e => e.Ztcname)
                    .HasColumnName("ZTCName")
                    .HasMaxLength(50);

                entity.Property(e => e.Ztcno)
                    .IsRequired()
                    .HasColumnName("ZTCNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ValueList>(entity =>
            {
                entity.HasKey(e => new { e.ValueCode, e.Value });

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()
");

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user
");
            });
        }
    }
}
