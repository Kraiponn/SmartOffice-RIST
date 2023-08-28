using SmartOffice.EmailCore.Models;
using Microsoft.EntityFrameworkCore;
using SmartOffice.EmailCore.IResponsitory;
using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SmartOffice.EmailCore
{
    public class SendEmail:ISendEmail
    {
         private readonly SendmailContext _SendmailContext;
        private readonly IHttpContextAccessor _context;
        public SendEmail(SendmailContext context, IHttpContextAccessor contextHttp)
        {
            _SendmailContext = context;
            _context = contextHttp;
        }
     public string EmailApprove_Alert(string SeqNo,string ToAddress,string CCAddress,string BCCAddress, string ReplyTo, string Subject, string BodyData, string BodyDataDetail, 
                string Attachments, string ExecSQL, string ExecDatabase, string ExecSQLResultFileName)
        {
            string htmlbody = File.ReadAllText("wwwroot/Template/alert _realtime.html");
            var _SeqNo = new SqlParameter("@SeqNo", 1);
            var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _CCAddress = new SqlParameter("@CCAddress", CCAddress ?? SqlString.Null);
            var _BCCAddress = new SqlParameter("@BCCAddress", BCCAddress ?? SqlString.Null);
            var _ReplyTo = new SqlParameter("@ReplyTo", ReplyTo ?? SqlString.Null);
            var _Subject = new SqlParameter("@Subject", Subject ?? SqlString.Null);
            var _BodyData = new SqlParameter("@BodyData", BodyData ?? SqlString.Null);
            var _BodyDataDetail = new SqlParameter("@BodyDataDetail", htmlbody ?? SqlString.Null);
            var _Attachments = new SqlParameter("@Attachments", Attachments ?? SqlString.Null);
            var _ExecSQL = new SqlParameter("@ExecSQL", ExecSQL ?? SqlString.Null);
            var _ExecDatabase = new SqlParameter("@ExecDatabase", ExecDatabase ?? SqlString.Null);
            var _ExecSQLResultFileName = new SqlParameter("@ExecSQLResultFileName", ExecSQLResultFileName ?? SqlString.Null);
            var sql = "EXEC Sendmail.dbo.sprSendMail @SeqNo,@ToAddress,@CCAddress,@BCCAddress,@ReplyTo,@Subject, @BodyData,@BodyDataDetail,@Attachments,@ExecSQL,@ExecSQLResultFileName,@ExecDatabase";
            try
            {
               var a = _SendmailContext.Database.ExecuteSqlCommand(sql, _SeqNo, _ToAddress, _CCAddress, _BCCAddress, _ReplyTo, _Subject, _BodyData, _BodyDataDetail, _Attachments, _ExecSQL, _ExecSQLResultFileName, _ExecDatabase);

                return "Send Complete";
            }
            catch(Exception e)
            {
                return e.Message;
            }
          
        }


     public string EmailInvite(List<string> email, string title, string datestart, string Detail, string Owner ,int appid, string Type)
        {
            string htmlbody = "";

            if(Type == "") {
                htmlbody = File.ReadAllText("wwwroot/Template/InviteMail.html");
            }
            else
            {
                htmlbody = File.ReadAllText("wwwroot/Template/InviteMailCancel.html");
            }
            string allemail = "";

            //body = body.Replace("{UserName}", "Admin");
            htmlbody = htmlbody.Replace("{Appname}", title);
            htmlbody = htmlbody.Replace("{Apptime}", datestart);
            htmlbody = htmlbody.Replace("{Detail}", Detail);
            htmlbody = htmlbody.Replace("{Owner}", Owner);
     
            htmlbody = htmlbody.Replace("{linkReject}", Owner);
            var Request = _context.HttpContext.Request;
      
            var builder = new UriBuilder("http://10.29.1.87/")
            {
                Path = String.Empty
            };
            var baseUri = builder.Uri;
            var baseUrl = baseUri.ToString()+ "SmartOffice/EAppointment/JoinAppointment/?result=Y&id=" + appid ;


            htmlbody = htmlbody.Replace("{linkApp}", baseUrl);
            foreach (string item in email)
            {
                allemail = allemail + item + ";";
            }


            var _SeqNo = new SqlParameter("@SeqNo", 1);
            //var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _ToAddress = new SqlParameter("@ToAddress", allemail);
            var _CCAddress = new SqlParameter("@CCAddress", SqlString.Null);
            var _BCCAddress = new SqlParameter("@BCCAddress",  SqlString.Null);
            var _ReplyTo = new SqlParameter("@ReplyTo", SqlString.Null);
            var _Subject = new SqlParameter("@Subject", "Invite to join appointment.");
            var _BodyData = new SqlParameter("@BodyData",  SqlString.Null);
            var _BodyDataDetail = new SqlParameter("@BodyDataDetail", htmlbody ?? SqlString.Null);
            var _Attachments = new SqlParameter("@Attachments", SqlString.Null);
            var _ExecSQL = new SqlParameter("@ExecSQL",  SqlString.Null);
            var _ExecDatabase = new SqlParameter("@ExecDatabase", SqlString.Null);
            var _ExecSQLResultFileName = new SqlParameter("@ExecSQLResultFileName", SqlString.Null);
            var sql = "EXEC Sendmail.dbo.sprSendMail @SeqNo,@ToAddress,@CCAddress,@BCCAddress,@ReplyTo,@Subject, @BodyData,@BodyDataDetail,@Attachments,@ExecSQL,@ExecSQLResultFileName,@ExecDatabase";
            try
            {
                var a = _SendmailContext.Database.ExecuteSqlCommand(sql, _SeqNo, _ToAddress, _CCAddress, _BCCAddress, _ReplyTo, _Subject, _BodyData, _BodyDataDetail, _Attachments, _ExecSQL, _ExecSQLResultFileName, _ExecDatabase);
                return "Send Complete";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string SendPasswordReset(string keyreset, string empcode)
        {
            try
            {
                var _confirmID = new SqlParameter("@confirmID", keyreset);
                var _empcode = new SqlParameter("@EmpID", empcode);
                var sql = "EXEC Sendmail.dbo.sprSendMailResetPassword @confirmID,@EmpID";
                var a = _SendmailContext.Database.ExecuteSqlCommand(sql, _confirmID, _empcode);

                return "Send Complete";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

     
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////             
        public string EmailReservationRoom(string ToAddress, string CCAddress, string Subject, string Title, string Date, string User, string Detail)
        {
            string htmlbody = "";
        
                htmlbody = File.ReadAllText("wwwroot/Template/ReservationRoomMail.html");



            htmlbody = htmlbody.Replace("{Subject}", Subject);
            htmlbody = htmlbody.Replace("{Appname}", Title);
            htmlbody = htmlbody.Replace("{Date}", Date);
            htmlbody = htmlbody.Replace("{User}", User);
            htmlbody = htmlbody.Replace("{Detail}", Detail);

                      
            var _SeqNo = new SqlParameter("@SeqNo", 1);
            //var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _CCAddress = new SqlParameter("@CCAddress", CCAddress);
            var _BCCAddress = new SqlParameter("@BCCAddress", SqlString.Null);
            var _ReplyTo = new SqlParameter("@ReplyTo", SqlString.Null);
            var _Subject = new SqlParameter("@Subject", Subject + " (" + Title + " " + Date + ")");
            var _BodyData = new SqlParameter("@BodyData", SqlString.Null);
            var _BodyDataDetail = new SqlParameter("@BodyDataDetail", htmlbody ?? SqlString.Null);
            var _Attachments = new SqlParameter("@Attachments", SqlString.Null);
            var _ExecSQL = new SqlParameter("@ExecSQL", SqlString.Null);
            var _ExecDatabase = new SqlParameter("@ExecDatabase", SqlString.Null);
            var _ExecSQLResultFileName = new SqlParameter("@ExecSQLResultFileName", SqlString.Null);
            var sql = "EXEC Sendmail.dbo.sprSendMail @SeqNo,@ToAddress,@CCAddress,@BCCAddress,@ReplyTo,@Subject, @BodyData,@BodyDataDetail,@Attachments,@ExecSQL,@ExecSQLResultFileName,@ExecDatabase";
            try
            {
                var a = _SendmailContext.Database.ExecuteSqlCommand(sql, _SeqNo, _ToAddress, _CCAddress, _BCCAddress, _ReplyTo, _Subject, _BodyData, _BodyDataDetail, _Attachments, _ExecSQL, _ExecSQLResultFileName, _ExecDatabase);
                return "Send Complete";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string EmailReservationAsset(string ToAddress, string CCAddress, string Subject, string Title, string Date, string User, string Detail)
        {
            string htmlbody = "";

            htmlbody = File.ReadAllText("wwwroot/Template/ReservationAssetMail.html");



            htmlbody = htmlbody.Replace("{Subject}", Subject);
            htmlbody = htmlbody.Replace("{Appname}", Title);
            htmlbody = htmlbody.Replace("{Date}", Date);
            htmlbody = htmlbody.Replace("{User}", User);
            htmlbody = htmlbody.Replace("{Detail}", Detail);


            var _SeqNo = new SqlParameter("@SeqNo", 1);
            //var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _ToAddress = new SqlParameter("@ToAddress", ToAddress);
            var _CCAddress = new SqlParameter("@CCAddress", CCAddress);
            var _BCCAddress = new SqlParameter("@BCCAddress", SqlString.Null);
            var _ReplyTo = new SqlParameter("@ReplyTo", SqlString.Null);
            var _Subject = new SqlParameter("@Subject", Subject + " (" + Title + " " + Date + ")");
            var _BodyData = new SqlParameter("@BodyData", SqlString.Null);
            var _BodyDataDetail = new SqlParameter("@BodyDataDetail", htmlbody ?? SqlString.Null);
            var _Attachments = new SqlParameter("@Attachments", SqlString.Null);
            var _ExecSQL = new SqlParameter("@ExecSQL", SqlString.Null);
            var _ExecDatabase = new SqlParameter("@ExecDatabase", SqlString.Null);
            var _ExecSQLResultFileName = new SqlParameter("@ExecSQLResultFileName", SqlString.Null);
            var sql = "EXEC Sendmail.dbo.sprSendMail @SeqNo,@ToAddress,@CCAddress,@BCCAddress,@ReplyTo,@Subject, @BodyData,@BodyDataDetail,@Attachments,@ExecSQL,@ExecSQLResultFileName,@ExecDatabase";
            try
            {
                var a = _SendmailContext.Database.ExecuteSqlCommand(sql, _SeqNo, _ToAddress, _CCAddress, _BCCAddress, _ReplyTo, _Subject, _BodyData, _BodyDataDetail, _Attachments, _ExecSQL, _ExecSQLResultFileName, _ExecDatabase);
                return "Send Complete";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
