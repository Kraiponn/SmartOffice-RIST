using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartOffice.EmailCore.Models
{
    public partial class SendmailContext : DbContext
    {
        public SendmailContext()
        {
        }

        public SendmailContext(DbContextOptions<SendmailContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<ItemTable> ItemTable { get; set; }
        public virtual DbSet<SendMail> SendMail { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => new { e.OperatorCode, e.Idx })
                    .HasName("PK_address_table");

                entity.Property(e => e.OperatorCode)
                    .HasColumnName("OPERATOR_CODE")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Idx).HasColumnName("IDX");

                entity.Property(e => e.AddDate)
                    .HasColumnName("ADD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressGroup)
                    .IsRequired()
                    .HasColumnName("ADDRESS_GROUP")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasColumnName("COMPUTER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[HostName]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()

");

                entity.Property(e => e.UpdDate)
                    .HasColumnName("UPD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[SystemUser]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user

");
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(e => e.Date);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ErrorMessage).HasMaxLength(4000);

                entity.Property(e => e.ErrorProcedure).HasMaxLength(128);
            });

            modelBuilder.Entity<ItemTable>(entity =>
            {
                entity.HasKey(e => new { e.ItemKey, e.ItemData });

                entity.ToTable("ITEM_TABLE");

                entity.Property(e => e.ItemKey)
                    .HasColumnName("ITEM_KEY")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ItemData)
                    .HasColumnName("ITEM_DATA")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AddDate)
                    .HasColumnName("ADD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasColumnName("COMPUTER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[HostName]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()

");

                entity.Property(e => e.ListBiko)
                    .HasColumnName("LIST_BIKO")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ListData)
                    .HasColumnName("LIST_DATA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate)
                    .HasColumnName("UPD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[SystemUser]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user

");
            });

            modelBuilder.Entity<SendMail>(entity =>
            {
                entity.HasKey(e => e.SeqNo);

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.Attachments).HasMaxLength(500);

                entity.Property(e => e.Bccaddress)
                    .HasColumnName("BCCAddress")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Ccaddress)
                    .HasColumnName("CCAddress")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[HostName]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[HostName] 
AS
Host_Name()

");

                entity.Property(e => e.ExecDatabase)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExecSql)
                    .HasColumnName("ExecSQL")
                    .HasMaxLength(1000);

                entity.Property(e => e.ExecSqlresultFileName)
                    .HasColumnName("ExecSQLResultFileName")
                    .HasMaxLength(50);

                entity.Property(e => e.ReplyTo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SendFlag).HasColumnType("datetime");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ToAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpdDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[CurrentTimeStamp]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[CurrentTimeStamp] 
AS
Current_TimeStamp

");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql(@"/****** Object:  Default [dbo].[SystemUser]    Script Date: 2018/05/21 13:30:43 ******/
CREATE DEFAULT [dbo].[SystemUser] 
AS
System_user

");
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("SYSTEM_LOG");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoggingDate).HasColumnType("datetime");

                entity.Property(e => e.LoginUserId)
                    .HasColumnName("LoginUserID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LoginUserName).HasMaxLength(100);

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Pcname)
                    .HasColumnName("PCName")
                    .HasMaxLength(100);

                entity.Property(e => e.Thread)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });
        }
    }
}
