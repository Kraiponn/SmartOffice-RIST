using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartOffice.ModelsPRApprove;

namespace SmartOffice.PRApprove
{
    public partial class PR_APPROVEContext : DbContext
    {
        public PR_APPROVEContext()
        {
        }

        public PR_APPROVEContext(DbContextOptions<PR_APPROVEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApproveDetail> ApproveDetail { get; set; }
        public virtual DbSet<AssignAuthorize> AssignAuthorize { get; set; }
        public virtual DbSet<OperatorApprove> OperatorApprove { get; set; }
        public virtual DbSet<Prdetail> Prdetail { get; set; }
        public virtual DbSet<PrdetailBk> PrdetailBk { get; set; }
        public virtual DbSet<PrdetailHistoryPur> PrdetailHistoryPur { get; set; }
        public virtual DbSet<TmpPrdetail> TmpPrdetail { get; set; }

        public DbSet<PRModel> pRModels { get; set; }

        // Unable to generate entity type for table 'dbo.Assign_Authorize_History'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Buyer_Function_2'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Operator_All_HR'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Section_2'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PR_Received'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Switch_Buyer_Hist'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Reject_Approve_Hist'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Z_PREPARE_APPROVE_BUYER'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Z_PREPARE_APPROVE_PO'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Tmp_Remain_PR_Export'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ImportLog'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Purpose'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.tblmatTemp'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.V_DISPLAY_APPROVE_BUYER2'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRDetail_BKPU20190318'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Pr_Approve_History_BKPU20190319'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.T_DeliveryPlace'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Assign_Urgent'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ZZ_Approve_Detail'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Flow_Approve_Master'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TMP_Operator_All_HR'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AccountNo_LSI'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AccountNo_LSI_No_used'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.T_DISPLAY_USER_APPROVE'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Tmp_Pr_Closing'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Operator_Approve_bk'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Flow_Approve_Master_bk'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Tmp_Approve_Detail'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.z_DISPLAY_APPROVE_NEW'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Approve_Detailbk_before_test'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.E_MAIL_ROUTINE_HIST'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Scipt_Approve_Hist'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.z_prdetail_62_dup'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Approve_Detail_History'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Z_DISPLAY_APPROVE_BUYER'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PR_Attach_Chk'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Lee_DISPLAY_APPROVE_NEW'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Pr_Approve_History'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Cancel_PR_Hist'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Cancel_Pr_Hist_Pur'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Flow_Approve_Master_BK20190411'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRDetailHistory'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Reject_Reason'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TMP_Progress_Rep'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRTEST'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PR_ATTACH_FILE_75'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Flow_Approve_Master_Hist'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PRDetailbk'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.From_Aug'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Tmp_Remain_PO_Export'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Approve_Detail_Cancle_History'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<PRModel>()
                        .HasKey(c => new { c.buyerCode});

            modelBuilder.Entity<ApproveDetail>(entity =>
            {
                entity.HasKey(e => e.Seq);

                entity.ToTable("Approve_Detail");

                entity.HasIndex(e => e.Prno)
                    .HasName("IX_Approve_Detail");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveDate)
                    .HasColumnName("Approve_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApproveFlag)
                    .HasColumnName("Approve_Flag")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AssignTo)
                    .HasColumnName("Assign_To")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionCode)
                    .HasColumnName("Charge_Section_Code")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("Create_Date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IssueSectionCode)
                    .HasColumnName("Issue_Section_Code")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LevelAppCode)
                    .HasColumnName("Level_App_Code")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpNoApp)
                    .HasColumnName("Op_No_App")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Prno)
                    .HasColumnName("PRNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SubLevelAppCode)
                    .HasColumnName("Sub_Level_App_Code")
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AssignAuthorize>(entity =>
            {
                entity.HasKey(e => e.Seq);

                entity.ToTable("Assign_Authorize");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("Create_Date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("Date_From")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateTo)
                    .HasColumnName("Date_To")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeleteFlag)
                    .HasColumnName("Delete_Flag")
                    .HasMaxLength(1)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("Department_Name")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DivName)
                    .HasColumnName("Div_Name")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Hq)
                    .HasColumnName("HQ")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpeName)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Operator)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PositionName)
                    .HasColumnName("Position_Name")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Reason)
                    .HasMaxLength(60)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SectionName)
                    .HasColumnName("Section_Name")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TempAuthorizeName)
                    .HasColumnName("Temp_Authorize_Name")
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TempAuthorizeNo)
                    .IsRequired()
                    .HasColumnName("Temp_Authorize_No")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<OperatorApprove>(entity =>
            {
                entity.HasKey(e => e.Operator);

                entity.ToTable("Operator_Approve");

                entity.Property(e => e.Operator)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Authority)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Department).HasMaxLength(40);

                entity.Property(e => e.EMail)
                    .HasColumnName("E_Mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Faxno)
                    .HasColumnName("FAXNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.OpeName).HasMaxLength(40);

                entity.Property(e => e.OpePassword)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).HasMaxLength(20);

                entity.Property(e => e.Remarks1).HasMaxLength(20);

                entity.Property(e => e.Remarks2).HasMaxLength(20);

                entity.Property(e => e.Remarks3).HasMaxLength(20);

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Telno)
                    .HasColumnName("TELNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate).HasColumnType("datetime");

                entity.Property(e => e.UpdOperator)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Prdetail>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_PRDetail_1");

                entity.ToTable("PRDetail");

                entity.HasIndex(e => e.Prno)
                    .HasName("IX_PRDetail");

                entity.HasIndex(e => e.PrnoTop)
                    .HasName("IX_PRDetail_1");

                entity.Property(e => e.Seq).HasColumnName("seq");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AfterApp)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AttachFilePath1)
                    .HasColumnName("ATTACH_FILE_PATH1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BasisofUnitPrice)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionName)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DecisionFormNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverySectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DesignatedMaker)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DevelopmentNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FileName1)
                    .HasColumnName("FILE_NAME1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FinFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.FixCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FixDvldate)
                    .HasColumnName("FixDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate1)
                    .HasColumnName("FixDVLDate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate2)
                    .HasColumnName("FixDVLDate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixItem)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixQty).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixSpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FixSpecification)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FixSupplier).HasMaxLength(50);

                entity.Property(e => e.FixSupplierCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixTotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixUnit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FixUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GroupReceipt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategoryName1)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName2)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName3)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LicenceNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LocalCurUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MakerName).HasMaxLength(100);

                entity.Property(e => e.MutualAgreement)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeDate).HasColumnType("datetime");

                entity.Property(e => e.OpeFaxno)
                    .HasColumnName("OpeFAXNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeTelno)
                    .HasColumnName("OpeTELNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderPattern)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Prctgr)
                    .IsRequired()
                    .HasColumnName("PRCTGR")
                    .HasMaxLength(20);

                entity.Property(e => e.Prno)
                    .IsRequired()
                    .HasColumnName("PRNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PrnoLine).HasColumnName("PRNoLine");

                entity.Property(e => e.PrnoTop)
                    .HasColumnName("PRNoTop")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Propename)
                    .HasColumnName("PROpename")
                    .HasMaxLength(40);

                entity.Property(e => e.PurDvldate)
                    .HasColumnName("PurDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Qty)
                    .HasColumnName("QTY")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuotationNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.Property(e => e.ReqDlvdate)
                    .HasColumnName("ReqDLVDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReqSupplier)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReqSupplierName).HasMaxLength(100);

                entity.Property(e => e.ReqSupplierReason).HasMaxLength(100);

                entity.Property(e => e.RequestUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResultsDay).HasColumnType("datetime");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName).HasMaxLength(40);

                entity.Property(e => e.SlipNoInDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Specification).HasMaxLength(50);

                entity.Property(e => e.SuspenseAccountNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TerminalId)
                    .IsRequired()
                    .HasColumnName("TerminalID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("Total_Amount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalLocalCurAmount)
                    .HasColumnName("Total_LocalCurAmount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Unit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName).HasMaxLength(30);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Urgent)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<PrdetailBk>(entity =>
            {
                entity.HasKey(e => e.Seq)
                    .HasName("PK_PRDetail");

                entity.ToTable("PRDetail_bk");

                entity.Property(e => e.Seq).HasColumnName("seq");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AfterApp)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AttachFilePath1)
                    .HasColumnName("ATTACH_FILE_PATH1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BasisofUnitPrice)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionName)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DecisionFormNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverySectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DesignatedMaker)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DevelopmentNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FileName1)
                    .HasColumnName("FILE_NAME1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FinFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FixDvldate)
                    .HasColumnName("FixDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate1)
                    .HasColumnName("FixDVLDate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate2)
                    .HasColumnName("FixDVLDate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixItem).HasMaxLength(30);

                entity.Property(e => e.FixItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixQty).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixSpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FixSpecification).HasMaxLength(30);

                entity.Property(e => e.FixSupplier).HasMaxLength(50);

                entity.Property(e => e.FixSupplierCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixTotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixUnit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FixUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GroupReceipt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategoryName1)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName2)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName3)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LicenceNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LocalCurUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MakerName).HasMaxLength(100);

                entity.Property(e => e.MutualAgreement)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeDate).HasColumnType("datetime");

                entity.Property(e => e.OpeFaxno)
                    .HasColumnName("OpeFAXNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeTelno)
                    .HasColumnName("OpeTELNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderPattern)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Prctgr)
                    .IsRequired()
                    .HasColumnName("PRCTGR")
                    .HasMaxLength(20);

                entity.Property(e => e.Prno)
                    .IsRequired()
                    .HasColumnName("PRNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PrnoLine).HasColumnName("PRNoLine");

                entity.Property(e => e.PrnoTop)
                    .HasColumnName("PRNoTop")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Propename)
                    .HasColumnName("PROpename")
                    .HasMaxLength(40);

                entity.Property(e => e.PurDvldate)
                    .HasColumnName("PurDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Qty)
                    .HasColumnName("QTY")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuotationNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(20);

                entity.Property(e => e.ReqDlvdate)
                    .HasColumnName("ReqDLVDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReqSupplier)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReqSupplierName).HasMaxLength(100);

                entity.Property(e => e.ReqSupplierReason).HasMaxLength(100);

                entity.Property(e => e.RequestUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResultsDay).HasColumnType("datetime");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName).HasMaxLength(40);

                entity.Property(e => e.SlipNoInDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Specification).HasMaxLength(30);

                entity.Property(e => e.SuspenseAccountNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TerminalId)
                    .IsRequired()
                    .HasColumnName("TerminalID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("Total_Amount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalLocalCurAmount)
                    .HasColumnName("Total_LocalCurAmount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Unit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName).HasMaxLength(30);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Urgent)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<PrdetailHistoryPur>(entity =>
            {
                entity.HasKey(e => e.RecNo);

                entity.ToTable("PRDetailHistoryPur");

                entity.Property(e => e.RecNo).ValueGeneratedNever();

                entity.Property(e => e.AccountName)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AfterApp)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AttachFilePath1)
                    .HasColumnName("ATTACH_FILE_PATH1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BasisofUnitPrice)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionName)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DecisionFormNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverySectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DesignatedMaker)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DevelopmentNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FileName1)
                    .HasColumnName("FILE_NAME1")
                    .HasMaxLength(70)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FinFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FixDvldate)
                    .HasColumnName("FixDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate1)
                    .HasColumnName("FixDVLDate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate2)
                    .HasColumnName("FixDVLDate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixItem)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixQty).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixSpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FixSpecification)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FixSupplier).HasMaxLength(50);

                entity.Property(e => e.FixSupplierCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixTotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixUnit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FixUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GroupReceipt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategoryName1)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName2)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCategoryName3)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LicenceNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LocalCurUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MakerName).HasMaxLength(100);

                entity.Property(e => e.MutualAgreement)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeDate).HasColumnType("datetime");

                entity.Property(e => e.OpeFaxno)
                    .HasColumnName("OpeFAXNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeTelno)
                    .HasColumnName("OpeTELNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderPattern)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Prctgr)
                    .IsRequired()
                    .HasColumnName("PRCTGR")
                    .HasMaxLength(20);

                entity.Property(e => e.Prno)
                    .IsRequired()
                    .HasColumnName("PRNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PrnoLine).HasColumnName("PRNoLine");

                entity.Property(e => e.PrnoTop)
                    .HasColumnName("PRNoTop")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Propename)
                    .HasColumnName("PROpename")
                    .HasMaxLength(40);

                entity.Property(e => e.PurDvldate)
                    .HasColumnName("PurDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Qty)
                    .HasColumnName("QTY")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuotationNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(20);

                entity.Property(e => e.ReqDlvdate)
                    .HasColumnName("ReqDLVDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReqSupplier)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReqSupplierName).HasMaxLength(100);

                entity.Property(e => e.ReqSupplierReason).HasMaxLength(100);

                entity.Property(e => e.RequestUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResultsDay).HasColumnType("datetime");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName).HasMaxLength(40);

                entity.Property(e => e.Seq).HasColumnName("seq");

                entity.Property(e => e.SlipNoInDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Specification).HasMaxLength(30);

                entity.Property(e => e.SuspenseAccountNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TerminalId)
                    .IsRequired()
                    .HasColumnName("TerminalID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("Total_Amount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalLocalCurAmount)
                    .HasColumnName("Total_LocalCurAmount")
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Unit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName).HasMaxLength(30);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Urgent)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TmpPrdetail>(entity =>
            {
                entity.HasKey(e => e.RecNo);

                entity.ToTable("Tmp_PRDetail");

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.AccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AfterApp)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BasisofUnitPrice)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BuyerCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ChargeSectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DecisionFormNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DeliverySectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DesignatedMaker)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DevelopmentNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FinFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountNo)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixAccountSubNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FixCurrency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FixDvldate)
                    .HasColumnName("FixDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate1)
                    .HasColumnName("FixDVLDate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixDvldate2)
                    .HasColumnName("FixDVLDate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.FixItem).HasMaxLength(30);

                entity.Property(e => e.FixItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FixItemCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixQty).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixQty2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixSpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FixSpecification).HasMaxLength(30);

                entity.Property(e => e.FixSupplier).HasMaxLength(50);

                entity.Property(e => e.FixSupplierCode)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.FixTotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FixUnit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FixUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.GroupReceipt)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ItemCategory1)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory2)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCategory3)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LicenceNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LocalCurAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LocalCurUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.MakerName).HasMaxLength(100);

                entity.Property(e => e.MutualAgreement)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OpeDate).HasColumnType("datetime");

                entity.Property(e => e.OpeFaxno)
                    .HasColumnName("OpeFAXNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OpeTelno)
                    .HasColumnName("OpeTELNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderPattern)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Prctgr)
                    .IsRequired()
                    .HasColumnName("PRCTGR")
                    .HasMaxLength(20);

                entity.Property(e => e.Prno)
                    .IsRequired()
                    .HasColumnName("PRNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.PrnoLine).HasColumnName("PRNoLine");

                entity.Property(e => e.PrnoTop)
                    .HasColumnName("PRNoTop")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Propename)
                    .HasColumnName("PROpename")
                    .HasMaxLength(40);

                entity.Property(e => e.PurDvldate)
                    .HasColumnName("PurDVLDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Qty)
                    .HasColumnName("QTY")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuotationNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.Property(e => e.Remarks).HasMaxLength(20);

                entity.Property(e => e.ReqDlvdate)
                    .HasColumnName("ReqDLVDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReqSupplier)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ReqSupplierName).HasMaxLength(100);

                entity.Property(e => e.ReqSupplierReason).HasMaxLength(100);

                entity.Property(e => e.RequestUnitPrice).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResultsDay).HasColumnType("datetime");

                entity.Property(e => e.SectionCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.SectionName).HasMaxLength(40);

                entity.Property(e => e.SlipNoInDate).HasColumnType("datetime");

                entity.Property(e => e.SpecialNote)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Specification).HasMaxLength(30);

                entity.Property(e => e.SuspenseAccountNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tax)
                    .HasColumnName("TAX")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TerminalId)
                    .IsRequired()
                    .HasColumnName("TerminalID")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName).HasMaxLength(30);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 4)");
            });
        }
    }
}
