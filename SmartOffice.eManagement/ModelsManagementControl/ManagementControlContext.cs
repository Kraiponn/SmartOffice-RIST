using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.eManagement.Models;

namespace SmartOffice.eManagement.ModelsManagementControl
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
        public virtual DbSet<InputItemListMessage> InputItemListMessage { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }
        public virtual DbSet<JounalVoucherTempleate> JounalVoucherTempleate { get; set; }
        public virtual DbSet<JournalVoucher> JournalVoucher { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<OperationCondition> OperationCondition { get; set; }
        public virtual DbSet<OperationConditionHist> OperationConditionHist { get; set; }
        public virtual DbSet<OperationGroup> OperationGroup { get; set; }
        public virtual DbSet<OperationItem> OperationItem { get; set; }
        public virtual DbSet<OperationItemCateg> OperationItemCateg { get; set; }
        public virtual DbSet<OperationItemDetail> OperationItemDetail { get; set; }
        public virtual DbSet<OperationItemValueTableDetail> OperationItemValueTableDetail { get; set; }
        public virtual DbSet<OperationMachine2> OperationMachine2 { get; set; }
        public virtual DbSet<OperationMachineReport2> OperationMachineReport2 { get; set; }
        public virtual DbSet<OperatorRole> OperatorRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SupplementItemCateg> SupplementItemCateg { get; set; }
        public virtual DbSet<SupplementItemDetail> SupplementItemDetail { get; set; }
        public virtual DbSet<SurveyPicsickLeave> SurveyPicsickLeave { get; set; }
        public virtual DbSet<ValueList> ValueList { get; set; }
        public virtual DbSet<ValueTable> ValueTable { get; set; }
        public DbQuery<vewHRMSEmployeeSurveyPIC> HRMSEmployeeSurveyPIC { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            modelBuilder.Query<vewHRMSEmployeeSurveyPIC>().ToView("vewHRMSEmployeeSurveyPIC");
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

                entity.Property(e => e.ItemName).HasMaxLength(200);

                entity.Property(e => e.Step).HasColumnType("decimal(18, 10)");

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

                entity.HasOne(d => d.ItemCategNavigation)
                    .WithMany(p => p.InputItemList)
                    .HasForeignKey(d => d.ItemCateg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputItemList_ItemCategory");

                entity.HasOne(d => d.ItemCodeNavigation)
                    .WithMany(p => p.InputItemList)
                    .HasForeignKey(d => d.ItemCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InputItemList_InputItem");
            });

            modelBuilder.Entity<InputItemListMessage>(entity =>
            {
                entity.HasKey(e => new { e.ItemCateg, e.ItemCode, e.DisplayOrder })
                    .HasName("PK_InputItemMessage");

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EndMessage).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.StartMessage).HasColumnType("datetime");

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
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

                entity.Property(e => e.ItemCategName).HasMaxLength(500);

                entity.Property(e => e.Remarks1).HasMaxLength(500);

                entity.Property(e => e.Remarks2).HasMaxLength(500);

                entity.Property(e => e.Remarks3).HasMaxLength(500);

                entity.Property(e => e.Remarks4).HasMaxLength(500);

                entity.Property(e => e.Remarks5).HasMaxLength(500);

                entity.Property(e => e.RemarksColor1)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor2)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor3)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor4)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksColor5)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RemarksTitle1).HasMaxLength(500);

                entity.Property(e => e.RemarksTitle2).HasMaxLength(500);

                entity.Property(e => e.RemarksTitle3).HasMaxLength(500);

                entity.Property(e => e.RemarksTitle4).HasMaxLength(500);

                entity.Property(e => e.RemarksTitle5).HasMaxLength(500);

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

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InputKind)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName).HasMaxLength(100);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SqlReport).HasMaxLength(255);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationCondition>(entity =>
            {
                entity.ToTable("Operation_Condition");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Design).HasMaxLength(300);

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Template).HasMaxLength(300);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<OperationConditionHist>(entity =>
            {
                entity.ToTable("Operation_ConditionHist");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Design).HasMaxLength(300);

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Template).HasMaxLength(300);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<OperationGroup>(entity =>
            {
                entity.HasKey(e => e.OpeGroupCode);

                entity.Property(e => e.OpeGroupCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCateg)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupName).HasMaxLength(50);

                entity.Property(e => e.SpecialGroup)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialOperate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationItem>(entity =>
            {
                entity.HasKey(e => e.OperationNo)
                    .HasName("PK_Operation_Item_1");

                entity.ToTable("Operation_Item");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InputKind)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCateg)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OpeGroupName).HasMaxLength(50);

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperationName).HasMaxLength(100);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialOperate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OperationItemCateg>(entity =>
            {
                entity.HasKey(e => new { e.OperationCode, e.ItemCateg });

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(6)
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

                entity.HasOne(d => d.ItemCategNavigation)
                    .WithMany(p => p.OperationItemCateg)
                    .HasForeignKey(d => d.ItemCateg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperationItemCateg_ItemCategory");

                entity.HasOne(d => d.OperationCodeNavigation)
                    .WithMany(p => p.OperationItemCateg)
                    .HasForeignKey(d => d.OperationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperationItemCateg_Operation");
            });

            modelBuilder.Entity<OperationItemDetail>(entity =>
            {
                entity.HasKey(e => new { e.OperationNo, e.OperationCode, e.ItemCateg, e.ItemCode })
                    .HasName("PK_Operation_Item");

                entity.ToTable("Operation_Item_Detail");

                entity.Property(e => e.OperationNo)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OperationCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCateg)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate).HasColumnType("datetime");

                entity.Property(e => e.CategInputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

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

                entity.Property(e => e.InputOptionItem)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.InputType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategName).HasMaxLength(50);

                entity.Property(e => e.ItemName).HasMaxLength(200);

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

            modelBuilder.Entity<OperationItemValueTableDetail>(entity =>
            {
                entity.ToTable("Operation_Item_ValueTable_Detail");

                entity.Property(e => e.AddDate).HasColumnType("date");

                entity.Property(e => e.ComputerName).HasMaxLength(50);

                entity.Property(e => e.DataType).HasMaxLength(10);

                entity.Property(e => e.InputItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InputType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OperationCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperationNo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TableCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("date");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.Value).HasMaxLength(200);
            });

            modelBuilder.Entity<OperationMachine2>(entity =>
            {
                entity.ToTable("OPERATION_MACHINE2");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginQty).HasColumnName("BEGIN_QTY");

                entity.Property(e => e.BeginningBalance).HasColumnName("BEGINNING_BALANCE");

                entity.Property(e => e.Currency)
                    .HasColumnName("CURRENCY")
                    .HasMaxLength(3);

                entity.Property(e => e.DateReturnAcc)
                    .HasColumnName("DATE_RETURN_ACC")
                    .HasColumnType("datetime");

                entity.Property(e => e.DicisionNo)
                    .HasColumnName("DICISION_NO")
                    .HasMaxLength(20);

                entity.Property(e => e.FinishedTarget)
                    .HasColumnName("FINISHED_TARGET")
                    .HasMaxLength(12);

                entity.Property(e => e.InchargeSectCode)
                    .HasColumnName("INCHARGE_SECT_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.Inv)
                    .HasColumnName("INV")
                    .HasMaxLength(40);

                entity.Property(e => e.InvAmt).HasColumnName("INV_AMT");

                entity.Property(e => e.InvQty).HasColumnName("INV_QTY");

                entity.Property(e => e.Inventory)
                    .HasColumnName("INVENTORY")
                    .HasMaxLength(50);

                entity.Property(e => e.Item)
                    .HasColumnName("ITEM")
                    .HasMaxLength(100);

                entity.Property(e => e.LastMonthConfirmation)
                    .HasColumnName("LAST_MONTH_CONFIRMATION")
                    .HasMaxLength(40);

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(20);

                entity.Property(e => e.MCNo)
                    .HasColumnName("M_C_NO")
                    .HasMaxLength(50);

                entity.Property(e => e.OperationExpectToOperationMonth)
                    .HasColumnName("OPERATION_EXPECT_TO_OPERATION_MONTH")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderName)
                    .HasColumnName("ORDER_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderSectCode)
                    .HasColumnName("ORDER_SECT_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.PrNo)
                    .HasColumnName("PR_NO")
                    .HasMaxLength(20);

                entity.Property(e => e.ProcessName)
                    .HasColumnName("PROCESS_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ProcessNameProductType)
                    .HasColumnName("PROCESS_NAME_PRODUCT_TYPE")
                    .HasMaxLength(50);

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Qty1).HasColumnName("QTY_1");

                entity.Property(e => e.Qty2).HasColumnName("QTY_2");

                entity.Property(e => e.RecordMmYy)
                    .HasColumnName("RECORD_MM_YY")
                    .HasMaxLength(12);

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(500);

                entity.Property(e => e.ResponByNameDept)
                    .HasColumnName("RESPON_BY_NAME_DEPT")
                    .HasMaxLength(40);

                entity.Property(e => e.ResponToConfirmedBy)
                    .HasColumnName("RESPON_TO_CONFIRMED_BY")
                    .HasMaxLength(40);

                entity.Property(e => e.SectName)
                    .HasColumnName("SECT_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.SortYyMm)
                    .HasColumnName("SORT_YY_MM")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Specification)
                    .HasColumnName("SPECIFICATION")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(20);

                entity.Property(e => e.SupplierName)
                    .HasColumnName("SUPPLIER_NAME")
                    .HasMaxLength(80);

                entity.Property(e => e.SuspenseAcctNo)
                    .HasColumnName("SUSPENSE_ACCT_NO")
                    .HasMaxLength(15);

                entity.Property(e => e.TfToFixedAssets).HasColumnName("TF_TO_FIXED_ASSETS");

                entity.Property(e => e.TfToSpareparts).HasColumnName("TF_TO_SPAREPARTS");

                entity.Property(e => e.TotalEndingBalance).HasColumnName("TOTAL_ENDING_BALANCE");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UsefulLiftYears)
                    .HasColumnName("USEFUL_LIFT_YEARS")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<OperationMachineReport2>(entity =>
            {
                entity.ToTable("OPERATION_MACHINE_REPORT2");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BeginQty).HasColumnName("BEGIN_QTY");

                entity.Property(e => e.BeginningBalance).HasColumnName("BEGINNING_BALANCE");

                entity.Property(e => e.Currency)
                    .HasColumnName("CURRENCY")
                    .HasMaxLength(3);

                entity.Property(e => e.DateReturnAcc)
                    .HasColumnName("DATE_RETURN_ACC")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1901/01/01')");

                entity.Property(e => e.DicisionNo)
                    .HasColumnName("DICISION_NO")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FinishedTarget)
                    .HasColumnName("FINISHED_TARGET")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InchargeSectCode)
                    .HasColumnName("INCHARGE_SECT_CODE")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Inv)
                    .HasColumnName("INV")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InvAmt).HasColumnName("INV_AMT");

                entity.Property(e => e.InvQty).HasColumnName("INV_QTY");
              

                entity.Property(e => e.Item)
                    .HasColumnName("ITEM")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastMonthConfirmation)
                    .HasColumnName("LAST_MONTH_CONFIRMATION")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Location)
                    .HasColumnName("LOCATION")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MCNo)
                    .HasColumnName("M_C_NO")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OperationExpectToOperationMonth)
                    .HasColumnName("OPERATION_EXPECT_TO_OPERATION_MONTH")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OrderName)
                    .HasColumnName("ORDER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OrderSectCode)
                    .HasColumnName("ORDER_SECT_CODE")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrNo)
                    .HasColumnName("PR_NO")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProcessName)
                    .HasColumnName("PROCESS_NAME")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProcessNameProductType)
                    .HasColumnName("PROCESS_NAME_PRODUCT_TYPE")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Qty).HasColumnName("QTY");

                entity.Property(e => e.Qty1).HasColumnName("QTY_1");

                entity.Property(e => e.Qty2).HasColumnName("QTY_2");

                entity.Property(e => e.RecordMmYy)
                    .HasColumnName("RECORD_MM_YY")
                    .HasMaxLength(12)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remark)
                    .HasColumnName("REMARK")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ResponByNameDept)
                    .HasColumnName("RESPON_BY_NAME_DEPT")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ResponToConfirmedBy)
                    .HasColumnName("RESPON_TO_CONFIRMED_BY")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SectName)
                    .HasColumnName("SECT_NAME")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

              

                entity.Property(e => e.Specification)
                    .HasColumnName("SPECIFICATION")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SupplierName)
                    .HasColumnName("SUPPLIER_NAME")
                    .HasMaxLength(80)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SuspenseAcctNo)
                    .HasColumnName("SUSPENSE_ACCT_NO")
                    .HasMaxLength(15)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TfToFixedAssets)
                    .HasColumnName("TF_TO_FIXED_ASSETS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TfToSpareparts)
                    .HasColumnName("TF_TO_SPAREPARTS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalEndingBalance).HasColumnName("TOTAL_ENDING_BALANCE");

                entity.Property(e => e.Unit)
                    .HasColumnName("UNIT")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UsefulLiftYears)
                    .HasColumnName("USEFUL_LIFT_YEARS")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OperatorRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.OperatorId })
                    .HasName("PK_OperatorRole_1");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.OperatorId)
                    .HasColumnName("OperatorID")
                    .HasMaxLength(100)
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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.OperatorRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OperatorRole_Role");
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
                    .HasMaxLength(300)
                    .IsUnicode(false);
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

                entity.Property(e => e.AddDate).HasColumnType("date");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InputOption)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SupplementItemDetail>(entity =>
            {
                entity.HasKey(e => e.SupplementId);

                entity.Property(e => e.SupplementId)
                    .HasColumnName("SupplementID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddDate).HasColumnType("date");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50);

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

                entity.Property(e => e.UpdDate).HasColumnType("date");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ztcname)
                    .HasColumnName("ZTCName")
                    .HasMaxLength(50);

                entity.Property(e => e.Ztcno)
                    .IsRequired()
                    .HasColumnName("ZTCNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SurveyPicsickLeave>(entity =>
            {
                entity.HasKey(e => e.SurveyPiccode);

                entity.ToTable("SurveyPICSickLeave");

                entity.Property(e => e.SurveyPiccode)
                    .HasColumnName("SurveyPICCode")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Codempid)
                    .HasColumnName("CODEMPID")
                    .HasMaxLength(10);

                entity.Property(e => e.Department)
                    .HasColumnName("DEPARTMENT")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Department1)
                    .HasColumnName("DEPARTMENT1")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PiccodeName)
                    .HasColumnName("PICCodeName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasColumnName("SECTION")
                    .HasMaxLength(200)
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

                entity.Property(e => e.ValueText).HasMaxLength(200);
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

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.Property(e => e.ValueCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
