using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.EmailCore.IResponsitory
{
    public interface ISendEmail
    {
        string EmailApprove_Alert(string SeqNo, string ToAddress, string CCAddress, string BCCAddress, string ReplyTo, string Subject, string BodyData, string BodyDataDetail,string Attachments, string ExecSQL, string ExecDatabase, string ExecSQLResultFileName);
        string SendPasswordReset(string keyreset,string empcode);
        string EmailInvite(List<string> email, string title, string datestart, string Detail, string Owner,int appid,string Type);
        string EmailReservationRoom(string ToAddress, string CCAddress, string Subject, string Title,string Date, string User, string Detail);
        string EmailReservationAsset(string ToAddress, string CCAddress, string Subject, string Title, string Date, string User, string Detail);
    }
}
